using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlUserManager : UserManager
	{
		public MsSqlUserManager(string s) : base(s)
		{}

		private static User UserFromReader(SqlDataReader rdr)
		{
			User u = new User();
			u.ID = (string) rdr["ID"];
			u.Name = (string) rdr["Name"];
			u.Password = (string) rdr["Password"];
			u.IsInvisible = (bool) rdr["IsInvisible"];
			u.Email = (string)rdr["Email"];

			u.SystemRights = (SystemRights)Enum.Parse(typeof(SystemRights), (string)rdr["SystemRights"]);
			return u;
		}

		private static ContestRegistration RegistrationFromReader(SqlDataReader rdr)
		{
			ContestRegistration cr = new ContestRegistration();
			cr.Rights = (ContestRights) Enum.Parse(typeof (ContestRights), (string) rdr["Rights"]);
			cr.IsInvisible = (bool) rdr["IsInvsible"];
			return cr;
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			comm.Parameters.AddWithValue("@id", customParam);
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			User user = (User) customParam;

			comm.Parameters.AddWithValue("@id", user.ID);
			comm.Parameters.AddWithValue("@pa", user.Password);
			comm.Parameters.AddWithValue("@na", user.Name);
			comm.Parameters.AddWithValue("@em", user.Email);
			comm.Parameters.AddWithValue("@ii", user.IsInvisible);
			comm.Parameters.AddWithValue("@ro", user.SystemRights);
		}

		private static void FillMultipleIDs(SqlCommand comm, object customParam)
		{
			object[] ids = (object[]) customParam;

			comm.Parameters.AddWithValue("@uid", ids[0]);
			comm.Parameters.AddWithValue("@cid", ids[1]);
		}

		public override void AddUser(User user)
		{
			string command = "INSERT INTO Users (ID, Password, Name, Email, IsInvisible, SystemRights)" +
			                 " VALUES (@id, @pa, @na, @em, @ii, @ro)";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, user);
		}

		public override User Authenticate(string userID, string password)
		{
			User u = GetUser(userID);
			if ( u != null && u.Password == password )
				return u;
			return null;
		}

		public override User GetUser(string userID)
		{
			string command = "SELECT * FROM Users WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, userID) )
			{
				if ( rdr.Read() )
					return UserFromReader(rdr);
				return null;
			}
		}

		public override void RemoveUser(string userID)
		{
			string command = "DELETE FROM Users WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillID, userID);
		}

		public override User[] GetUsers()
		{
			string command = "SELECT * FROM Users";
			List<User> us = new List<User>();
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, null, null) )
			{
				while ( rdr.Read() )
					us.Add(UserFromReader(rdr));
			}
			return us.ToArray();
		}

		public override void SetRegistration(string userID, int contestID, ContestRegistration registration)
		{
			string command = "UPDATE Rights SET SystemRights = @ro, IsInvisible = @ii WHERE UserID = @uid" +
			                 " AND ContestID = @cid";
			ParameterAdder pa = delegate(SqlCommand comm, object customParam)
			                    {
			                    	ContestRegistration cr = (ContestRegistration) customParam;
			                    	comm.Parameters.AddWithValue("@ro", cr.Rights);
			                    	comm.Parameters.AddWithValue("@ii", cr.IsInvisible);
			                    };
			pa += FillMultipleIDs;
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, pa, new object[] {userID, contestID});
		}

		public override ContestRegistration GetRegistration(string userID, int contestID)
		{
			string command = "SELECT SystemRights,IsInvisible FROM Rights WHERE ContestID = @cid AND UserID = @uid";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillMultipleIDs,
			                                            new object[] {userID, contestID}) )
				return RegistrationFromReader(rdr);
		}

		public override void UpdateUser(User user)
		{
			string command = "UPDATE Users SET Password = @pa, Name = @na, Email = @em, IsInvisible = @ii, SystemRights = @ro " +
			                 "WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, user);
		}

		public override bool ValidateID(string userID)
		{
			string command = "SELECT ID FROM Users WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, userID) != null);
		}

		public override ProblemRights GetProblemRights(string userID, int problemID)
		{
			throw new Exception("The method or operation is not implemented.");
		}
	}
}