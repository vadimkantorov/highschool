using System.Xml;

using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class SubmissionManager
	{
		protected string _connectionString;

		public SubmissionManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(int submisssionID);
		public abstract Submission GetSubmission(int submissionID);
		public abstract Submission[] GetSubmissions(SubmissionsFilter filter);
		public abstract int GetSubmissionsCount(SubmissionsFilter filter);
		public abstract void AddSubmission(Submission submission);
		public abstract void UpdateSubmission(Submission submission);
		public abstract string GetSource(int submissionID);
		public abstract XmlDocument GetTestLog(int submissionID);
	}
}