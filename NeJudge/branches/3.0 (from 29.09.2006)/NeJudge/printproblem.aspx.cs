using System;
using System.Web.UI;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	public partial class PrintProblemPage : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				ProblemIDValidator pv = new ProblemIDValidator();
				pv.Process();

				if( pv.ParameterDefined )
				{
					selprob.Visible = false;
					goButton.Visible = false;
					Problem p = Problem.GetProblem(pv.ValidatedValue);
					Contest t = Contest.GetContest(p.ContestID);

					if( !Page.User.IsInRole("Administrator") && t.Time == ContestTime.Forthcoming )
						throw new NeJudgeInvalidParametersException("problemID");
					//"Соревнование, задачу с которого вы хотите просмотреть, ещё не началось. Попробуйте позже."
					problemView.ProblemID = p.ID;
				}
				else
					problemView.Visible = false;
			}
		}

		public void goButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(UrlRenderer.RenderProblemUrl(selprob.ProblemID));
		}
	}
}