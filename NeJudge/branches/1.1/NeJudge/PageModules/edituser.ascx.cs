using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for edituser.
	/// </summary>
	public class register : UserControl
	{
		protected TextBox loginTextBox;
		protected TextBox passTextBox;
		protected TextBox nameTextBox;
		protected Button finishButton;
		protected TextBox oldpassTextBox;
		protected TextBox repassTextBox;
		protected TextBox mailTextBox;
		protected Label errLiteral;
		protected HtmlGenericControl outMessage;
		protected HtmlTable Table1;
		protected RegularExpressionValidator mailTextBoxValidator;
		protected CustomValidator oldpassTextBoxValidator;
		protected RequiredFieldValidator nameTextBoxValidator;
		protected CustomValidator loginTextBoxValidator;
		protected CustomValidator passTextBoxValidator;
		protected CustomValidator repassTextBoxValidator;
		private int uid;

		private void Hide(string mess)
		{
			errLiteral.Text = mess;
			errLiteral.Visible = true;
			for (int i = 1; i < Table1.Rows.Count; i++)
			{
				Table1.Rows[i].Visible = false;
			}
			finishButton.Visible = false;
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				try
				{
					uid = int.Parse(Request.QueryString["uid"]);
				}
				catch
				{
					uid = -1;
				}
				BaseDb db = DbFactory.ConstructDatabase();
				if (db.CheckUid(uid)) //редактирование
				{
					if (!Page.User.Identity.IsAuthenticated || BaseDb.IsAnonymous(Page.User) || db.GetUid(Page.User.Identity.Name) != uid)
					{
						throw new NeJudgeException("Identity");
					}
					else
					{
						Table1.Rows[2].Visible = false;
						loginTextBoxValidator.Enabled = false;
						User u = db.GetUser(uid);
						nameTextBox.Text = u.Fullname;
						mailTextBox.Text = u.Email;
					}
				}
				else //регистрация
				{
					Table1.Rows[3].Visible = false;
					oldpassTextBoxValidator.Enabled = false;
				}
				db.Close();
			}
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
			this.loginTextBoxValidator.ServerValidate += new ServerValidateEventHandler(this.loginTextBoxValidator_ServerValidate);
			this.oldpassTextBoxValidator.ServerValidate += new ServerValidateEventHandler(this.oldpassValidator_ServerValidate);
			this.passTextBoxValidator.ServerValidate += new ServerValidateEventHandler(this.passTextBoxValidator_ServerValidate);
			this.repassTextBoxValidator.ServerValidate += new ServerValidateEventHandler(this.repassTextBoxValidator_ServerValidate);
			this.finishButton.Click += new EventHandler(this.finishButton_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		protected override void LoadViewState(object savedState)
		{
			uid = (int) savedState;
		}

		protected override object SaveViewState()
		{
			return uid;
		}

		#endregion

		private void finishButton_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				BaseDb db = DbFactory.ConstructDatabase();
				User u = new User(loginTextBox.Text.Trim(), passTextBox.Text, nameTextBox.Text.Trim(), mailTextBox.Text.Trim());
				if (uid == -1)
				{
					db.AddUser(u);
					Response.Redirect("~/default.aspx", false);
				}
				else
				{
					if (oldpassTextBox.Text == "")
						u.Password = db.GetUser(uid).Password;
					db.UpdateUser(u, uid);
					errLiteral.Text = "Данные сохранены";
					errLiteral.Visible = true;
				}
				db.Close();
			}
		}

		private void loginTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			BaseDb db = DbFactory.ConstructDatabase();
			if (db.IsRegistered(loginTextBox.Text.Trim()))
			{
				loginTextBoxValidator.ErrorMessage = "<FONT COLOR='Red'><b>Этот логин зарезервирован. Попробуйте ввести другой.</b></FONT>";
				args.IsValid = false;
			}
			else if (loginTextBox.Text.Trim() == "")
				args.IsValid = false;
			db.Close();
		}

		private void repassTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (oldpassTextBox.Text != "" || oldpassTextBoxValidator.Enabled == false)
				if (repassTextBox.Text != passTextBox.Text)
					args.IsValid = false;
		}

		private void oldpassValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (oldpassTextBox.Text != "")
			{
				BaseDb db = DbFactory.ConstructDatabase();
				args.IsValid = db.Authenticate(Page.User.Identity.Name, oldpassTextBox.Text);
				db.Close();
			}
		}

		private void passTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (oldpassTextBox.Text != "" || oldpassTextBoxValidator.Enabled == false)
				if (passTextBox.Text.Trim() == "")
					args.IsValid = false;
		}
	}
}