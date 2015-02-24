using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ne.Helpers;
using Ne.Database.Classes;
using Ne.ContestTypeHandlers;

namespace Ne.Judge
{
	[RequireSubmissionID]
	public partial class testlog : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			RequirementsProcessor rp = new RequirementsProcessor(GetType());
			rp.ProcessRequirements();

			if ( rp.SubmissionIDDefined )
			{
				Submission sm = Submission.GetSubmission(rp.SubmissionID);
				ITestLogManager itm =
					Factory.GetHandlerInstance(
					Contest.GetContest(Problem.GetProblem(sm.ProblemID).ContestID).Type
				).TestLogManager;
				
				TestLogGrid = itm.BuildTestLogGrid(sm);
				TestLogGrid.Visible = true;
			}
			else
				TestLogGrid.Visible = false;
		}
	}
}
