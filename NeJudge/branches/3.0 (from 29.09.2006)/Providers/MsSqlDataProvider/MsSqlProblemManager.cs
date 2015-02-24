using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	class MsSqlProblemManager : ProblemManager
	{
		public MsSqlProblemManager(string s) : base(s)
		{}

		private static Problem FromReader(SqlDataReader sr)
		{
			Problem p = new Problem();

			p.ID = (int) sr["ID"];
			p.ContestID = (int) sr["ContestID"];
			p.ShortName = (string) sr["ShortName"];
			p.Name = (string) sr["Name"];

			p.TimeLimit = (int) sr["TimeLimit"];
			p.MemoryLimit = (int) sr["MemoryLimit"];
			p.OutputLimit = (int) sr["OutputLimit"];

			p.InputFile = (string) sr["InputFile"];
			p.OutputFile = (string) sr["OutputFile"];
			p.Statement = null;
			p.CheckerBytes = null;
			return p;
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Problem problem = (Problem) customParam;

			comm.Parameters.AddWithValue("@id", problem.ID);
			comm.Parameters.AddWithValue("@cid",problem.ContestID);
			comm.Parameters.AddWithValue("@sn", problem.ShortName);
			comm.Parameters.AddWithValue("@na", problem.Name);
			comm.Parameters.AddWithValue("@tl", problem.TimeLimit);
			comm.Parameters.AddWithValue("@ml", problem.MemoryLimit);
			comm.Parameters.AddWithValue("@ol", problem.OutputLimit);
			comm.Parameters.AddWithValue("@if", problem.InputFile);
			comm.Parameters.AddWithValue("@of", problem.OutputFile);

			if(problem.Statement != null)
				comm.Parameters.AddWithValue("@st", problem.Statement.XmlDocument.InnerXml);
			if(problem.CheckerBytes != null)
				comm.Parameters.AddWithValue("@cb", problem.CheckerBytes);
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			comm.Parameters.AddWithValue("@id", customParam);
		}

		private const string SELECT_STRING = "SELECT ID,ContestID,ShortName,Name,TimeLimit,MemoryLimit,OutputLimit,"
		                                     + "InputFile,OutputFile FROM Problems";

		public override XmlDocument GetProblemXmlData(int problemID, string dataKey)
		{
			string command = String.Format("SELECT {0} FROM Problems WHERE ID=@id",dataKey);
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return q.ExecuteXmlDocument(command, FillID, problemID);
		}

		public override byte[] GetCheckerBytes(int problemID)
		{
			string command = "SELECT CheckerBytes FROM Problems WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command , FillID ,problemID) )
			{
				rdr.Read();
				long len = rdr.GetBytes(0, 0, null, 0, 0);
				byte[] buf = new byte[len];
				rdr.GetBytes(0, 0, buf, 0, buf.Length);
				return buf;
			}
		}

		public override void AddProblem(Problem problem)
		{
			string command =
				"INSERT INTO Problems (ContestID,ShortName,Name,Statement,TimeLimit,MemoryLimit,OutputLimit," +
				"InputFile,OutputFile)" +
				"VALUES (@cid,@sn,@na,@st,@tl,@ml,@ol,@if,@of);" +
				"SELECT ID FROM Problems WHERE ID=@@IDENTITY";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				problem.ID = (int) q.ExecuteScalar(command, new ParameterAdder(FillParams), problem);
		}

		public override Problem GetProblem(int problemID)
		{
			string command = String.Format("{0} WHERE ID=@id", SELECT_STRING);
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			{
				using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, problemID) )
				{
					rdr.Read();
					return FromReader(rdr);
				}
			}
		}

		public override Problem[] GetProblems(int contestID)
		{
			string command = String.Format("{0} WHERE ContestID=@id ORDER BY ShortName", SELECT_STRING);
			List<Problem> problems = new List<Problem>();

			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			{
				using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, contestID) )
				{
					while ( rdr.Read() )
						problems.Add(FromReader(rdr));
				}
			}
			return problems.ToArray();
		}

		public override void RemoveProblem(int problemID)
		{
			string command = "DELETE FROM Problems WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillID, problemID);
		}

		public override void UpdateProblem(Problem problem)
		{
			string statementCmdPart = problem.Statement == null ? "" : ",Statement=@st";
			string checkerBytesCmdPart = problem.CheckerBytes == null ? "" : ",CheckerBytes=@cb";
			string command = String.Format("UPDATE Problems SET ContestID=@cid,ShortName=@sn,Name=@na," +
				"TimeLimit=@tl,MemoryLimit=@ml,OutputLimit=@ol,InputFile=@if,OutputFile=@of{0}{1} " +
				"WHERE ID=@id",statementCmdPart,checkerBytesCmdPart);
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, problem);
		}

		public override bool ValidateID(int problemID)
		{
			string command = "SELECT ID FROM Problems WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, problemID) != null);
		}
	}
}