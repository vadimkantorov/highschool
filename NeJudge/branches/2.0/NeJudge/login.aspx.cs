using System;
using System.Web.Security;
using System.Web.UI;

using Ne.Helpers;

using NeUser = Ne.Database.Classes.User;

namespace Ne.Judge
{
	public partial class LoginPage : Page
	{
		private const string _err_msg =
			"<b>Логин или пароль неверен</b>, пожалуйста, введите снова.<br/> Возможно, Вы не зарегистрированы - <a href=\"edituser.aspx\">сделайте это</a>";

		protected void loginButton_Click(object sender, EventArgs e)
		{
			NeUser u = NeUser.Authenticate(usernameTextBox.Text, passwordTextBox.Text);
			if ( u != null )
			{
				AuthenticationHandler.SetCookieWithRoles(u.Role, usernameTextBox.Text, persistCheckBox.Checked);
				Response.Redirect(FormsAuthentication.GetRedirectUrl(usernameTextBox.Text, persistCheckBox.Checked));

				//FormsAuthentication.RedirectFromLoginPage(usernameTextBox.Text, persistCheckBox.Checked);
			}
			else
				ErrorMessage.Message = _err_msg;
		}
	}
}