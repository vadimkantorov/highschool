using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	[Flags]
	public enum ContestTime
	{
		None = 0,
		Current = 1,
		Past = 2,
		Forthcoming = 4
	}

	[Serializable]
	public class Contest
	{
		int id = -1;
		DateTime beginning;
		DateTime ending;
		string name;
		string type;

		public string Type
		{
			get { return type; }
			set { type = value; }
		}

		public ContestTime Time
		{
			get
			{
				if ( ending < DateTime.Now )
					return ContestTime.Past;
				if ( beginning > DateTime.Now )
					return ContestTime.Forthcoming;
				return ContestTime.Current;
			}
		}

		public int ID
		{
			get { return id; }
			set { id = value; }
		}

		public DateTime Beginning
		{
			get { return beginning; }
			set { beginning = value; }
		}

		public DateTime Ending
		{
			get { return ending; }
			set { ending = value; }
		}

		public TimeSpan Duration
		{
			get { return ending - beginning; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public Contest()
		{}

		public Contest(DateTime beginning, DateTime ending, string name, string type)
		{
			this.beginning = beginning;
			this.ending = ending;
			this.name = name;
			this.type = type;
		}

		#region Database Access Members

		public static bool ValidateID(int contestID)
		{
			return DataProvider.Provider.ContestManager.ValidateID(contestID);
		}

		public static Contest GetContest(int contestID)
		{
			return DataProvider.Provider.ContestManager.GetContest(contestID);
		}

		public static Contest[] GetContests(ContestTime time)
		{
			if ( time != ContestTime.None )
				return DataProvider.Provider.ContestManager.GetContests(time);
			else
				return new Contest[] {};
		}

		public void Remove()
		{
			DataProvider.Provider.ContestManager.RemoveContest(id);
		}

		public void Store()
		{
			if ( id == -1 )
				DataProvider.Provider.ContestManager.AddContest(this);
			else
				DataProvider.Provider.ContestManager.UpdateContest(this);
		}

		#endregion
	}
}