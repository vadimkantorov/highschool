using System;
using System.Web;
using System.Web.Security;
using DataAccess.Queries.Users;
using Model;
using NHibernate;

namespace Web.Services
{
	public interface IUserSession
	{
		void LogIn(User user);
		void LogOut();
		User CurrentUser { get; }
		bool IsAuthenticated { get; }
	}

	class UserSession : IUserSession
	{
		public const string AnonymousUserName = "anonymous";

		public UserSession(ISession session)
		{
			var userName = HttpContext.Current.User.Identity.Name;
			IsAuthenticated = !string.IsNullOrEmpty(userName);
			
			if (IsAuthenticated)
				CurrentUser = new UserByUserName {UserName = userName}.Load(session);
			else
				CurrentUser = new UserByUserName {UserName = AnonymousUserName}.Load(session);
		}

		public void LogIn(User user)
		{
			FormsAuthentication.SetAuthCookie(user.UserName, true);
		}

		public void LogOut()
		{
			FormsAuthentication.SignOut();
		}

		public User CurrentUser { get; private set; }

		public bool IsAuthenticated { get; private set; }
	}
}