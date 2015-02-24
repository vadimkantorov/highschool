using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class ContestManager
	{
		protected string _connectionString;

		public ContestManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(int contestID);
		public abstract Contest GetContest(int contestID);
		public abstract Contest[] GetContests(ContestTime type);
		public abstract void AddContest(Contest c);
		public abstract void UpdateContest(Contest c);
		public abstract void RemoveContest(int contestID);
	}
}