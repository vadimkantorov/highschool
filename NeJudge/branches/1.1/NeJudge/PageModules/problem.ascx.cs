using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;
using SharpDfTest;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for problem.
	/// </summary>
	[RequireProblemId]
	public class problem : UserControl
	{
		protected HyperLink Hyperlink1;
		protected HyperLink Hyperlink2;
		protected HyperLink Hyperlink3;
		protected HyperLink Hyperlink4;

		protected Literal nameLiteral;
		protected Literal textLiteral;
		protected Literal infoLiteral;
		protected Literal outfoLiteral;
		protected Literal inexLiteral;
		protected Literal outexLiteral;
		protected Literal authorLiteral;
		protected Literal tlLiteral;
		protected Literal mlLiteral;
		protected Literal olLiteral;

		protected Button goButton;
		protected HtmlTable Table2;
		protected HtmlTable Table1;


		protected SelectProblem selprob;

		private void Hide()
		{
			Table2.Visible = false;
		}

		private void FillLiterals(Problem p)
		{
			nameLiteral.Text = "<h1> Задача " + p.ShortName +
				" ( #" + p.PID + " ). " + p.Name + "</h1>";
			Limits l = DfTest.GetLimits(p.PID.ToString());
			tlLiteral.Text += l.Time + " секунды";
			mlLiteral.Text += l.Memory + " КБ";
			olLiteral.Text += l.Output + " байт";
			if (p.Text == "")
			{
				textLiteral.Visible = false;
			}
			else
			{
				textLiteral.Text += p.Text;
			}
			if (p.InputFormat == "")
			{
				infoLiteral.Visible = false;
			}
			else
			{
				infoLiteral.Text += p.InputFormat;
			}
			if (p.OutputFormat == "")
			{
				outfoLiteral.Visible = false;
			}
			else
			{
				outfoLiteral.Text += p.OutputFormat;
			}
			string str = "";
			if (p.InputSample == "")
			{
				inexLiteral.Visible = false;
			}
			else
			{
				inexLiteral.Text += "<code>";
				StringReader str_rdr = new StringReader(p.InputSample);
				while ((str = str_rdr.ReadLine()) != null)
				{
					inexLiteral.Text += str + "<br>";
				}
				inexLiteral.Text += "</code>";
			}
			if (p.OutputSample == "")
			{
				outexLiteral.Visible = false;
			}
			else
			{
				outexLiteral.Text += "<code>";
				StringReader str_rdr2 = new StringReader(p.OutputSample);
				while ((str = str_rdr2.ReadLine()) != null)
				{
					outexLiteral.Text += str + "<br>";
				}
				outexLiteral.Text += "</code>";
			}
			if (p.Author == "")
			{
				authorLiteral.Visible = false;
			}
			else
			{
				authorLiteral.Text += p.Author;
			}
		}

		private void InitLinks(int pid)
		{
			Hyperlink1.NavigateUrl += pid;
			Hyperlink2.NavigateUrl += pid;
			Hyperlink3.NavigateUrl += pid;
			Hyperlink4.NavigateUrl += pid;
		}
		private void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();
				
				selprob.AddHidableControl(goButton);
				
				if ( rp.PidDefined )
				{
					selprob.PID = rp.ProblemID;

					BaseDb db = DbFactory.ConstructDatabase();
					Problem p = db.GetProblem(rp.ProblemID);
					Contest t = db.GetContest(p.TID);
					db.Close();
					
					if(!BaseDb.IsAdmin(Page.User) && t.Future)
						throw new NeJudgeInvalidParametersException("pid");//"Соревнование, задачу с которого вы хотите просмотреть, ещё не началось. Попробуйте позже."
					
					#region Возня со ссылками
					InitLinks(rp.ProblemID);
					if (BaseDb.IsAdmin(Page.User))
					{
						Hyperlink2.Text = "Редактировать";
						Hyperlink2.NavigateUrl = "../editproblem.aspx?pid=" + rp.ProblemID + "&ret=p";

						Hyperlink3.Visible = Hyperlink4.Visible = false;
					}
					else
					{
						if (BaseDb.IsAnonymous(Page.User) || t.Old)
							Hyperlink2.Visible = Hyperlink3.Visible = Hyperlink4.Visible = false;
					}
					#endregion
					
					FillLiterals(p);
				}
				else
				{
					Hide();
				}
			}
		}

		#region Web Form Designer generated code

		protected override void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.goButton.Click += new EventHandler(this.goButton_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void goButton_Click(object sender, EventArgs e)
		{
			Response.Redirect("problem.aspx?pid=" + selprob.PID);
		}
	}
}