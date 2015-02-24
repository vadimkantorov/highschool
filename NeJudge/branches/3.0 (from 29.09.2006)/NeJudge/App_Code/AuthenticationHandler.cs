using System;
using System.Configuration;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

using Ne.Database.Classes;

namespace Ne.Helpers
{
	public static class AuthenticationHandler
	{
		public static void SetCookieWithRoles(SystemRole role, string username, bool persist)
		{
			string roles = role.ToString();

			FormsAuthenticationConfiguration sect =
				((AuthenticationSection) ConfigurationManager.GetSection("system.web/authentication")).Forms;
			FormsAuthenticationTicket authTicket = new
				FormsAuthenticationTicket(1, username, DateTime.Now,
				                          persist ? DateTime.Now.AddYears(50) : DateTime.Now.AddMinutes(sect.Timeout.TotalMinutes),
				                          persist, roles);
			string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
			HttpCookie cook = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
			cook.Expires = authTicket.Expiration; //TODO: check
			HttpContext.Current.Response.Cookies.Set(cook);
		}

		public static void SignOut()
		{
			FormsAuthentication.SignOut();
			SetCookieWithRoles(SystemRole.Anonymous, User.AnonymousUserName, true);
		}
	}
}