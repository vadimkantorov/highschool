using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ne.Database.Classes;
using Ne.Helpers;

using NeUser = Ne.Database.Classes.User;

namespace Ne.Judge
{
	[RequireContestID]
	[RequireEveryProblemID]
	[RequireUserID]
	[RequireOutcome]
	public partial class StatusPage : Page
	{
		SubmissionsFilter f;

		protected void Page_Load(object sender, EventArgs e)
		{
			if ( Page.User.IsInRole("Anonymous") )
				throw new NeJudgeSecurityException("User, Administrator, Judge");
			if ( !IsPostBack )
			{
				//Page.Response.AddHeader("Refresh", "90");
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				/*SubmissionsFilter*/ f = new SubmissionsFilter(0);
				if( rp.OutcomeDefined )
					f.Outcome = rp.Outcome;
				f.UserID = ( rp.UserIDDefined && User.IsInRole("Judge") ) ? rp.UserID : User.Identity.Name;
				
				if( rp.ContestIDDefined)
				{
					if (Contest.GetContest(rp.ContestID).Time == ContestTime.Forthcoming && !Page.User.IsInRole("Judge"))
						throw new NeJudgeSecurityException("Ќевозможно просмотреть submissions будущего соревновани€");

					f.ContestID = rp.ContestID;
					f.ProblemID = (rp.ProblemIDDefined) ? rp.ProblemID : 0;
					int page;
					int.TryParse(Request.QueryString["page"], out page);
					statusGV.PageIndex = ( page >= 1 ) ? page - 1 : 0;
				}
				else
					statusGV.Visible = false;
				
				filter.Filter = f;
				//Context.Items["filter"] = f;
			}
		}

		protected void statusGV_PageIndexChanging(object source, GridViewPageEventArgs e)
		{
			string redirect_url = "status.aspx";
			bool bg_flag = false;
			foreach ( string key in Page.Request.QueryString )
			{
				if ( key != "page" && key != string.Empty && key != null )
				{
					if ( !bg_flag )
					{
						redirect_url += "?";
						bg_flag = true;
					}
					else
					{
						redirect_url += "&";
					}
					redirect_url += String.Format("{0}={1}", key, Page.Request.QueryString[key]);
				}
			}
			redirect_url += "&page=" + (e.NewPageIndex + 1);
			Page.Response.Redirect(redirect_url);
		}

		protected void statusGV_DataBound(object sender, EventArgs e)
		{
			HtmlFunctions.BeautifyGridView(statusGV);
		}
		
		protected void statusGV_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if ( e.Row.RowType == DataControlRowType.DataRow )
			{
				DataRowView drv = (DataRowView)e.Row.DataItem;

				for ( int i = 0; i < drv.DataView.Table.Columns.Count; i++ )
					e.Row.Cells[i].Text = Server.HtmlDecode(e.Row.Cells[i].Text);
			}
		}
		protected void data_Selecting(object sender, ObjectDataSourceSelectingEventArgs e)
		{
			e.InputParameters["Filter"] = f;
		}
}
}