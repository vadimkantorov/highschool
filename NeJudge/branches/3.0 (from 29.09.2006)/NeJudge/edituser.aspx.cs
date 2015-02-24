using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ne.Helpers;
using NeUser = Ne.Database.Classes.User;
namespace Ne.Judge
{
	[RequireUserID]
	public partial class EditUserPage : Page
	{
		protected HtmlGenericControl outMessage;
		NeUser u;
		bool reg;

		private void Hide(string mess)
		{
			errLiteral.Text = mess;
			errLiteral.Visible = true;
			for ( int i = 1; i < Table1.Rows.Count; i++ )
				Table1.Rows[i].Visible = false;
			finishButton.Visible = false;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !IsPostBack )
			{
				/*RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				if ( rp.UserIDDefined ) //редактирование
				{
					reg = false;
					if(!User.IsInRole("Judge") && rp.UserID != User.Identity.Name)
						throw new NeJudgeException("Identity");
					
					Table1.Rows[2].Visible = false;
					loginTextBoxValidator.Enabled = false;
					u = NeUser.GetUser(rp.UserID);
					nameTextBox.Text = u.Name;
					mailTextBox.Text = u.Email;
				}
				else //регистрация
				{
					reg = true;
					u = new NeUser();
					Table1.Rows[3].Visible = false;
					oldpassTextBoxValidator.Enabled = false;
				}*/
			}
		}

		protected override void LoadViewState(object savedState)
		{
			Pair p = (Pair)savedState;
			u = (NeUser)p.First;
			reg = (bool)p.Second;
		}

		protected override object SaveViewState()
		{
			return new Pair(u, reg);
		}

		void UpdateField()
		{
			if ( reg || oldpassTextBox.Text != "" )
				u.Password = passTextBox.Text;
			if(reg)	
				u.ID = loginTextBox.Text;
			u.Email = mailTextBox.Text;
			u.Name = nameTextBox.Text;
		}

		protected void finishButton_Click(object sender, EventArgs e)
		{
			if ( Page.IsValid )
			{
				UpdateField();
				u.Store();
				errLiteral.Text = "Данные сохранены";
				errLiteral.Visible = true;
			}
		}

		protected void loginTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if ( NeUser.ValidateID(loginTextBox.Text) )
			{
				loginTextBoxValidator.ErrorMessage =
					"<FONT COLOR='Red'><b>Этот логин зарезервирован. Попробуйте ввести другой.</b></FONT>";
				args.IsValid = false;
			}
			else if ( loginTextBox.Text.Trim() == "" )
				args.IsValid = false;
		}

		protected void repassTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if ( oldpassTextBox.Text != "" || oldpassTextBoxValidator.Enabled == false )
				if ( repassTextBox.Text != passTextBox.Text )
					args.IsValid = false;
		}

		protected void oldpassValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if ( oldpassTextBox.Text != "" )
				args.IsValid = NeUser.Authenticate(User.Identity.Name, oldpassTextBox.Text) != null;
		}

		protected void passTextBoxValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if ( oldpassTextBox.Text != "" || oldpassTextBoxValidator.Enabled == false )
				if ( passTextBox.Text.Trim() == "" )
					args.IsValid = false;
		}
	}
}