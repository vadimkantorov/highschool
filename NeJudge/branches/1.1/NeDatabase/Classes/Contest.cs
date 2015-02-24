using System;

namespace Ne.Database.New
{
	public class Contest : IStoreable
	{
		internal int _id = -1;
		private DateTime _beginning;
		private DateTime _ending;
		private string _name;
		private bool _allowAll;

		public bool Old
		{
			get { return ( _ending < DateTime.Now ); }
		}

		public bool Now
		{
			get { return ( _beginning < DateTime.Now && _ending > DateTime.Now ); }
		}

		public bool Future
		{
			get { return ( _beginning > DateTime.Now ); }
		}

		[Flags]
		public enum ContestType
		{
			Past,
			Current,
			Forthcoming
		}

		public int ID
		{
			get { return _id; }
		}

		public DateTime Beginning
		{
			get { return _beginning; }
			set { _beginning = value; }
		}

		public DateTime Ending
		{
			get { return _ending; }
			set { _ending = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public bool AllowAll
		{
			get { return _allowAll; }
			set { _allowAll = value; }
		}

		public Contest() {}

		public Contest(DateTime beginning, DateTime ending, string name, bool allowAll)
		{
			_beginning = beginning;
			_ending = ending;
			_name = name;
			_allowAll = allowAll;
		}

		public static bool ValidateID(int contestID)
		{
			return true;
		}

		public static Contest GetContest(int contestID)
		{
			return null;
		}
		
		public static Contest[] GetContests(ContestType type)
		{
			return null;
		}

		public void Store()
		{}
	}
}
