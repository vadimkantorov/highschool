using System;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	[Flags]
	public enum ContestRole
	{
		Judge = 1,
		Contestant = 2
	}

	[Flags]
	public enum SystemRole
	{
		Administrator = 1,
		Judge = 2,
		User = 4,
		Anonymous = 8
	}

	[Serializable]
	public class User
	{
		#region Fields

		string id = "";
		string password;
		string name;
		string email;
		bool isBanned;
		bool isInvisible;
		SystemRights systemRights = SystemRights.None;
		SystemRole _role = SystemRole.User;

		#endregion

		#region Properties

		public static string AnonymousUserName
		{
			get { return "Anonymous"; }
		}

		public string ID
		{
			get { return id; }
			set { id = value; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		public bool IsInvisible
		{
			get { return isInvisible; }
			set { isInvisible = value; }
		}

		public SystemRights SystemRights
		{
			get { return systemRights; }
			set { systemRights = value; }
		}

		[Obsolete]
		public SystemRole Role
		{
			get { return _role; }
			set { _role = value; }
		}

		#endregion

		#region Constructors

		public User(string passwd, string name, string email, bool isBanned, bool isInvisible)
		{
			this.password = passwd;
			this.name = name;
			this.email = email;
			this.isBanned = isBanned;
			this.isInvisible = isInvisible;
		}

		public User()
		{ }

		#endregion

		#region Database Access Members

		public static bool ValidateID(string userID)
		{
			return DataProvider.Provider.UserManager.ValidateID(userID);
		}

		public static User GetUser(string userID)
		{
			return DataProvider.Provider.UserManager.GetUser(userID);
		}

		public static User[] GetUsers()
		{
			return DataProvider.Provider.UserManager.GetUsers();
		}

		public static User Authenticate(string username, string password)
		{
			return DataProvider.Provider.UserManager.Authenticate(username, password);
		}

		public ContestRegistration GetRegistration(int contestID)
		{
			return DataProvider.Provider.UserManager.GetRegistration(id, contestID);
		}

		public static ContestRegistration GetRegistration(string userID, int contestID)
		{
			return DataProvider.Provider.UserManager.GetRegistration(userID, contestID);
		}

		public void SetRegistration(int contestID, ContestRegistration registration)
		{
			DataProvider.Provider.UserManager.SetRegistration(id, contestID, registration);
		}

		public void Store()
		{
			if( id != AnonymousUserName )
			{
				if( !ValidateID(id) )
					DataProvider.Provider.UserManager.AddUser(this);
				else
					DataProvider.Provider.UserManager.UpdateUser(this);
			}
			else
				throw new NeDatabaseException("This username is reserved");
		}
		
		public ProblemRights GetProblemRights(int problemID)
		{
			ProblemRights pr = ProblemRights.None;
			ContestRights baseRights = GetContestRights(Problem.GetProblem(problemID).ContestID);
			ProblemRights concreteRights = DataProvider.Provider.UserManager.GetProblemRights(id, problemID);
			for( int i = 1; i <= 1024; i *= 4 )
				ProceeProblemRight((ProblemRights)i, (ProblemRights)( i * 2 ), (ContestRights)i,baseRights,concreteRights, ref pr);
			return pr;
		}

		public ContestRights GetContestRights(int contestID)
		{
			ContestRights cr = ContestRights.None;
			ContestRights concreteRights = DataProvider.Provider.UserManager.GetRegistration(id, contestID).Rights;

			for( int i = 1; i <= 16384; i *= 4 )
				ProcessContestRight((ContestRights)i, (ContestRights)( i * 2 ), (SystemRights)i, systemRights, concreteRights, ref cr);
			return cr;
		}

		void ProceeProblemRight(ProblemRights problemAllow, ProblemRights problemDeny, ContestRights contestAllow, ContestRights baseRights,
			ProblemRights concreteRights, ref ProblemRights pr)
		{
			if( ( baseRights & contestAllow ) != 0 )
			{
				if( ( concreteRights & problemDeny ) != 0 )
					pr |= problemDeny;
				else
					pr |= problemAllow;
			}
			else
			{
				if( ( concreteRights & problemAllow ) != 0 )
					pr |= problemAllow;
				else
					pr |= problemDeny;
			}
		}


		void ProcessContestRight(ContestRights contestAllow, ContestRights contestDeny, SystemRights systemAllow, SystemRights baseRights,
			ContestRights concreteRights, ref ContestRights cr)
		{
			if( (baseRights & systemAllow) != 0 )
			{
				if( (concreteRights & contestDeny) != 0 )
					cr |= contestDeny;
				else
					cr |= contestAllow;
			}
			else
			{
				if( (concreteRights & contestAllow) != 0 )
					cr |= contestAllow;
				else
					cr |= contestDeny;
			}
		}

		#endregion
	}
}