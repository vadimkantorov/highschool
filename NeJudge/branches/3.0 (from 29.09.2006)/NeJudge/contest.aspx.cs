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
				ContestIDValidator cv = new ContestIDValidator();
				cv.Process();

				if( cv.ParameterDefined )
				{
					if( Contest.GetContest(cv.ValidatedValue).Time == ContestTime.Forthcoming && !Page.User.IsInRole("Administrator") )
						throw new NeJudgeInvalidParametersException("contestID");
					//Hide("Ёто соревнование начнетс€ через " + HtmlFunctions.BeautifyTimeSpan(db.GetContest(tid).Beginning - DateTime.Now, false) + ". —ейчас задачи посмотреть нельз€.");
					
					#region —сылочки (Visible в зависимости от роли)

					statHL.NavigateUrl = UrlRenderer.RenderStatusUrl(cv.ValidatedValue);
					quesHL.NavigateUrl = UrlRenderer.RenderQuestionsUrl(cv.ValidatedValue);
					monHL.NavigateUrl = UrlRenderer.RenderMonitorUrl(cv.ValidatedValue);
					editHL.NavigateUrl = UrlRenderer.RenderEditContestUrl(cv.ValidatedValue);
					printHL.NavigateUrl = UrlRenderer.RenderPrintContestUrl(cv.ValidatedValue);

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