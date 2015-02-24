using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	[RequireProblemID]
	public partial class AskPage : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !User.IsInRole("Judge") && !User.IsInRole("User") )
				throw new NeJudgeSecurityException("User");

			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();
				if ( rp.ProblemIDDefined )
					selprob.ProblemID = rp.ProblemID;
			}
		}

		protected void sendMessageB_Click(object sender, EventArgs e)
		{
			int pid = selprob.ProblemID;
			Contest c = Contest.GetContest(Problem.GetProblem(pid).ContestID);
			if ( c.Time == ContestTime.Current )
			{
				if ( Page.IsValid )
				{
					Message m = null;
					if ( User.IsInRole("User") )
						m = new Message(pid, User.Identity.Name, DateTime.Now, MessageType.Question, messageTB.Text, "");
					else
						m = new Message(pid, User.Identity.Name, DateTime.Now, MessageType.Clarification, "", messageTB.Text);
					m.Store();
					Response.Redirect(UrlRenderer.RenderQuestionsUrl(Problem.GetProblem(pid).ContestID));
				}
			}
			else
				throw new NeJudgeInvalidParametersException("pid"); //"—оревнование либо закончилось, либо ещЄ не начиналась."
		}

		protected void messageV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if ( messageTB.Text.Length < 10 )
				args.IsValid = false;
			else if ( messageTB.Text.Length > 60 )
			{
				messageV.ErrorMessage = "¬ведите текст, длиной не больше 60 символов";
				args.IsValid = false;
			}
		}
	}
}