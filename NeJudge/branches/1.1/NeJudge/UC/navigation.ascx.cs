using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	///		A control to display navigation.
	/// </summary>
	public class navigation : UserControl
	{
		protected Repeater anon_repeater;
		protected Repeater common_repeater;
		protected Repeater account_repeater;
		protected HtmlGenericControl anon_div;
		protected HtmlGenericControl common_div;
		protected HtmlGenericControl account_div;
		protected HtmlGenericControl admin_div;
		protected Repeater admin_repeater;

		public struct Link
		{
			public string Name;
			public string Ref;

			public Link(string name, string refer)
			{
				Name = name;
				Ref = refer;
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			anon_div.Visible = false;
			//common_div.Visible = false;
			account_div.Visible = false;
			admin_div.Visible = false;

			ArrayList cons = new ArrayList();
			cons.Add(new Link("Главная страница", "~/default.aspx"));
			cons.Add(new Link("Список соревнований", "~/contests.aspx"));
			cons.Add(new Link("Монитор", "~/monitor.aspx"));
			common_repeater.DataSource = cons;
			ArrayList acc = new ArrayList();
			if (!Page.User.Identity.IsAuthenticated)
			{
				ArrayList anon = new ArrayList();
				anon.Add(new Link("Войти в систему", "~/login.aspx"));
				anon.Add(new Link("Регистрация", "~/edituser.aspx"));
				anon_repeater.DataSource = anon;
				anon_div.Visible = true;
			}
			else if (!BaseDb.IsAnonymous(Page.User))
			{
				BaseDb db = DbFactory.ConstructDatabase();
				acc.Add(new Link("Личные данные", "~/edituser.aspx?uid=" + db.GetUid(Page.User.Identity.Name)));
				db.Close();

				if (BaseDb.IsAdmin(Page.User))
				{
					ArrayList adm = new ArrayList();
					adm.Add(new Link("Управление соревнованиями", "~/editcontest.aspx"));
					adm.Add(new Link("Управление задачами", "~/editproblem.aspx"));
					adm.Add(new Link("Просмотр всех решений", "~/status.aspx"));
					adm.Add(new Link("Просмотр вопросов", "~/questions.aspx"));
					admin_repeater.DataSource = adm;
					admin_div.Visible = true;
				}
				else
				{
					cons.Add(new Link("Послать на проверку", "~/submit.aspx"));
					cons.Add(new Link("Задать вопрос", "~/ask.aspx"));
					acc.Add(new Link("Посланные решения", "~/status.aspx"));
					acc.Add(new Link("Заданные вопросы", "~/questions.aspx"));
				}
				account_repeater.DataSource = acc;
				account_div.Visible = true;
			}
			DataBind();
			/*if(!basedb.IsAnonymous(Page.User))
			{
				basedb db = dbfactory.ConstructDatabase();
				acc.Add(new Link("Личные данные","~/edituser.aspx?uid="+db.GetUid(Page.User.Identity.Name)));
				db.Close();
			}
			if ( basedb.IsAdmin(Page.User) )
			{
				ArrayList adm = new ArrayList();
				adm.Add(new Link("Управление соревнованиями","~/editcontest.aspx"));
				adm.Add(new Link("Управление задачами","~/editproblem.aspx"));
				adm.Add(new Link("Просмотр всех решений","~/status.aspx"));
				adm.Add(new Link("Просмотр вопросов","~/questions.aspx"));
				admin_repeater.DataSource = adm;
				admin_div.Visible = true;
			}
			else if ( !basedb.IsAnonymous(Page.User) )
			{
				cons.Add(new Link("Послать на проверку","~/submit.aspx"));
				cons.Add(new Link("Задать вопрос","~/ask.aspx"));
				acc.Add(new Link("Посланные решения","~/status.aspx"));
				acc.Add(new Link("Заданные вопросы","~/questions.aspx"));
			}
			common_repeater.DataSource = cons;
			common_div.Visible = true;
			account_repeater.DataSource = acc;
			account_div.Visible = true;
		}
		if ( !Page.User.Identity.IsAuthenticated )
		{
			ArrayList anon = new ArrayList();
			anon.Add(new Link("Войти в систему","~/login.aspx"));
			anon.Add(new Link("Регистрация","~/edituser.aspx"));
			anon_repeater.DataSource = anon;
			anon_div.Visible = true;
		}*/
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
			this.EnableViewState = false;
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion
	}
}