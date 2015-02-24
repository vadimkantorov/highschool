using System;
using DataAccess.Queries.Users;
using Model;
using NHibernate;

namespace Web.Services
{
	public interface IAuthenticationService
	{
		User Authenticate(string userName, string password);
	}

	public class AuthenticationService : IAuthenticationService
	{
		public User Authenticate(string userName, string password)
		{
			var user = new UserByUserName {UserName = userName}.Load(session);
			if (user != null && user.Password.Matches(password))
				return user;
			
			return null;
		}

		public AuthenticationService(ISession session)
		{
			this.session = session;
		}

		readonly ISession session;
	}
}