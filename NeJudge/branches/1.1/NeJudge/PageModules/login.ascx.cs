using System;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public class WebForm1 : UserControl
	{
		protected TextBox usernameTextBox;
		protected TextBox passwordTextBox;
		protected CheckBox persistCheckBox;
		protected Button loginButton;
		protected ErrorMessage ErrorMessage;

		private void Page_Load(object sender, EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
			this.EnableViewState = false;
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		private void loginButton_Click(object sender, EventArgs e)
		{
			bool authenticated = false;
			if (FormsAuthentication.Authenticate(usernameTextBox.Text, passwordTextBox.Text))
				authenticated = true;
			BaseDb db = DbFactory.ConstructDatabase();
			if ( !authenticated )
			{
				try
				{
					authenticated = db.Authenticate(usernameTextBox.Text, passwordTextBox.Text);
				}
				catch (Exception ex)
				{
					ErrorMessage.Message = "Ошибка аутентификации: <br /<br />" + ex.Message + "<br />" + ex.Source;
					authenticated = false;
				}
			}
			if (authenticated)
			{
				string roles = db.GetRoles(usernameTextBox.Text).ToString("d");

				// Create the authentication ticket and store the roles in the
				// custom UserData property of the authentication ticket
				FormsAuthenticationTicket authTicket = new
					FormsAuthenticationTicket(
					1, // version
					usernameTextBox.Text, // user name
					DateTime.Now, // creation
					persistCheckBox.Checked ? DateTime.Now.AddYears(50) : DateTime.Now.AddMinutes(300), //TODO:получать timeout из конфига Expiration
					persistCheckBox.Checked, // Persistent
					roles); // User data
				string encryptedTicket = FormsAuthentication.Encrypt(authTicket);

				HttpCookie cook = new HttpCookie(FormsAuthentication.FormsCookieName, //+"Roles",
				                                 encryptedTicket);
				if (persistCheckBox.Checked)
					cook.Expires = authTicket.Expiration;
				Response.Cookies.Add(cook);

				Response.Redirect(FormsAuthentication.GetRedirectUrl(
					usernameTextBox.Text,
					persistCheckBox.Checked));

				//FormsAuthentication.RedirectFromLoginPage(usernameTextBox.Text, persistCheckBox.Checked);
			}
			else
				ErrorMessage.Message = "<b>Логин или пароль неверен</b>, пожалуйста, введите снова.<br/> Возможно, Вы не зарегистрированы - <a href=\"edituser.aspx\">сделайте это</a>";
			db.Close();
		}
	}
}