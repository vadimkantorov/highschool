using System;
using System.Globalization;
using System.Text;
using System.Web;

namespace Ne.Judge.ErrorIO
{
// Exceptional Events, Exceptional Measures
// Published: http://msdn.microsoft.com/asp.net/ 
// Author: Eli Robillard 
// Website: http://www.erobillard.com/

	public interface IErrorIOHandler
	{
		string Store(Exception ex);
		ErrorStorage Retrieve();
		void Clear();
	}

	public class ErrorIOFactory
	{
		private ErrorIOFactory()
		{
		}

		public static IErrorIOHandler Create(string strMethod)
		{
			// Dim strMethod As string
			// The method to use is defined in the web.config, appSettings section like so:
			// 	<appSettings>
			// 		<add key="customErrorMethod" value="cookie" />
			// 	</appSettings>
			// strMethod = System.Configuration.ConfigurationSettings.AppSettings("customErrorMethod")
			switch (strMethod.ToLower(CultureInfo.InstalledUICulture))
			{
				case "application":
					HttpContext.Current.Trace.Write("ErrorIOFactory: Create('Application')");
					return new ErrorApplication();
				case "context":
					HttpContext.Current.Trace.Write("ErrorIOFactory: Create('Context')");
					return new ErrorContext();
				case "cookie":
					HttpContext.Current.Trace.Write("ErrorIOFactory: Create('Cookie')");
					return new ErrorCookie();
				case "querystring":
					HttpContext.Current.Trace.Write("ErrorIOFactory: Create('Querystring')");
					return new ErrorQuerystring();
				default:
					// Cookie works with contextErrors mode="On", global.asax:Response.Redirect, global.asax:Server.Transfer, 
					// and is not prone to displaying someone else's error (as Application may be). So, it wins default.
					HttpContext.Current.Trace.Write("ErrorIOFactory: Create('[Cookie by default]')");
					return new ErrorCookie();
			}
		}
	}

	public class ErrorStorage
	{
		// An abstract class with shared fields and methods 
		public string ErrorPath;
		public string Message;
		public string Source;
		public string StackTrace;
		public string Date;
		public string Type;
	}


	public class ErrorApplication : ErrorStorage, IErrorIOHandler
	{
		public string Store(Exception objError)
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Application Store()");
			ErrorPath = HttpContext.Current.Request.FilePath;
			// Build IP address into each identifier to ensure clients don't get each other's errors.
			string _errorip = HttpContext.Current.Request.UserHostAddress;

			HttpContext.Current.Application.Lock();
			HttpContext.Current.Application.Add("LastError" + _errorip, objError);
			HttpContext.Current.Application.Add("LastErrorDate" + _errorip, DateTime.Now.ToString(CultureInfo.InstalledUICulture));
			HttpContext.Current.Application.Add("LastErrorPath" + _errorip, ErrorPath);
			HttpContext.Current.Application.UnLock();

			return "?aspxErrorPath=" + ErrorPath;
		}

		public ErrorStorage Retrieve()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Application Retrieve()");
			try
			{
				// IP addresses are built into each identifier to ensure clients don't get each other's errors.
				string errorip = HttpContext.Current.Request.UserHostAddress;
				Exception objException = HttpContext.Current.Application.Get("LastError" + errorip) as Exception;

				Message = objException.Message;
				Source = objException.Source;
				StackTrace = objException.StackTrace;
				Type = objException.GetType().FullName;
				Date = HttpContext.Current.Application.Get("LastErrorDate" + errorip) as string;
				ErrorPath = HttpContext.Current.Application.Get("LastErrorPath" + errorip) as string;
			}
			catch
			{
				Message = "There was a problem retrieving Error data from the Application.";
				Source = "Assembly ErrorIO; class ErrorApplication; Method Retrieve().";
				StackTrace = "Not available.";
				Date = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
				ErrorPath = "Not available.";
				Type = "Not available.";
			}
			return this;
		}

		public void Clear()
		{
			// IP address is built into each identifier to ensure clients don't get each other's errors.
			string _errorip = HttpContext.Current.Request.UserHostAddress;

			HttpContext.Current.Trace.Write("ErrorIOFactory: Application Clear()");
			HttpContext.Current.Application.Remove("LastError" + _errorip);
			HttpContext.Current.Application.Remove("LastErrorDate" + _errorip);
			HttpContext.Current.Application.Remove("LastErrorPath" + _errorip);
		}

	}


	public class ErrorCookie : ErrorStorage, IErrorIOHandler
	{
		public string Store(Exception objError)
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Cookie Store()");
			ErrorPath = HttpContext.Current.Request.FilePath;

			HttpCookie cookieError = new HttpCookie("LastError");
			cookieError.Values["Message"] = objError.Message;
			cookieError.Values["Source"] = objError.Source;
			cookieError.Values["StackTrace"] = objError.StackTrace;
			cookieError.Values["DateTime"] = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
			cookieError.Values["FilePath"] = ErrorPath;
			cookieError.Values["Type"] = objError.GetType().FullName;
			cookieError.Expires = DateTime.Now.AddMinutes(30);
			HttpContext.Current.Response.Cookies.Add(cookieError);
			return "?aspxErrorPath=" + ErrorPath;
		}

		public ErrorStorage Retrieve()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Cookie Retrieve()");

			HttpCookie cookieError = HttpContext.Current.Request.Cookies["LastError"];
			try
			{
				Message = cookieError.Values["Message"];
				Source = cookieError.Values["Source"];
				StackTrace = cookieError.Values["StackTrace"];
				Date = cookieError.Values["DateTime"];
				ErrorPath = cookieError.Values["FilePath"];
				Type = cookieError.Values["Type"];
			}
			catch
			{
				Message = "There was a problem retrieving Error data from the Cookie.";
				Source = "Assembly ErrorIO; class ErrorCookie; Method Retrieve().";
				StackTrace = "Not available.";
				Date = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
				ErrorPath = "Not available.";
				Type = "Not available.";
			}
			return this;
		}

		public void Clear()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Cookie Clear()");
			HttpCookie cookieError = new HttpCookie("Error");
			cookieError.Expires = DateTime.Now.AddYears(-30);
			HttpContext.Current.Response.Cookies.Add(cookieError);
		}
	}


	public class ErrorContext : ErrorStorage, IErrorIOHandler
	{
		public string Store(Exception objError)
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Context Store()");
			ErrorPath = HttpContext.Current.Request.FilePath;
			HttpContext.Current.Items.Add("LastError", objError);
			HttpContext.Current.Items.Add("LastErrorDate", DateTime.Now.ToString(CultureInfo.InstalledUICulture));
			HttpContext.Current.Items.Add("LastErrorPath", ErrorPath);
			return "?aspxErrorPath=" + ErrorPath;
		}

		public ErrorStorage Retrieve()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Context Retrieve()");
			try
			{
				Exception objException = HttpContext.Current.Items["LastError"] as Exception;
				Message = objException.Message;
				Source = objException.Source;
				StackTrace = objException.StackTrace;
				Type = objException.GetType().FullName;
				Date = HttpContext.Current.Items["LastErrorDate"] as string;
				ErrorPath = HttpContext.Current.Items["LastErrorPath"] as string;
			}
			catch
			{
			}
				// Any part of the above could fail, so only replace empty items with N/A or a better description.
			finally
			{
				if (Message == "" || Source == "")
				{
					Message = "There was a problem retrieving Error data from the Context.";
					Source = "Assembly ErrorIO; class ErrorQuerystring; Method Retrieve().";
				}
				if (StackTrace == "")
					StackTrace = "Not available.";
				if (Date == "")
					Date = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
				if (ErrorPath == "")
					ErrorPath = "Not available.";
				if (Type == "")
					Type = "Not available.";
			}
			return this;
		}


		public void Clear()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Context Clear()");
			HttpContext.Current.Items.Remove("LastError");
			HttpContext.Current.Items.Remove("LastErrorDate");
			HttpContext.Current.Items.Remove("LastErrorPath");
		}
	}


	public class ErrorQuerystring : ErrorStorage, IErrorIOHandler
	{
		public string Store(Exception objError)
		{
			// Note: in this implementation the values aren't actually stored, but put into a value to be retrieved with Querystring()
			HttpContext.Current.Trace.Write("ErrorIOFactory: Querystring Store()");
			StringBuilder sbQuerystring = new StringBuilder(512);
			ErrorPath = HttpContext.Current.Request.FilePath;
			sbQuerystring.Append("?aspxErrorPath=");
			sbQuerystring.Append(ErrorPath);
			sbQuerystring.Append("&message=");
			sbQuerystring.Append(objError.Message);
			sbQuerystring.Append("&source=");
			sbQuerystring.Append(objError.Source);
			sbQuerystring.Append("&stacktrace=");
			sbQuerystring.Append(HttpUtility.UrlEncode(objError.StackTrace));
			sbQuerystring.Append("&errordate=");
			sbQuerystring.Append(DateTime.Now.ToString(CultureInfo.InstalledUICulture));
			sbQuerystring.Append("&type=");
			sbQuerystring.Append(objError.GetType().FullName);
			return sbQuerystring.ToString();
		}

		public ErrorStorage Retrieve()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Querystring Retrieve()");
			Message = HttpContext.Current.Request.QueryString["message"];
			Source = HttpContext.Current.Request.QueryString["source"];
			StackTrace = HttpContext.Current.Request.QueryString["stacktrace"];
			Date = HttpContext.Current.Request.QueryString["errordate"];
			Type = HttpContext.Current.Request.QueryString["type"];
			ErrorPath = HttpContext.Current.Request.QueryString["aspxErrorPath"];

			if (Message == "" || Source == "")
			{
				Message = "There was a problem retrieving Error data from the Querystring.";
				Source = "Assembly ErrorIO; class ErrorQuerystring; Method Retrieve().";
			}
			if (StackTrace == "")
				StackTrace = "Not available.";
			if (Date == "")
				Date = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
			if (ErrorPath == "")
				ErrorPath = "Not available.";
			if (Type == "")
				Type = "Not available.";
			return this;
		}

		public void Clear()
		{
			HttpContext.Current.Trace.Write("ErrorIOFactory: Querystring Clear()");
			// Nothing to do
		}
	}
}