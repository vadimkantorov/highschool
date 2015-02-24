using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	[RequireContestId]
	//[RequireProblemId]
	[RequireUserId]
	public class status : UserControl
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
			this.statusGrid.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.statusGrid_PageIndexChanged);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		protected SubmissionsFilter filter;
		protected DataGrid statusGrid;

		private RequirementsProcessor _rp;
		private string result = "";
		private string problem = "";

		private void Page_Load(object sender, EventArgs e)
		{
			if(BaseDb.IsAnonymous(Page.User))
				throw new NeJudgeSecurityException("User, Administrator, Judge");
			if(!IsPostBack)
			{
				Page.Response.AddHeader("Refresh", "90");
				ArrayList sids = new ArrayList();
				int page = 0;
				#region параметры
				_rp = new RequirementsProcessor(this.GetType(), Context);
				_rp.ProcessRequirements();
				if ( Page.Request.QueryString["sname"] != null )
				{
					problem = Page.Request.QueryString["sname"];
				}
				if ( Page.Request.QueryString["result"] != null )
				{
					result = Page.Request.QueryString["result"];
				}
				try
				{
					page = int.Parse(Page.Request.QueryString["page"]);
				}
				catch
				{
					page = 0;
				}
				using(BaseDb db = DbFactory.ConstructDatabase())
				{
					if ( _rp.UidDefined )
					{
						if ( BaseDb.IsJudge(Page.User) )
							filter.UserID = _rp.UserID;
						else
							throw new NeJudgeSecurityException("Judge");
					}

					if ( _rp.TidDefined )
					{
						//filter.ContestID = _rp.ContestID;
						if (db.GetContest(_rp.ContestID).Future)
						{
							Hide("Невозможно просмотреть submissions будущего соревнования");
						}
						else
						{
							int pid = -1;
							if ( problem != "" )
							{
								pid = db.GetPidByShortName(_rp.ContestID, problem);
								if ( pid == -1 )
								{
									throw new NeJudgeInvalidParametersException("sname");
								}
								else
								{
									if ( !Page.IsPostBack ) filter.ProblemID = problem[0];
								}
							}
							if ( result != "" )
							{
								try
								{
									Result r = (Result)Enum.Parse
										(typeof(Result), result);
									if ( !Page.IsPostBack ) filter.Result = r;
								}
								catch ( ArgumentException )
								{
									throw new NeJudgeInvalidParametersException("result");
								}
							}
							#endregion
							foreach ( Submission s in db.GetSubmissions(_rp.ContestID, 
								_rp.UidDefined ? _rp.UserID : 0,
								( problem == "" ) ? 0 : pid, result) )
							{
								sids.Add(s.SID);
							}
							if (sids.Count == 0)
							{
								//Hide("В этом соревновании нет ни одного submission");
							}
							sids.Reverse();
							statusGrid.VirtualItemCount = sids.Count;
							if (sids.Count - page * statusGrid.PageSize < 0)
								page = sids.Count / statusGrid.PageSize;
							statusGrid.CurrentPageIndex = page;
							ArrayList arr = new ArrayList();
							int l;
							if (sids.Count - page*statusGrid.PageSize < statusGrid.PageSize)
								l = sids.Count;
							else
								l = page*statusGrid.PageSize + statusGrid.PageSize;
							for (int i = page*statusGrid.PageSize; i < l; i++)
								arr.Add(sids[i]);
							Bind(arr);
							HtmlFunctions.BeautifyDataGrid(statusGrid);
						}
					}
					else
						Hide(null);
				}
			}
		}

		private void Bind(ArrayList sids)
		{
			DataTable dt = new DataTable();
			DataRow dr;
			Submission s;
			BaseDb db = DbFactory.ConstructDatabase();
			Contest con = db.GetContest(_rp.ContestID);
			dt.Columns.Add("ID", typeof (int));
			dt.Columns.Add("Время");
			dt.Columns.Add("Команда");
			dt.Columns.Add("Задача");
			dt.Columns.Add("Язык");
			dt.Columns.Add("Статус");
			dt.Columns.Add("Тест №", typeof (uint));
			dt.Columns.Add("Время работы");
			dt.Columns.Add("Выделено памяти");
			foreach (int sid in sids)
			{
				dr = dt.NewRow();
				s = db.GetSubmission(sid);
				dr[0] = sid;
				dr[1] = HtmlFunctions.BeautifyTimeSpan(s.Time - con.Beginning, true);
				dr[2] = db.GetUser(s.UID).Fullname;
				dr[3] = String.Format("<a href='problem.aspx?pid={0}'>{1}</a>", s.PID, 
					db.GetProblemShortName(s.PID));
				dr[4] = s.SubmissionLanguage;
				string pattern = "<a href='viewdata.aspx?mode={0}&sid={1}'>{2}</a>";
				if (s.Result.Code == Result.CE)
					dr[5] = String.Format(pattern, "comp-report", sid, s.Result.ToHtmlString());
				else if (s.Result.Code == Result.FA)
					dr[5] = String.Format(pattern, "error-report", sid, s.Result.ToHtmlString());
				else
					dr[5] = s.Result.ToHtmlString();
				if (s.Result.TestNumber > 0)
					dr[6] = s.Result.TestNumber;
				if (s.Result.Code != Result.CE && s.Result.Code != Result.FA && s.Result.Code != Result.WAIT && s.Result.Code != Result.RU && s.Result.Code != Result.TLE)
				{
					dr[7] = Math.Round(s.Result.TimeWorked, 4) + " сек";
				}
				if (s.Result.Code != Result.CE && s.Result.Code != Result.FA && s.Result.Code != Result.WAIT && s.Result.Code != Result.RU && s.Result.Code != Result.MLE)
				{
					dr[8] = s.Result.MemoryUsed + " КБ";
				}
				dt.Rows.Add(dr);
			}
			db.Close();
			statusGrid.DataSource = dt;
			statusGrid.DataBind();
		}

		private void Hide(string mess)
		{
			statusGrid.Visible = false;
			/*if (mess != null)
				selcon.ErrorText = mess;*/
		}

		private void statusGrid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
		{
			string redirect_url = "status.aspx";
			bool bg_flag = false;
			foreach ( string key in Page.Request.QueryString )
			{
				if ( key != "page" && key != string.Empty && key != null )
				{
					if ( !bg_flag )
					{
						redirect_url +=	"?";
						bg_flag = true;
					}
					else
					{
						redirect_url += "&";
					}
					redirect_url += String.Format("{0}={1}", key, Page.Request.QueryString[key]);
				}
			}
			if (e.NewPageIndex > statusGrid.CurrentPageIndex)
				redirect_url += "&page=" + (++statusGrid.CurrentPageIndex);
			else
				redirect_url += "&page=" + (--statusGrid.CurrentPageIndex);
			Page.Response.Redirect(redirect_url);
		}
	}
}
