namespace Ne.Database
{
	public class User
	{
		private string username, password, fullname, email;
		private Role r;

		public User(string username, string password, string fullname, string email)
		{
			this.username = username;
			this.password = password;
			if (fullname.Trim() != "")
				this.fullname = fullname;
			else
				this.fullname = "New user";
			this.email = email;
		}

		public bool IsInRole(Role ro)
		{
			return (r == ro);
		}

		public User(string username, string password, string fullname, string email, Role r)
		{
			this.username = username;
			this.password = password;
			this.r = r;
			if (fullname.Trim() != "")
				this.fullname = fullname;
			else
				this.fullname = "New user";
			this.email = email;
		}

		public string Username
		{
			get { return username; }
		}

		public string Fullname
		{
			get { return fullname; }
		}

		public Role Role
		{
			get { return r; }
		}

		public string Password
		{
			get { return password; }
			set { password = value; }
		}

		public string Email
		{
			get { return email; }
		}
	}
}