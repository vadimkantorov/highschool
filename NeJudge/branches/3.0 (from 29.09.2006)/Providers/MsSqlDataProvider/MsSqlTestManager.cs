using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlTestManager : TestManager
	{
		public MsSqlTestManager(string s)
			: base(s)
		{ }

		private static Test FromReader(SqlDataReader sr)
		{
			Test c = new Test(( int ) sr["ProblemID"], ( int ) sr["TestNumber"], ( string ) sr["Description"]);
			c.Points = ( int ) sr["Points"];
			c.Input = null;
			c.Output = null;
			c.MarkStored();
			return c;
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			int[] a = ( int[] ) customParam;
			comm.Parameters.AddWithValue("@pid", a[0]);
			comm.Parameters.AddWithValue("@tn", a[1]);
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Test c = ( Test ) customParam;
			comm.Parameters.AddWithValue("@pid", c.ProblemID);
			comm.Parameters.AddWithValue("@tn", c.TestNumber);
			comm.Parameters.AddWithValue("@desc", c.Description);
			comm.Parameters.AddWithValue("@po", c.Points);

			if ( c.Input != null )
				comm.Parameters.AddWithValue("@in", c.Input);
			if ( c.Output != null )
				comm.Parameters.AddWithValue("@out", c.Output);
		}

		public override void AddTest(Test c)
		{
			string command =
				"INSERT INTO Tests (ProblemID,TestNumber,Description,Points,Input,Output) VALUES (@pid,@tn,@desc,@po,@in,@out)";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteScalar(command, FillParams, c);
		}

		public override Test GetTest(int problemID, int testNumber)
		{
			string command = "SELECT ProblemID,TestNumber,Description,Points FROM Tests WHERE ProblemID=@pid AND TestNumber=@tn";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			{
				using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, new int[] { problemID, testNumber }) )
				{
					rdr.Read();
					return FromReader(rdr);
				}
			}
		}


		public override void RemoveTest(int problemID, int testNumber)
		{
			string command = "DELETE FROM Tests WHERE ProblemID=@pid AND TestNumber=@tn";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillID, new int[] { problemID, testNumber });
		}

		public override void UpdateTest(Test c)
		{
			string inputCmdPart = c.Input == null ? "" : ",Input=@in";
			string outputCmdPart = c.Output == null ? "" : ",Output=@out";
			string command = String.Format("UPDATE Tests SET Points=@po, Description=@desc{0}{1} WHERE ProblemID=@pid AND TestNumber=@tn",
				inputCmdPart,outputCmdPart);

			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, c);
		}

		public override bool ValidateID(int problemID, int testNumber)
		{
			string command = "SELECT TestNumber FROM Tests WHERE ProblemID=@pid AND TestNumber=@tn";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, new int[] { problemID, testNumber }) != null);
		}

		byte[] GetTestBytes(string field, int problemID, int testNumber)
		{
			string command = String.Format("SELECT {0} FROM Tests WHERE ProblemID=@pid AND TestNumber=@tn", field);
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, new int[] { problemID, testNumber }) )
			{
				rdr.Read();
				long len = rdr.GetBytes(0, 0, null, 0, 0);
				byte[] buf = new byte[len];
				rdr.GetBytes(0, 0, buf, 0, buf.Length);
				return buf;
			}
		}

		public override byte[] GetInput(int problemID, int testNumber)
		{
			return GetTestBytes("Input", problemID, testNumber);
		}

		public override byte[] GetOutput(int problemID, int testNumber)
		{
			return GetTestBytes("Output", problemID, testNumber);
		}

		public override Test[] GetTests(int problemID)
		{
			string command = "SELECT ProblemID,TestNumber,Description,Points FROM Tests WHERE ProblemID=@pid";
			List<Test> tests = new List<Test>();

			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			{
				using ( SqlDataReader rdr = q.ExecuteReader(command, delegate(SqlCommand comm, object p)
					{
						comm.Parameters.AddWithValue("@pid", problemID);
					}, null) )
				{
					while ( rdr.Read() )
						tests.Add(FromReader(rdr));
				}
			}
			return tests.ToArray();
		}
	}
}