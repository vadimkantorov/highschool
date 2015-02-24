using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Security;

using Ne.Helpers;
using Ne.Database.Interfaces;
using NeUser = Ne.Database.Classes.User;

namespace Ne.Judge
{
	public class Global : HttpApplication
	{
		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

			if ( authCookie == null )
			{
				AuthenticationHandler.SignOut();
				authCookie = Request.Cookies[FormsAuthentication.FormsCookieName]; //можно считывать из Reponse
			}

			FormsAuthenticationTicket authTicket;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			catch(ArgumentException)
			{
				AuthenticationHandler.SignOut();
				authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}

			if ( authTicket.Expired )
			{
				AuthenticationHandler.SignOut();
				authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			FormsIdentity id = new FormsIdentity(authTicket);
			Context.User = new GenericPrincipal(id, authTicket.UserData.Split(new char[] {',', ' '}));
		}

		protected void Application_Error(Object sender, EventArgs e)
		{
			Exception err = Server.GetLastError().GetBaseException();
			Server.ClearError();

			err.Data.Add("Time", DateTime.Now.ToString());
			err.Data.Add("Path", Request.FilePath);

			Context.Items["Error"] = err;
			Server.Transfer("~/error.aspx?aspxErrorPath=" + Request.FilePath);
		}

		protected void Application_Start(object sender , EventArgs e)
		{
			DataProvider.Initialize();
		}
	}
}