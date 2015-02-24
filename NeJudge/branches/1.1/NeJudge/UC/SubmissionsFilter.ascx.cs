using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for SubmissionsFilter.
	/// </summary>
	public class SubmissionsFilter : UserControl
	{
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
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
			this.filterButton.Click += new EventHandler(this.filterButton_Click);
			this.Init += new EventHandler(this.Page_Init);

		}
		#endregion

		protected DropDownList userDropDownList;
		protected DropDownList resultDropDownList;
		protected HtmlTableCell userTd;
		protected Button filterButton;
		protected DropDownList contestDropDownList;
		protected DropDownList snameDropDownList;
		protected Literal noSuchProb;

		private string _url = "";

		public string Url
		{
			get
			{
				return _url;
			}
			set
			{
				_url = value;
			}
		}

		private void Page_Init(object sender, EventArgs e)
		{
			noSuchProb.Visible = false;
			if ( !Page.IsPostBack )
			{
				BaseDb db = DbFactory.ConstructDatabase();
				if ( !BaseDb.IsJudge(Page.User) )
				{
					userTd.Visible = false;
				}
				else
				{
					userDropDownList.Items.Add(new ListItem("--Ыўсющ--", "0"));
					foreach ( User u in db.GetUsers() )
					{
						userDropDownList.Items.Add(new ListItem(u.Fullname, 
							db.GetUid(u.Username).ToString()));
					}
				}

				snameDropDownList.Items.Add(new ListItem("--Ыўсрџ--", "ALL"));
				for ( char c = 'A'; c <= 'Z'; c++ )
				{
					snameDropDownList.Items.Add(new ListItem(c.ToString(), c.ToString()));
				}

				int[] old_tids = db.GetOldTids();
				int[] now_tids = db.GetNowTids();
				Contest t = null;
				for ( int i = 0; i < old_tids.Length; i++ )
				{
					t = db.GetContest(old_tids[i]);
					contestDropDownList.Items.Add(new ListItem(t.Name, old_tids[i].ToString()));
				}
				for ( int i = 0; i < now_tids.Length; i++ )
				{
					t = db.GetContest(now_tids[i]);
					contestDropDownList.Items.Add(new ListItem(t.Name, now_tids[i].ToString()));
				}
				
				resultDropDownList.Items.Add(new ListItem("--Ыўсющ--", "ALL"));
				foreach ( Result r in Enum.GetValues(typeof(Result)) )
				{
					SubmissionResult res = new SubmissionResult(r);
					resultDropDownList.Items.Add(new ListItem(res.ToString(), r.ToString()));
				}
				userDropDownList.SelectedIndex = resultDropDownList.SelectedIndex = 
					contestDropDownList.SelectedIndex = snameDropDownList.SelectedIndex = 0;
			}
		}

		private void filterButton_Click(object sender, EventArgs e)
		{
			int tid = int.Parse(contestDropDownList.SelectedValue);
			string redirect_url = String.Format("{0}?tid={1}", _url, tid);
			if ( BaseDb.IsJudge(Page.User) )
			{
				int uid = int.Parse(userDropDownList.SelectedValue);
				if ( uid != 0 )
				{
					redirect_url += String.Format("&uid={0}", uid);
				}
			}
			if ( snameDropDownList.SelectedValue != "ALL" )
			{
				BaseDb db = DbFactory.ConstructDatabase();
				if ( db.GetPidByShortName(tid, snameDropDownList.SelectedValue) == -1 )
				{
					noSuchProb.Visible = true;
					return;
				}
				redirect_url += String.Format("&sname={0}", snameDropDownList.SelectedValue);
			}
			if ( resultDropDownList.SelectedValue != "ALL" )
			{
				redirect_url += String.Format("&result={0}", resultDropDownList.SelectedValue);
			}
			Page.Response.Redirect(redirect_url);
		}

		public int ContestID
		{
			set
			{
				contestDropDownList.SelectedValue = value.ToString();
			}
		}

		public int UserID
		{
			set
			{
				userDropDownList.SelectedValue = value.ToString();
			}
		}

		public Result Result
		{
			set
			{
				resultDropDownList.SelectedValue = value.ToString();
			}
		}

		public char ProblemID
		{
			set
			{
				snameDropDownList.SelectedValue = value.ToString();
			}
		}
	}
}
