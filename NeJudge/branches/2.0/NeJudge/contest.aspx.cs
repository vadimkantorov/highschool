using System;
using System.Data;
using System.Web.UI;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	[RequireContestID]
	public partial class ContestPage : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();
				if( rp.ContestIDDefined )
				{
					if( Contest.GetContest(rp.ContestID).Time == ContestTime.Forthcoming && !Page.User.IsInRole("Administrator") )
						throw new NeJudgeInvalidParametersException("contestID");
					//Hide("Ёто соревнование начнетс€ через " + HtmlFunctions.BeautifyTimeSpan(db.GetContest(tid).Beginning - DateTime.Now, false) + ". —ейчас задачи посмотреть нельз€.");
					
					#region —сылочки (Visible в зависимости от роли)

					statHL.NavigateUrl = UrlRenderer.RenderStatusUrl(rp.ContestID);
					quesHL.NavigateUrl = UrlRenderer.RenderQuestionsUrl(rp.ContestID);
					monHL.NavigateUrl = UrlRenderer.RenderMonitorUrl(rp.ContestID);
					editHL.NavigateUrl = UrlRenderer.RenderEditContestUrl(rp.ContestID);
					printHL.NavigateUrl = UrlRenderer.RenderPrintContestUrl(rp.ContestID);

					if( Page.User.IsInRole("Anonymous") )
					{
						statHL.Visible = false;
						quesHL.Visible = false;
					}
					if( Page.User.IsInRole("Administrator") )
						editHL.Visible = true;

					#endregion
				}
				else
				{
					monHL.Visible = false;
					statHL.Visible = false;
					quesHL.Visible = false;
					printHL.Visible = false;
				}
			}
		}
	}
}