using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.ContestTypeHandlers;

namespace ICPCHandler
{
	public class ICPCTestLogManager : ITestLogManager
	{
		OutcomeManager oMan;

		string FormatResult(RunResult rr, CheckStatus cs)
		{
			if ( rr.Status != RunStatus.Ok )
			{
				switch ( rr.Status )
				{
					case RunStatus.TimeLimit:
						return oMan.GetPrintableValue(ICPCOutcomeManager.TimeLimit);
					case RunStatus.MemoryLimit:
						return oMan.GetPrintableValue(ICPCOutcomeManager.MemoryLimit);
					case RunStatus.OutputLimit:
						return oMan.GetPrintableValue(ICPCOutcomeManager.OutputLimit);
					case RunStatus.RuntimeError:
						return oMan.GetPrintableValue(ICPCOutcomeManager.RuntimeError);
					case RunStatus.SecurityViolation:
						return oMan.GetPrintableValue(ICPCOutcomeManager.SecurityViolation);
					case RunStatus.Failure:
						return oMan.GetPrintableValue(OutcomeManager.TestingFailure);
				}
			}
			else
			{
				switch ( cs )
				{ 
					case CheckStatus.Ok:
						return oMan.GetPrintableValue(ICPCOutcomeManager.Accepted);
					case CheckStatus.WrongAnswer:
						return oMan.GetPrintableValue(ICPCOutcomeManager.WrongAnswer);
					case CheckStatus.PresentationError:
						return oMan.GetPrintableValue(ICPCOutcomeManager.PresentationError);
					case CheckStatus.NotChecked:
						return oMan.GetPrintableValue(ICPCOutcomeManager.TestingFailure);
				}
			}
			return "";
		}

		public DataGrid BuildTestLogGrid(Submission subm)
		{
			Contest con = Contest.GetContest(Problem.GetProblem(subm.ProblemID).ContestID);
			oMan = Factory.GetHandlerInstance(con.Type).OutcomeManager;

			DataTable dt = new DataTable();
			
			dt.Columns.Add("№ теста");
			dt.Columns.Add("Результат");
			dt.Columns.Add("Комментарий чекера");
			dt.Columns.Add("Время работы");
			dt.Columns.Add("Память");

			subm.LoadLog();
			for ( int i = 0; i < subm.Log.TestCollection.Count; ++i )
			{
				DataRow dr = dt.NewRow();
				dr[0] = i;
				dr[1] = FormatResult(subm.Log.TestCollection[i].RunResult,
					subm.Log.TestCollection[i].CheckStatus);
				dr[2] = subm.Log.TestCollection[i].CheckerComment;
				dr[3] = subm.Log.TestCollection[i].RunResult.TimeWorked;
				dr[4] = subm.Log.TestCollection[i].RunResult.MemoryUsed;
				dt.Rows.Add(dr);
			}

			DataGrid ret = new DataGrid();

			ret.Width = new Unit(100.0, UnitType.Percentage);
			ret.CellPadding = 5;
			ret.HeaderStyle.CssClass = "gridHeader";
			ret.DataSource = dt;
			ret.DataBind();

			return ret;
		}
	}
}
