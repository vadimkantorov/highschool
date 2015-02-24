using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for printcontest.
	/// </summary>
	public class printcontest : Page
	{
		protected PlaceHolder PlaceHolder1;

		private void Page_Load(object sender, EventArgs e)
		{
			int tid;
			try
			{
				tid = int.Parse(Page.Request.QueryString["TID"]);
			}
			catch
			{
				tid = -1;
			}
			BaseDb db = DbFactory.ConstructDatabase();
			if (db.CheckTid(tid))
			{
				PlaceHolder1.Controls.Add(new LiteralControl("<div align='center'><h1 style='color:#418ade;'>" + db.GetContest(tid).Name + "</h1></div><hr>"));
				ArrayList a = db.GetProblems(tid);
				foreach (Problem p in a)
				{
					Control c = LoadControl("PageModules/printproblem.ascx");
					((printproblem) c).PID = p.PID;
					PlaceHolder1.Controls.Add(c);
					PlaceHolder1.Controls.Add(new LiteralControl("<br>"));
				}
			}
			else
			{
				throw new NeJudgeInvalidParametersException("tid");
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
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion
	}
}