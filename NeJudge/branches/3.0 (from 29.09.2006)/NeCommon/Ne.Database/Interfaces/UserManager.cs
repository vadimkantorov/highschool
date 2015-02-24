using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class UserManager
	{
		protected string _connectionString;

		public UserManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract bool ValidateID(string userID);
		public abstract User GetUser(string userID);
		public abstract User[] GetUsers();
		public abstract void AddUser(User user);
		public abstract void UpdateUser(User user);
		public abstract void RemoveUser(string userID);

		public abstract User Authenticate(string userID, string password);

		public abstract ContestRegistration GetRegistration(string userID, int contestID);
		public abstract void SetRegistration(string userID, int contestID, ContestRegistration registration);

		public abstract ProblemRights GetProblemRights(string userID, int problemID);
	}
}