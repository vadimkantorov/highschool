using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Collections.Generic;

using Ne.Database.Classes;
using Ne.Helpers;
namespace Ne.Judge
{
	[RequireMode("create","RequireContestId")]
	[RequireMode("edit","RequireProblemId")]
	public partial class EditProblemPage : Page
	{
		Problem problem;
		protected void Page_Load(object sender, EventArgs e)
		{
			/*if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();
				
				if( rp.ModeDefined )
				{
					if( rp.Mode == "create" )
					{
						if ((Contest.GetContest(rp.ContestID).Time & (ContestTime.Past | ContestTime.Current)) != 0)
							throw new NeJudgeInvalidParametersException("contestID", "Нельзя создать задачу в текущем или прошедшем соревновании");

						selprob.ContestID = rp.ContestID;
						problem = new Problem();
						problem.ContestID = rp.ContestID;
					}
					else if( rp.Mode == "edit" )
					{
						selprob.ProblemID = rp.ProblemID;
						problem = Problem.GetProblem(rp.ProblemID);
					}
					problem.LoadStatement();

					mv.SetActiveView(contentsParams);
				}
				else
				{
					mv.Visible = false;
					menu.Visible = false;
					saveB.Visible = false;
				}
			}*/
		}

		#region Managing ViewState
		protected override object SaveViewState()
		{
			return new Pair(base.SaveViewState(), problem);
		}

		protected override void LoadViewState(object s)
		{
			Pair t = (Pair)s;
			base.LoadViewState(t.First);
			problem = (Problem)t.Second;
		}
		#endregion

		protected void saveB_Click(object sender, EventArgs e)
		{
			if( mv.ActiveViewIndex == 0 )
				contentsParams_Deactivate(null, null);
			else
				testsParams_Deactivate(null,null);
			problem.Store();
		}

		protected void menu_MenuItemClick(object sender, System.Web.UI.WebControls.MenuEventArgs e)
		{
			mv.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
		}

		protected void goButton_Click(object sender, EventArgs e)
		{
			if( selprob.ProblemID != 0 )
				Response.Redirect("~/editproblem2.aspx?mode=edit&problemID=" + selprob.ProblemID);
			else
				Response.Redirect("~/editproblem2.aspx?mode=create&contestID=" + selprob.ContestID);
		}

		#region Contents Params
		protected void contentsParams_Activate(object sender, EventArgs e)
		{
			nameTB.Text = problem.Name;
			textTB.Text = problem.Statement.Text;
			inputFormatTB.Text = problem.Statement.OutputFormat;
			outputFormatTB.Text = problem.Statement.OutputFormat;
			inputSampleTB.Text = problem.Statement.InputSample;
			outputSampleTB.Text = problem.Statement.OutputSample;
			authorTB.Text = problem.Statement.Author;
			Bind();
		}
		protected void contentsParams_Deactivate(object sender, EventArgs e)
		{
			ProblemStatement ps = new ProblemStatement();
			ps.Author = authorTB.Text;
			ps.InputFormat = inputFormatTB.Text;
			ps.OutputFormat = outputFormatTB.Text;
			ps.InputSample = inputSampleTB.Text;
			ps.OutputSample = outputSampleTB.Text;
			ps.Text = textTB.Text;
			
			problem.Statement = ps;
			problem.Name = nameTB.Text;
			problem.Store();
		}
		#region Hints
		void Bind()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Подсказка");
			foreach(string hint in problem.Statement.Hints)
			{
				DataRow dr = dt.NewRow();
				dr["Подсказка"] = hint;
				dt.Rows.Add(dr);
			}
			hintsGV.DataSource = dt;// problem.Statement.Hints;
			hintsGV.DataBind();
		}

		protected void hintsGV_RowEditing(object sender, GridViewEditEventArgs e)
		{
			hintsGV.EditIndex = e.NewEditIndex;

			Bind();
		}

		protected void hintsGV_RowDeleting(object sender, GridViewDeleteEventArgs e)
		{
			problem.Statement.Hints.RemoveAt(e.RowIndex);

			Bind();
		}

		protected void hintsGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			problem.Statement.Hints[e.RowIndex] = ( (TextBox)hintsGV.Rows[e.RowIndex].Cells[0].Controls[0] ).Text;
			hintsGV.EditIndex = -1;

			Bind();
		}

		protected void hintsGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			hintsGV.EditIndex = -1;

			Bind();
		}

		protected void addHintB_Click(object sender, EventArgs e)
		{
			problem.Statement.Hints.Add(newHintTB.Text);
			newHintTB.Text = "";

			Bind();
		}
		#endregion
		#endregion		
		#region Test Params
		protected void testsParams_Activate(object sender, EventArgs e)
		{

		}
		protected void testsParams_Deactivate(object sender, EventArgs e)
		{

		}
		#endregion
	}
}