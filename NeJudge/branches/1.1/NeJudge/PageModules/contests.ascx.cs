using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	public class contests : UserControl
	{
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
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		protected TextBox TextBox1;
		protected DataGrid DataGrid1;
		protected LinkButton LinkButton1;

		private void Bind()
		{
			DataTable dt = new DataTable("Соревнования");
			dt.Columns.Add("TID");
			dt.Columns.Add("Name");
			dt.Columns.Add("Monitor");
			dt.Columns.Add("Beginning", typeof (DateTime));
			dt.Columns.Add("Ending", typeof (DateTime));
			dt.Columns.Add("Status");
			DataRow dr;
			Contest t;
			using(BaseDb db = DbFactory.ConstructDatabase())
			{
				int[] oldtids = db.GetOldTids();
				int[] nowtids = db.GetNowTids();
				int[] futtids = db.GetFutureTids();
			
				foreach (int tid in nowtids)
				{
					t = db.GetContest(tid);
					dr = dt.NewRow();
					dr[0] = tid;
					if(t.Future && !BaseDb.IsAdmin(Page.User))
						dr[1] = t.Name;
					else
						dr[1] = "<a href='contest.aspx?tid=" + tid + "'>" + t.Name + "</a>";
					if(!t.Future || BaseDb.IsAdmin(Page.User))
						dr[2] = "<a href='monitor.aspx?tid=" + tid + "'>Ссылка</a>";
					dr[3] = t.Beginning;
					dr[4] = t.Ending;
					dr[5] = "Идет";
					dt.Rows.Add(dr);
				}
				foreach (int tid in futtids)
				{
					t = db.GetContest(tid);
					dr = dt.NewRow();
					dr[0] = tid;
					if(t.Future && !BaseDb.IsAdmin(Page.User))
						dr[1] = t.Name;
					else
						dr[1] = "<a href='contest.aspx?tid=" + tid + "'>" + t.Name + "</a>";
					if(!t.Future || BaseDb.IsAdmin(Page.User))
						dr[2] = "<a href='monitor.aspx?tid=" + tid + "'>Ссылка</a>";
					dr[3] = t.Beginning;
					dr[4] = t.Ending;
					dr[5] = "Ещё не начиналось ( начнется через " + HtmlFunctions.BeautifyTimeSpan(db.GetContest(tid).Beginning - DateTime.Now, false)+" )";
					dt.Rows.Add(dr);
				}
				foreach (int tid in oldtids)
				{
					t = db.GetContest(tid);
					dr = dt.NewRow();
					dr[0] = tid;
					if(t.Future && !BaseDb.IsAdmin(Page.User))
						dr[1] = t.Name;
					else
						dr[1] = "<a href='contest.aspx?tid=" + tid + "'>" + t.Name + "</a>";
					if(!t.Future || BaseDb.IsAdmin(Page.User))
						dr[2] = "<a href='monitor.aspx?tid=" + tid + "'>Ссылка</a>";
					dr[3] = t.Beginning;
					dr[4] = t.Ending;
					dr[5] = "Закончилось";
					dt.Rows.Add(dr);
				}
			}
			DataGrid1.DataSource = dt;
			DataGrid1.DataBind();
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (BaseDb.IsAdmin(Page.User))
			{
				//LinkButton1.Visible = true;
				//TextBox1.Visible = true;
			}
			Bind();
		}
	}
}