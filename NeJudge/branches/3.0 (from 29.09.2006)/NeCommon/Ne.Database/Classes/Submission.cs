using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	public class Submission
	{
		#region Fields

		int _id = -1;
		int _problemID;
		string _userID;
		string _langID;
		DateTime _time;
		string _outcome;

		TestLog _testLog;
		string _source;

		#endregion

		#region Properties

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public int ProblemID
		{
			get { return _problemID; }
			set { _problemID = value; }
		}

		public string UserID
		{
			get { return _userID; }
			set { _userID = value; }
		}

		public string LanguageID
		{
			get { return _langID; }
			set { _langID = value; }
		}

		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}

		public string Outcome
		{
			get { return _outcome; }
			set { _outcome = value; }
		}

		public string Source
		{
			get
			{
				//if ( _source == null )
				//	LoadSource();
				return _source;
			}
			set { _source = value; }
		}
		
		public TestLog Log
		{
			get
			{
				//if( _testLog == null)
				//	LoadLog();
				return _testLog;
			}
			set { _testLog = value; }
		}

		#endregion

		#region Constructors

		public Submission(int problemID, string source, string userID, string languageID, DateTime time) : this()
		{
			_problemID = problemID;
			_userID = userID;
			_langID = languageID;
			_source = source;
			_time = time;
		}

		public Submission()
		{
			_source = "";
			_testLog = new TestLog();
		}

		#endregion

		#region Database access members

		public void LoadSource()
		{
			if ( _id != -1 )
				_source = DataProvider.Provider.SubmissionManager.GetSource(_id);
		}
		
		public void LoadLog()
		{
			if ( _id != -1 )
				_testLog = TestLog.FromXml(DataProvider.Provider.SubmissionManager.GetTestLog(_id));
		}

		public void Store()
		{
			if ( _id == -1 )
				DataProvider.Provider.SubmissionManager.AddSubmission(this);
			else
				DataProvider.Provider.SubmissionManager.UpdateSubmission(this);
		}

		public static bool ValidateID(int submisssionID)
		{
			return DataProvider.Provider.SubmissionManager.ValidateID(submisssionID);
		}

		public static Submission GetSubmission(int submissionID)
		{
			return DataProvider.Provider.SubmissionManager.GetSubmission(submissionID);
		}

		public static Submission[] GetSubmissions(SubmissionsFilter filter)
		{
			return DataProvider.Provider.SubmissionManager.GetSubmissions(filter);
		}

		public static int GetSubmissionsCount(SubmissionsFilter filter)
		{
			return DataProvider.Provider.SubmissionManager.GetSubmissionsCount(filter);
		}

		#endregion
	}

	[Serializable]
	public class SubmissionsFilter
	{
		int problemID = 0, contestID = -1;
		string outcome = "ALL";
		string userID = null;
		int from = -1;
		int to = -1;

		public bool RequiredOutcome
		{
			get { return outcome != "ALL"; }
		}

		public bool RequiredPaging
		{
			get { return (from != -1 && to != -1); }
		}

		public bool RequiredProblemID
		{
			get { return (problemID != 0); }
		}

		public bool RequiredUserID
		{
			get { return (userID != null); }
		}

		public int From
		{
			get { return from; }
			set { from = value; }
		}

		public int To
		{
			get { return to; }
			set { to = value; }
		}

		public string Outcome
		{
			get { return outcome; }
			set { outcome = value; }
		}

		public int ProblemID
		{
			get { return problemID; }
			set { problemID = value; }
		}

		public string UserID
		{
			get { return userID; }
			set { userID = value; }
		}

		public int ContestID
		{
			get { return contestID; }
			set { contestID = value; }
		}

		public SubmissionsFilter(int contestID)
		{
			this.contestID = contestID;
		}

		public SubmissionsFilter(int contestID, int from, int to) : this(contestID)
		{
			this.from = from;
			this.to = to;
		}
	}
}