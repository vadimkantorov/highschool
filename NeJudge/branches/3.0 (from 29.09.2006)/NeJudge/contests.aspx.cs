using System;
using System.Data;
using System.Web.UI;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	public partial class ContestsPage : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ID");
			dt.Columns.Add("Name");
			dt.Columns.Add("Monitor");
			dt.Columns.Add("Beginning", typeof(DateTime));
			dt.Columns.Add("Ending", typeof(DateTime));
			dt.Columns.Add("Status");

			Contest[] cons = Contest.GetContests(ContestTime.Current |
												 ContestTime.Forthcoming | ContestTime.Past);

			foreach( Contest cnt in cons )
			{
				DataRow dr = dt.NewRow();
				dr[0] = cnt.ID;
				if( cnt.Time == ContestTime.Forthcoming && !Page.User.IsInRole("Administrator") )
					dr[1] = cnt.Name;
				else
					dr[1] = UrlRenderer.RenderContestUrl(cnt);
				if( cnt.Time == ContestTime.Forthcoming && !Page.User.IsInRole("Judge") )
					dr[2] = "Недоступен";
				else
					dr[2] = UrlRenderer.RenderMonitorUrl(cnt);
				dr[3] = cnt.Beginning;
				dr[4] = cnt.Ending;
				switch( cnt.Time )
				{
					case ContestTime.Current:
						dr[5] = String.Format("Идет (конец через {0})",
											  TimeUtils.BeautifyTimeSpan(cnt.Ending - DateTime.Now, false));
						break;
					case ContestTime.Forthcoming:
						dr[5] = String.Format("Еще не начиналось (начало через {0})",
											  TimeUtils.BeautifyTimeSpan(cnt.Beginning - DateTime.Now, false));
						break;
					case ContestTime.Past:
						dr[5] = "Закончилось";
						break;
				}
				dt.Rows.Add(dr);
			}

			contestsGV.DataSource = dt;
			contestsGV.DataBind();
		}
	}
}