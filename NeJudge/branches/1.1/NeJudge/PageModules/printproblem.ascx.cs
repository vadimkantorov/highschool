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
	/// Summary description for printproblem.
	/// </summary>
	public class printproblem : UserControl
	{
		protected Literal nameLiteral;
		protected Literal textLiteral;
		protected Literal infoLiteral;
		protected Literal outfoLiteral;
		protected Literal inexLiteral;
		protected Literal outexLiteral;
		protected HtmlTable Table1;
		protected Literal tlLiteral;
		protected Literal mlLiteral;
		protected Literal olLiteral;
		//protected HtmlGenericControl constraintsDiv;
		protected Literal authorLiteral;
		private int pid;

		public int PID
		{
			get { return pid; }
			set { pid = value; }
		}

		private void Hide(string mess)
		{
			nameLiteral.Text = mess;
			//constraintsDiv.Visible = false;
			textLiteral.Visible = false;
			infoLiteral.Visible = false;
			outfoLiteral.Visible = false;
			inexLiteral.Visible = false;
			outexLiteral.Visible = false;
			authorLiteral.Visible = false;
		}

		private void Page_Load(object sender, EventArgs e)
		{
			BaseDb db = DbFactory.ConstructDatabase();
			if (db.CheckPid(pid))
			{
				Problem p = db.GetProblem(pid);
				if (db.GetContest(p.TID).Future)
				{
					Hide("Соревнование,	задачу с которого вы хотите	просмотреть, ещё не	началось. Попробуйте позже.");
				}
				else
				{
					nameLiteral.Text = "<h1> Задача	" + p.ShortName +
						" (	#" + pid + " ).	" + p.Name + "</h1>";
					Limits l = DfTest.GetLimits(pid.ToString());
					tlLiteral.Text += l.Time + "	секунды";
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
						authorLiteral.Text += "<hr>";
					}
				}
			}
			else
			{
				throw new NeJudgeInvalidParametersException("pid");
			}
			db.Close();
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
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion
	}
}