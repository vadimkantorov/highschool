using System;

namespace Ne.Database.New
{
	public class Message : IStoreable
	{
		private int _id = -1;
		private int _contestId;
		private int _problemId;
		private int _userId;
		private DateTime _time;
		private bool _isClarif;
		private string _text;
		private string _answer;

		public int ID
		{
			get { return _id; }
		}

		public int ContestID
		{
			get { return _contestId; }
			set { _contestId = value; }
		}

		public int ProblemID
		{
			get { return _problemId; }
			set { _problemId = value; }
		}

		public int UserID
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}

		public bool IsClarification
		{
			get { return _isClarif; }
			set { _isClarif = value; }
		}

		public string Text
		{
			get { return _text; }
			set { _text = value; }
		}

		public string Answer
		{
			get { return _answer; }
			set { _answer = value; }
		}

		public bool ValidateID(int messageID)
		{
			return true;
		}

		public static Message GetMessage(int messageID)
		{
			return null;
		}

		// userID may be asisgned to -1 to get messages from all users
		public static Message[] GetMessages(int contestId, int userID)
		{
			return null;
		}

		public Message()
		{}

		public Message(int contestID, int problemID, int userID, DateTime time, bool isClarif, string text,
			string answer)
		{
			_contestId = contestID;
			_problemId = problemID;
			_userId = userID;
			_time = time;
			_isClarif = isClarif;
			_text = text;
			_answer = answer;
		}

		public void Store()
		{}
	}
}
