using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	public class User
	{
		#region Fields

		string name;
		string userName;
		string passwordHash;
		string email;

		#region ORM related fields

		int id;

		#endregion

		#endregion

		#region Properties

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string UserName
		{
			get { return userName; }
			set { userName = value; }
		}

		public string PasswordHash
		{
			get { return passwordHash; }
			set { passwordHash = value; }
		}

		public string Email
		{
			get { return email; }
			set { email = value; }
		}

		#region ORM related properties

		public int ID
		{
			get { return id; }
		}

		#endregion

		#endregion
	}
}
