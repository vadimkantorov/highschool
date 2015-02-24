using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Ne.Judge
{
	public partial class DesignPage : MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			#region Header initialization
			pname.Text = Page.Header.Title;

			string groups = "";
			if( Page.User.IsInRole("Administrator") )
				groups += "Администратор";
			if( Page.User.IsInRole("Judge") )
			{
				if( groups != "" )
					groups += ", ";
				groups += "Судья";
			}
			if( Page.User.IsInRole("User") )
			{
				if( groups != "" )
					groups += ", ";
				groups += "Пользователь";
			}
			if( Page.User.IsInRole("Anonymous") )
			{
				groups = "Анонимный пользователь";
				signOut.Enabled = false;
			}
			if( !Page.User.IsInRole("Anonymous") )
				signOut.Text = "Выйти (" + Page.User.Identity.Name + ": ";
			signOut.Text += groups;
			if( !Page.User.IsInRole("Anonymous") )
				signOut.Text += ")";
			#endregion

			#region Navigation initialization
			if( Page.User.IsInRole("Anonymous") )
				anon_div.Visible = true;
			else
			{
				account_div.Visible = true;

				if( Page.User.IsInRole("Administrator") )
					admin_div.Visible = true;
				else
				{
					commonBL.Items.Add(new ListItem("Послать на проверку", "~/submit.aspx"));
					commonBL.Items.Add(new ListItem("Задать вопрос", "~/ask.aspx"));
					accountBL.Items.Add(new ListItem("Личные данные", "~/edituser.aspx?userID=" +
						Page.User.Identity.Name));
					accountBL.Items.Add(new ListItem("Посланные решения", "~/status.aspx"));
					if( Page.User.IsInRole("Judge") || Page.User.IsInRole("User") )
						accountBL.Items.Add(new ListItem("Заданные вопросы", "~/questions.aspx"));
				}
			}
			#endregion
		}

		protected void Logout(object sender, EventArgs e)
		{
			Helpers.AuthenticationHandler.SignOut();
			Response.Redirect("~/login.aspx");
		}
	}
}