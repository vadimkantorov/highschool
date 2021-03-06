using System;
using System.Web.UI;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	[RequireContestID]
	public partial class PrintContestPage : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				if( rp.ContestIDDefined )
				{
					selcon.Visible = false;
					Contest t = Contest.GetContest(rp.ContestID);
					if( !Page.User.IsInRole("Administrator") && t.Time == ContestTime.Forthcoming )
						throw new NeJudgeInvalidParametersException("problemID");
					//"������������, ������� �� ������ �����������, ��� �� ��������. ���������� �����."
					problemsPH.Controls.Add(new LiteralControl("<div align='center'><h1 style='color:#418ade;'>" + t.Name + "</h1></div>"));
					foreach( Problem p in Problem.GetProblems(t.ID) )
					{
						problemsPH.Controls.Add(new LiteralControl("<hr/>"));
						ProblemView pv = (ProblemView)LoadControl("~/UC/problemview.ascx");
						pv.ProblemID = p.ID;
						problemsPH.Controls.Add(pv);
						problemsPH.Controls.Add(new LiteralControl("<br/>"));
					}
				}
				else
					problemsPH.Visible = false;
			}
		}
	}
}