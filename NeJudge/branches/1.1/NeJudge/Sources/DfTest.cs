#region Using directives

using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Xml;
using Ne.Database;
using Ne.Judge;

#endregion

namespace SharpDfTest
{
	public class DfTest
	{
		private string prob;
		private string subm;
		private string ext;
		private const int minute = 60000;

		public static void CreateProblem(string problem_id, uint tests_count, string test_mask,
		                                 string answer_mask, string input, string output, Limits limits, string checker_type,
		                                 string checker_command)
		{
		}

		public static string GetReportFilename(string subm)
		{
			return Path.Combine(Path.Combine(Config.SubmissionsDirectory, subm), Config.Report);
		}

		public static Limits GetLimits(string problem_id)
		{
			Limits l = new Limits(0, 0, 0);
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(Path.Combine(Path.Combine(Config.ProblemsDirectory, problem_id), "problem.xml"));
			}
			catch (Exception)
			{
			}
			XmlElement root = doc.DocumentElement;
			if (root != null)
			{
				XmlNodeList tests = root.GetElementsByTagName("tests");
				if (tests.Count > 0)
				{
					XmlNodeList lims = (tests[0] as XmlElement).GetElementsByTagName("limits");
					if (lims.Count > 0)
					{
						XmlElement lim = lims[0] as XmlElement;
						if (lim != null)
						{
							string time = lim.GetAttribute("cpu-time");
							string mem = lim.GetAttribute("memory");
							string output = lim.GetAttribute("output");
							try
							{
								l.Time = float.Parse(time, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
							}
							catch (FormatException)
							{
							}
							try
							{
								l.Output = uint.Parse(output);
							}
							catch (FormatException)
							{
							}
							try
							{
								if (mem.Length > 0 && Char.ToLower(mem[mem.Length - 1]) == 'm')
								{
									string m = mem.Substring(0, mem.Length - 1);
									l.Memory = uint.Parse(m)*1024;
								}
								else
								{
									l.Memory = uint.Parse(mem);
								}
							}
							catch (FormatException)
							{
							}
						}
					}
				}
			}
			return l;
		}

		private SubmissionResult InternalCheckSolution()
		{
			SubmissionResult res = new SubmissionResult();
			res.TimeWorked = 0;
			res.MemoryUsed = 0;
			res.Info = "";
			res.Code = Result.FA;
			string problem_arg = Path.Combine(Config.ProblemsDirectory, prob);
			string subm_arg = Path.Combine(Config.SubmissionsDirectory, subm);
			Process comp = new Process();
			comp.StartInfo.UseShellExecute = false;
			comp.StartInfo.FileName = Config.DfTest;
			comp.StartInfo.Arguments = "\"" + problem_arg + "\" \"" + subm_arg + "\" " + ext;
			comp.StartInfo.WorkingDirectory = Path.GetDirectoryName(Config.DfTest);
			comp.StartInfo.RedirectStandardOutput = true;
			Updatedb(Result.RU);
			comp.Start();
			string std_output = comp.StandardOutput.ReadToEnd();

			if (!comp.WaitForExit(minute*15))
			{
				res.Info = "Time limit exceeded for solution testing";
				return res;
			}
			if (comp.ExitCode != 0)
			{
				if (comp.ExitCode == 1)
				{
					res.Code = Result.CE;
				}
				else
				{
					res.Info = "Testing failed";
					StreamWriter sw = new StreamWriter(Path.Combine(subm_arg, "error.desc"));
					sw.Write(std_output);
					sw.Close();
				}
				return res;
			}
			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(Path.Combine(subm_arg, "log.xml"));
			}
			catch (FileNotFoundException)
			{
				res.Info = "Dftest didn't create a log file";
				return res;
			}
			catch (XmlException)
			{
				res.Info = "Dftest created invalid log";
				return res;
			}

			float max_time = 0.0F;
			int max_mem = 0;
			int test = 0;
			string last_res = "";
			string comment = "";
			XmlNodeList list = doc.ChildNodes[1].ChildNodes;
			foreach (XmlNode n in list)
			{
				XmlElement cure = n as XmlElement;
				if (n == null || n.Name != "test")
				{
					res.Info = "Log file has incorrect format";
					return res;
				}
				last_res = cure.GetAttribute("result");
				float cur_time;
				int cur_mem;
				try
				{
					cur_time = float.Parse(cure.GetAttribute("time"),
					                       NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture);
					cur_mem = int.Parse(cure.GetAttribute("memory"));
					if (max_time < cur_time)
					{
						max_time = cur_time;
					}
					if (max_mem < cur_mem)
					{
						max_mem = cur_mem;
					}
				}
				catch (ArgumentException)
				{
					res.Info = "Log file has incorrect format";
					return res;
				}
				comment = cure.InnerText;
				test++;
			}
			if (test == 0)
			{
				res.Info = "No test results were found";
				return res;
			}
			res.TimeWorked = max_time;
			res.MemoryUsed = max_mem;
			res.Info = comment;
			res.TestNumber = test;
			switch (last_res)
			{
				case "PASSED":
					res.Code = Result.AC;
					res.TestNumber = 0;
					break;
				case "CTLE":
				case "RTLE":
					res.Code = Result.TLE;
					break;
				case "RE":
					res.Code = Result.CRASH;
					break;
				default:
					res.Code = (Result) Enum.Parse(typeof (Result), last_res, true);
					break;
			}
			return res;
		}

		public void CheckSolution()
		{
			SubmissionResult s = InternalCheckSolution();
			BaseDb db = DbFactory.ConstructDatabase();
			db.AddResult(s, int.Parse(subm));
			db.Close();
		}

		public DfTest(string problem, string submission, string extension)
		{
			prob = problem;
			subm = submission;
			ext = extension;
		}

		public void Updatedb(Result code)
		{
			SqlConnection conn = new SqlConnection(SqlConfig.SqlConnectionString);
			conn.Open();
			string update = "UPDATE Submissions SET Code='" + code.ToString() + "' WHERE SID=" + subm;
			SqlCommand comm = new SqlCommand(update, conn);
			SqlTransaction trans = conn.BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
			}
			conn.Close();
		}
	}
}