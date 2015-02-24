using System;
using System.Web.UI;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	[RequireProblemID]
	public partial class ProblemPage : Page
	{
		private void InitLinks(int pid)
		{
			submitHL.NavigateUrl = UrlRenderer.RenderSubmitUrl(pid);
			askHL.NavigateUrl = UrlRenderer.RenderAskUrl(pid);
			printProblemHL.NavigateUrl = UrlRenderer.RenderPrintProblemUrl(pid);
			questionsHL.NavigateUrl = UrlRenderer.RenderQuestionsUrl(Problem.GetProblem(pid).ContestID);
			editHL.NavigateUrl = UrlRenderer.RenderEditProblemUrl(pid, 'p');
		}

		private void HideLinks()
		{
			submitHL.Visible = questionsHL.Visible = askHL.Visible = false;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				if ( rp.ProblemIDDefined )
				{
					selprob.ProblemID = rp.ProblemID;

					Problem p = Problem.GetProblem(rp.ProblemID);
					Contest t = Contest.GetContest(p.ContestID);

					if (!Page.User.IsInRole("Administrator") && t.Time == ContestTime.Forthcoming)
						throw new NeJudgeInvalidParametersException("problemID");
					//"������������, ������ � �������� �� ������ �����������, ��� �� ��������. ���������� �����."
					problemView.ProblemID = p.ID;
					#region ����� �� ��������

					InitLinks(rp.ProblemID);
					if ( Page.User.IsInRole("Administrator") )
					{
						editHL.Visible = true;
						HideLinks();
					}
					else
					{
						if (Page.User.IsInRole("Anonymous") || t.Time == ContestTime.Past)
							HideLinks();
					}

					#endregion
				}
				else
				{
					problemTable.Visible = false;
				}
			}
		}

		public void goButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(UrlRenderer.RenderProblemUrl(selprob.ProblemID));
		}
	}
}