using System;
using System.Collections;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for SelectProblem.
	/// </summary>
	public class SelectProblem : UserControl
	{
		protected HtmlInputHidden pidField;
		protected HtmlSelect selContests;
		protected HtmlTable Table1;

		private bool now, future, old;

		protected string ctrlsl2hide;
		private void Page_Load(object sender, EventArgs e)
		{
			ArrayList tids = new ArrayList();
			BaseDb db = DbFactory.ConstructDatabase();
			if (now)
			{
				int[] nowtids = db.GetNowTids();
				foreach(int tid in nowtids)
					tids.Add(tid);
			}
			if (old)
			{
				int[] oldtids = db.GetOldTids();
				foreach(int tid in oldtids)
					tids.Add(tid);
			}
			if (future)
			{
				int[] futuretids = db.GetFutureTids();
				foreach(int tid in futuretids)
					tids.Add(tid);
			}
			
			Contest t;
			string str = "";
			foreach(int tid in tids)
			{
				if (!IsPostBack)
				{
					t = db.GetContest(tid);
					selContests.Items.Add(new ListItem(t.Name, tid.ToString()));
				}
				foreach (Problem p in db.GetProblems(tid))
					str += "new Problem(\"Задача " + p.ShortName + ". " + p.Name + "\"," + p.PID + "," + tid + "),";
			}
			db.Close();
			
			Page.RegisterArrayDeclaration("arr", (str.Length != 0) ? str.Substring(0, str.Length - 1) : str);
			Page.RegisterArrayDeclaration("hi",ctrlsl2hide);

			if(!Page.IsStartupScriptRegistered("Startup"))
				Page.RegisterStartupScript("Startup", "<script>Init()</script>");
		}

		public bool Now
		{
			set { now = value; }
		}

		public bool Future
		{
			set { future = value; }
		}

		public bool Old
		{
			set { old = value; }
		}

		public int PID
		{
			get
			{
				int ret;
				try
				{
					ret = int.Parse(pidField.Value);
				}
				catch
				{
					ret = -1;
				}
				Ne.Database.BaseDb db = Ne.Database.DbFactory.ConstructDatabase();
				if(db.CheckPid(ret))
				{
					db.Close();
					return ret;
				}
				else
				{
					db.Close();
					throw new NeJudgeInvalidParametersException("pid");
				}
			}
			set { pidField.Value = value.ToString(); }
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
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		public void Hide()
		{
			Table1.Visible = false;
			//errLiteral.Visible = true;
		}

		public void AddHidableControl(Control c)
		{
			if(ctrlsl2hide != "" && ctrlsl2hide != null)
				ctrlsl2hide += ",";
			ctrlsl2hide += "\"" + c.ClientID+"\"";
		}


		protected override object SaveViewState()
		{
			return ctrlsl2hide;
		}
		
		protected override void LoadViewState(object savedState)
		{
			ctrlsl2hide = savedState as string;
		}
	}
}