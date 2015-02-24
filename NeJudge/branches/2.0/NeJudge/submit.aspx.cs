using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.Helpers;
using Ne.Tester;
using Ne.ContestTypeHandlers;

using NeUser = Ne.Database.Classes.User;

namespace Ne.Judge
{
	[RequireProblemID]
	public partial class SubmitPage : Page
	{
		private void Bind()
		{
			languageDropDownList.DataSource = Language.GetLanguages();
			languageDropDownList.DataBind();
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !User.IsInRole("User") )
				throw new NeJudgeSecurityException("User");

			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				Bind();
				if ( rp.ProblemIDDefined )
					selprob.ProblemID = rp.ProblemID;
			}
		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			Problem p = Problem.GetProblem(selprob.ProblemID);
			if ( Contest.GetContest(p.ContestID).Time == ContestTime.Current )
			{
				if ( Page.IsValid )
				{
					Submission s = new Submission(p.ID, (sourceTextBox.Text.Trim() != "") ? sourceTextBox.Text.Trim():
						Encoding.UTF8.GetString(file.FileBytes),User.Identity.Name, 
						languageDropDownList.SelectedValue, DateTime.Now);
					s.Store();
					try
					{
						INeTester tst = (INeTester) Activator.GetObject(typeof (INeTester), 
							"tcp://localhost:8008/NeTester/Tester.rem");
						tst.EnqueueSubmission(s.ID);
					}
					catch ( Exception ex )
					{
						//TODO: testerFail.Message = "Проверка решения временно невозможна. Пожалуйста, повторите попытку позднее";
						s.Outcome = OutcomeManager.CannotJudge;
						s.Store();
					}
					Response.Redirect("status.aspx?contestID=" + p.ContestID, false);
				}
			}
			else
				throw new NeJudgeInvalidParametersException("problemID");
			//"Соревнование либо закончилась, либо ещё не начиналось.";
		}

		protected void sourceV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			args.IsValid = (sourceTextBox.Text.Trim() == "" ^ !file.HasFile);
		}
	}
}