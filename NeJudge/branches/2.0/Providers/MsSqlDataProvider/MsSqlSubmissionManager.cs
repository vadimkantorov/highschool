using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Xml;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlSubmissionManager : SubmissionManager
	{
		public MsSqlSubmissionManager(string s) : base(s)
		{}

		private static Submission FromReader(SqlDataReader sr)
		{
			Submission s = new Submission();
			s.ID = (int) sr["ID"];
			s.ProblemID = (int) sr["ProblemID"];
			s.UserID = (string) sr["UserID"];
			s.LanguageID = (string) sr["LanguageID"];
			s.Time = (DateTime) sr["Time"];
			s.Outcome = (string)sr["Outcome"];
			s.Source = null;
			s.Log = null;
			return s;
		}

		private static void addFilterParams(SqlCommand comm, object customParam)
		{
			SubmissionsFilter filter = (SubmissionsFilter) customParam;
			if ( filter.RequiredProblemID )
				comm.Parameters.AddWithValue("@pid", filter.ProblemID);
			if ( filter.RequiredUserID )
				comm.Parameters.AddWithValue("@uid", filter.UserID);
			if ( filter.RequiredOutcome )
				comm.Parameters.AddWithValue("@ou", filter.Outcome.ToString());
			comm.Parameters.AddWithValue("@cid", filter.ContestID);
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			comm.Parameters.AddWithValue("@id", customParam);
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Submission submission = (Submission) customParam;
			comm.Parameters.AddWithValue("@id", submission.ID);
			comm.Parameters.AddWithValue("@pr", submission.ProblemID);
			comm.Parameters.AddWithValue("@us", submission.UserID);
			comm.Parameters.AddWithValue("@la", submission.LanguageID);
			comm.Parameters.AddWithValue("@ti", submission.Time);
			comm.Parameters.AddWithValue("@ou", submission.Outcome);

			if ( submission.Source != null )
				comm.Parameters.AddWithValue("@sr", submission.Source);
			if ( submission.Log != null )
				comm.Parameters.AddWithValue("@tl", submission.Log.ToXml().InnerXml);
		}

		public override int GetSubmissionsCount(SubmissionsFilter filter)
		{
			string command = "SELECT COUNT(*) FROM (SELECT Submissions.ID FROM Submissions,Contests,Problems" +
			                 " WHERE Submissions.ProblemID=Problems.ID AND Problems.ContestID = Contests.ID AND Contests.ID=@cid";
			if ( filter.RequiredUserID )
				command += " AND Submissions.UserID=@uid";
			if ( filter.RequiredOutcome )
				command += " AND Submissions.Outcome=@ou";
			if ( filter.RequiredProblemID )
				command += " AND Submissions.ProblemID = @pid";

			command += ") AS T2";

			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (int) q.ExecuteScalar(command, addFilterParams, filter);
		}

		public override void AddSubmission(Submission submission)
		{
			string command = "INSERT INTO Submissions (ProblemID,UserID,LanguageID,Time,Outcome," +
			                 "Source,TestLog) VALUES (@pr,@us,@la,@ti,@ou,@sr,@tl);" +
			                 "SELECT ID FROM Submissions WHERE ID=@@IDENTITY";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				submission.ID = (int) q.ExecuteScalar(command, FillParams, submission);
		}

		public override Submission GetSubmission(int submissionID)
		{
			string command = "SELECT ID,ProblemID,UserID,LanguageID,Time,Outcome FROM Submissions " +
			                 "WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, submissionID) )
			{
				rdr.Read();
				return FromReader(rdr);
			}
		}

		public override Submission[] GetSubmissions(SubmissionsFilter filter)
		{
			string command = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID DESC)" +
			                 " AS RowID,ID,ProblemID,UserID,LanguageID,Time,Outcome FROM (SELECT Submissions.ID,Submissions.ProblemID," +
			                 "Submissions.UserID,Submissions.LanguageID,Submissions.Time,Submissions.Outcome " +
							 "FROM Submissions,Contests,Problems" +
			                 " WHERE Submissions.ProblemID=Problems.ID AND Problems.ContestID = Contests.ID AND Contests.ID=@cid";

			if ( filter.RequiredUserID )
				command += " AND Submissions.UserID=@uid";
			if ( filter.RequiredOutcome )
				command += " AND Submissions.Outcome=@ou";
			if ( filter.RequiredProblemID )
				command += " AND Submissions.ProblemID = @pid";

			command += ") AS T2) AS T";

			if ( filter.RequiredPaging )
				command += string.Format(" WHERE RowID between {0} and {1}", filter.From, filter.To);
			else
				command += " ORDER BY RowID DESC";

			List<Submission> list = new List<Submission>();
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command.ToString(), addFilterParams, filter) )
			{
				while ( rdr.Read() )
					list.Add(FromReader(rdr));
			}
			return list.ToArray();
		}

		public override void UpdateSubmission(Submission submission)
		{
			string sourceCmdPart = submission.Source == null ? "" : ",Source=@sr";
			string testlogCmdPart = submission.Log == null ? "" : ",TestLog=@tl";
			string command = String.Format("UPDATE Submissions SET ProblemID=@pr,UserID=@us,LanguageID=@la," +
				"Time=@ti,Outcome=@ou{0}{1} WHERE ID=@id",sourceCmdPart,testlogCmdPart);
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, submission);
		}

		public override bool ValidateID(int submissionID)
		{
			string command = "SELECT ID FROM Submissions WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, submissionID) != null);
		}

		public override string GetSource(int submissionID)
		{
			string command = "SELECT Source From Submissions WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (string) q.ExecuteScalar(command, FillID, submissionID);
		}

		public override XmlDocument GetTestLog(int submissionID)
		{
			string command = "SELECT TestLog From Submissions WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return q.ExecuteXmlDocument(command, FillID, submissionID);
		}
	}
}