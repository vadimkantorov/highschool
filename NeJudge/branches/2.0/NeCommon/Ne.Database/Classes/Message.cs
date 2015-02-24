using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	public enum MessageType
	{
		Question,
		Clarification,
		JuryMessage
	}

	public class Message
	{
		private int _id = -1;
		private int _problemId;
		private string _userId;
		private DateTime _time;
		private MessageType _type;
		private string _fromContestant;
		private string _fromJury;

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public int ProblemID
		{
			get { return _problemId; }
			set { _problemId = value; }
		}

		public string UserID
		{
			get { return _userId; }
			set { _userId = value; }
		}

		public DateTime Time
		{
			get { return _time; }
			set { _time = value; }
		}

		public MessageType Type
		{
			get { return _type; }
			set { _type = value; }
		}

		public string ContestantMessage
		{
			get { return _fromContestant; }
			set { _fromContestant = value; }
		}

		public string JuryMessage
		{
			get { return _fromJury; }
			set { _fromJury = value; }
		}

		/*public bool ValidateID(int messageID)
		{
			return true;
		}*/

		public static Message GetMessage(int messageID)
		{
			return DataProvider.Provider.MessageManager.GetMessage(messageID);
		}

		public static Message[] GetMessages(MessagesFilter mf)
		{
			return DataProvider.Provider.MessageManager.GetMessages(mf);
		}

		public Message()
		{}

		public Message(int problemID, string userID, DateTime time, MessageType type, string contestantText,
		               string juryText)
		{
			_problemId = problemID;
			_userId = userID;
			_time = time;
			_type = type;
			_fromContestant = contestantText;
			_fromJury = juryText;
		}

		public void Store()
		{
			if ( _id == -1 )
				DataProvider.Provider.MessageManager.AddMessage(this);
			else
				DataProvider.Provider.MessageManager.UpdateMessage(this);
		}
	}

	[Serializable]
	public class MessagesFilter
	{
		int contestID, problemID;
		MessageType mtype;
		string userID = "ALL";
		bool emptyJuryMessage = false;
		
		public MessagesFilter(int contestID, MessageType mtype)
		{
			this.contestID = contestID;
			this.mtype = mtype;
			if( mtype == MessageType.JuryMessage )
				emptyJuryMessage = false;
		}

		public int ContestID
		{
			get { return contestID; }
			set { contestID = value; }
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

		public bool RequiredEmptyJuryMessage
		{
			get { return emptyJuryMessage; }
			set { emptyJuryMessage = value; }
		}

		public MessageType Type
		{
			get { return mtype; }
			set { mtype = value; }
		}

		public bool ReguiredProblemID
		{
			get { return problemID != 0; }
		}

		public bool RequiredUserID
		{
			get { return userID != "ALL"; }
		}
	}
}