namespace Ne.Database.New
{
	public class User : IStoreable
	{
		internal string _id = "";
		private string _password;
		private string _fullname;
		private UserPrivileges _privileges;
		private string _eMail;

		public string ID
		{
			get { return _id; }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; }
		}

		public string FullName
		{
			get { return _fullname; }
			set { _fullname = value; }
		}

		public UserPrivileges Privileges
		{
			get { return _privileges; }
			set { _privileges = value; }
		}

		public string EMail
		{
			get { return _eMail; }
			set { _eMail = value; }
		}

		public User() {}
		
		public User(string password, string fullName, UserPrivileges privileges, string eMail)
		{
			_password = password;
			_fullname = fullName;
			_privileges = privileges;
			_eMail = eMail;
		}

		public static bool ValidateID(string userID)
		{
			return true;
		}

		public static User GetUser(User u)
		{
			return null;
		}

		public static User[] GetUsers()
		{
			return null;
		}

		public ContestRegistration GetRegistration(int contestId)
		{
			return new ContestRegistration();
		}

		public void SetRegistration(int contestId, ContestRegistration registration)
		{}

		public void Store()
		{}
	}
}
