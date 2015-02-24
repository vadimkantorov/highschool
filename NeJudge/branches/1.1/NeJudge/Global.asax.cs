using System;
using System.Configuration;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using Ne.Database;
using Ne.Judge.ErrorIO;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public class Global : HttpApplication
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		public Global()
		{
			InitializeComponent();

		}

		protected void Application_Start(Object sender, EventArgs e)
		{
		}

		protected void Session_Start(Object sender, EventArgs e)
		{
		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{
		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{
		}

		private void SignOut()
		{
			FormsAuthentication.SignOut();
		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{
			// Extract the forms authentication cookie
			string cookieName = FormsAuthentication.FormsCookieName; //+"Roles";
			HttpCookie authCookie = Context.Request.Cookies[cookieName];
			if (null == authCookie) //|| authCookie.Expires < )
			{
				// There is no authentication cookie.
				return;
			}
			FormsAuthenticationTicket authTicket = null;
			try
			{
				authTicket = FormsAuthentication.Decrypt(authCookie.Value);
			}
			catch (Exception)
			{
				// TODO: Log exception details (omitted for simplicity)
				SignOut();
				return;
			}
			if (authTicket == null || authTicket.Expired)
			{
				// Cookie failed to decrypt.
				SignOut();
				return;
			}


			// When the ticket was created, the UserData property was assigned a
			// pipe delimited string of role names.
			Role r = (Role) Enum.Parse(typeof (Role), authTicket.UserData);
			string[] roles = r.ToString().Split(new char[] {' ', ','});

			// Create an Identity object
			FormsIdentity id = new FormsIdentity(authTicket);
			// This principal will flow throughout the request.
			GenericPrincipal principal = new GenericPrincipal(id, roles);
			// Attach the new principal object to the current HttpContext object
			Context.User = principal;

		}

		//[System.Security.Permissions.PermissionSet(System.Security.Permissions.SecurityAction.Assert, Name="FullTrust")]
		protected void Application_Error(Object sender, EventArgs e)
		{
			HttpContext.Current.Trace.Write("global.asax: Application_Error");

			Exception objError = Server.GetLastError().GetBaseException();

			// Write to Event Log based on web.config setting:
			// <add key="customErrorAutomaticLogging" value="on/off" />
			if (ConfigurationSettings.AppSettings["customErrorAutomaticLogging"].ToLower() == "on")
				WriteErrorToLog(objError);

			// Automatic E-mail Notification based on web.config setting:
			// <add key="customErrorAutomaticEmail" value="on/off" />
			// <add key="customErrorAutomaticEmailAddress" value="[address goes here]" />
			if (ConfigurationSettings.AppSettings["customErrorAutomaticEmail"].ToLower() == "on")
				EmailError(objError, ConfigurationSettings.AppSettings["customErrorEmailAddress"]);

			// If customErrorMethod isn't off or unset, then store the exception
			// web.config:appSettings:customErrorMethod follows this syntax:
			// <add key="customErrorMethod" value="application/cookie/context/querystring/off" />
			string strErrorMethod = ConfigurationSettings.AppSettings["customErrorMethod"].ToLower();
			if (strErrorMethod != "off" && strErrorMethod != "")
			{
				// Make an exception storage basket as specified by web.config setting: 
				// <add key="customErrorMethod" value="application/cookie/context/querystring/off" />
				IErrorIOHandler objErrorBasket = ErrorIOFactory.Create(strErrorMethod);

				// Store the exception in the basket while generating the path to the custom error page.
				string strRedirect = ConfigurationSettings.AppSettings["customErrorPage"];
				string strQueryString = objErrorBasket.Store(objError);
				string strFilePath = strRedirect + strQueryString;

				if (strRedirect != "")
				{
					switch (ConfigurationSettings.AppSettings["customErrorBranchMethod"].ToLower())
					{
						case "transfer":
							// Use with application, context, or cookie. Results in originally requested URL.
							Server.Transfer(strFilePath, false);
							break;
						case "redirect":
							// Use with application, cookie, or querystring. Results in the custom error page's URL.
							Response.Redirect(strFilePath);
							break;
					}
				}
			}
			/*StringBuilder message = new StringBuilder(); 
			if (Server != null) 
			{
				Exception ex;
				for (ex = Server.GetLastError(); ex != null; ex = ex.InnerException) 
				{
					message.AppendFormat("{0}: {1}{2}\r\n", 
						ex.GetType().FullName, 
						ex.Message,
						ex.StackTrace);
				}
				EventLog.WriteEntry("NeJudge",message.ToString(),EventLogEntryType.Error);
			}*/
		}

		private bool WriteErrorToLog(Exception objError)
		{
			bool res = true;
			try
			{
				string strError = objError.ToString();
				string strSource = "NeJudge Error";
				// Create the event log if it does not exist
				if (EventLog.SourceExists(strSource))
				{
					// Write to the event log
					//EventLog objEventLog;
					//objEventLog.Source = strSource;
					EventLog.WriteEntry(strSource, strError, EventLogEntryType.Error);
				}
				else
				{
					EventLog.CreateEventSource(strSource, strSource);
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
				sbMessage.Append("\r\nError Occured: ");
				sbMessage.Append(DateTime.Now.ToString());
				sbMessage.Append("\r\nFile:");
				sbMessage.Append(strErrorPath);
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

		protected void Session_End(Object sender, EventArgs e)
		{
		}

		protected void Application_End(Object sender, EventArgs e)
		{
		}

		#region Web Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		#endregion
	}
}