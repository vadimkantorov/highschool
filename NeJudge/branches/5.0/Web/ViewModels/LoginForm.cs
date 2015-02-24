using System;
using System.Web.Mvc;

namespace Web.ViewModels
{
	public class LoginForm
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public string Error { get; set; }

		public LoginForm WithErrorMessage(string msg)
		{
			Error = msg;
			return this;
		}
	}
}