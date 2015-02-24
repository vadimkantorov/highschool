using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Security;

using Ne.Helpers;
using Ne.Configuration;
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

		//[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Name="FullTrust")]
		protected void Application_Error(Object sender, EventArgs e)
		{
			Exception err = Server.GetLastError().GetBaseException();
			Server.ClearError();

			err.Data.Add("Time", DateTime.Now.ToString());
			err.Data.Add("Path", Request.FilePath);
			Context.Items["Error"] = err;
			Server.Transfer("error.aspx?aspxErrorPath=" + Request.FilePath);
			//if ( ConfigurationSettings.AppSettings["customErrorAutomaticEmail"].ToLower() == "on" )
			//	EmailError(objError, ConfigurationSettings.AppSettings["customErrorEmailAddress"]);
		}

		protected void Application_Start(object sender , EventArgs e)
		{
			Configurator.InitNeJudgeConfiguration();
			DataProvider.Initialize(Configurator.NeJudgeConfiguration.ProviderPath,
				Configurator.NeJudgeConfiguration.ConnectionString);
			Ne.ContestTypeHandlers.Factory.Initialize(Configurator.NeJudgeConfiguration.ContestTypeConfigurators);
		}

		private bool WriteErrorToLog(string message)
		{
			bool res = true;
			try
			{
				string source = "NeJudge Error";
				if ( EventLog.SourceExists(source) )
				{
					//EventLog objEventLog;
					//objEventLog.Source = strSource;
					EventLog.WriteEntry(source, message, EventLogEntryType.Error);
				}
				else
				{
					EventLog.CreateEventSource(source, source);
				}
			}
			catch
			{
				// Might not be able to write to the Event Log
				res = false;
			}
			return res;
		}

		private bool EmailError(Exception objError, string strTo)
		{
			bool res = true;
			try
			{
				string strError = objError.ToString();
				string strErrorPath = HttpContext.Current.Request.FilePath;
				StringBuilder sbMessage = new StringBuilder(200);
				sbMessage.Append("\r\nError: \r\n");
				sbMessage.Append(strError);
				SmtpMail.Send("ASP.NET global.asax", strTo, "Unhandled ASP.NET Error", sbMessage.ToString());
			}
			catch
			{
				// May not have SMTP service available
				res = false;
			}
			return res;
		}
	}
}