using System;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for hat.
	/// </summary>
	public class header : UserControl
	{
		protected LinkButton signOut;
		protected Label pname;

		private void Page_Load(object sender, EventArgs e)
		{
			if(!IsPostBack)
			{
				if (Page.User.Identity.IsAuthenticated)
				{
					string groups = "";
					if (BaseDb.IsAdmin(Page.User))
						groups += "Администратор";
					if (BaseDb.IsJudge(Page.User))
					{
						if (groups != "")
							groups += ", ";
						groups += "Судья";
					}
					if (BaseDb.IsUser(Page.User))
					{
						if (groups != "")
							groups += ", ";
						groups += "Пользователь";
					}
					if (BaseDb.IsAnonymous(Page.User))
					{
						if (groups != "")
							groups += ", ";
						groups += "Анонимный пользователь";
					}
					signOut.Text = "Выйти (";
					signOut.Text += Page.User.Identity.Name + ": " + groups;
					signOut.Text += ")";
				}
				else
				{
					signOut.Text = "Вы не аутентифицированы";
					signOut.Enabled = false;
				}
			}
		}

		public string PageName
		{
			set { pname.Text = value; }
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.signOut.Click += new System.EventHandler(this.LinkButton1_Click);
			this.EnableViewState = false;
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		private void LinkButton1_Click(object sender, EventArgs e)
		{
			FormsAuthentication.SignOut();
			Response.Redirect("~/login.aspx");
		}
	}
}