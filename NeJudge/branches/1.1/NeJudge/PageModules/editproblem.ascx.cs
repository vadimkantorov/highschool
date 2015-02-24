using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for editproblem.
	/// </summary>
	public class editproblem : UserControl
	{
		protected TextBox problemNameTextBox;
		protected TextBox inputFormatTextBox;
		protected HtmlGenericControl outerror;
		protected TextBox outputFormatTextBox;
		protected TextBox inputSampleTextbox;
		protected TextBox outputSampleTextbox;
		protected Button finishButton;

		private int tid, pid;
		protected TextBox authorTextBox;
		protected HtmlTable Table1;
		protected TextBox problemTextTextBox;

		private void Hide(string mess)
		{
			outerror.InnerHtml = "<FONT COLOR=\"Red\"><b>" + mess + "</b></FONT>";
			Table1.Visible = false;
			finishButton.Visible = false;
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (!BaseDb.IsAdmin(Page.User))
				throw new NeJudgeSecurityException("Administrator");
			if (!IsPostBack)
			{
				try
				{
					pid = int.Parse(Request.QueryString["pid"]);
				}
				catch
				{
					pid = -1;
				}
				try
				{
					tid = int.Parse(Request.QueryString["tid"]);
				}
				catch
				{
					tid = -1;
				}
				BaseDb db = DbFactory.ConstructDatabase();
				if (db.CheckPid(pid))
				{
					tid = db.GetTid(pid);
					Problem p = db.GetProblem(pid);
					problemNameTextBox.Text = Server.HtmlDecode(p.Name);
					problemTextTextBox.Text = Server.HtmlDecode(p.Text);
					inputFormatTextBox.Text = Server.HtmlDecode(p.InputFormat);
					outputFormatTextBox.Text = Server.HtmlDecode(p.OutputFormat);
					inputSampleTextbox.Text = Server.HtmlDecode(p.InputSample);
					outputSampleTextbox.Text = Server.HtmlDecode(p.OutputSample);
					authorTextBox.Text = Server.HtmlDecode(p.Author);
				}
				else if (db.CheckTid(tid))
				{
					pid = -1;
					finishButton.Text = "Добавить задачу";
					if (db.GetContest(tid).Old)
						throw new NeJudgeInvalidParametersException("tid");
				}
				else
				{
					throw new NeJudgeInvalidParametersException("tid");
				}
				db.Close();
			}
		}

		protected override object SaveViewState()
		{
			return new int[] {pid, tid};
		}

		protected override void LoadViewState(object st)
		{
			pid = ((int[]) st)[0];
			tid = ((int[]) st)[1];
		}

		private int InsertProblem(Problem p)
		{
			BaseDb db = DbFactory.ConstructDatabase();
			int ret = -1;
			try
			{
				ret = db.AddProblem(p);
			}
			catch (SystemException ex)
			{
				outerror.InnerHtml = "<b>An exception of type " + ex.GetType() +
					" was encountered while attempting to roll back the transaction. Message:</b> <br/><br/> <i>" + ex.Message + "</i>";
			}
			db.Close();
			return ret;
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
			this.finishButton.Click += new EventHandler(this.finishButton_Click);
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion

		private void finishButton_Click(object sender, EventArgs e)
		{
			if (pid == -1)
			{
				Problem p = new Problem(Server.HtmlEncode(problemNameTextBox.Text),
				                        Server.HtmlEncode(problemTextTextBox.Text),
				                        Server.HtmlEncode(inputFormatTextBox.Text),
				                        Server.HtmlEncode(outputFormatTextBox.Text),
				                        Server.HtmlEncode(inputSampleTextbox.Text),
				                        Server.HtmlEncode(outputSampleTextbox.Text), null,
				                        Server.HtmlEncode(authorTextBox.Text), -1, tid);
				//TODO:SharpDfTest.DfTest.CreateProblem
				pid = InsertProblem(p);
			}
			else
			{
				Problem p = new Problem(Server.HtmlEncode(problemNameTextBox.Text),
				                        Server.HtmlEncode(problemTextTextBox.Text),
				                        Server.HtmlEncode(inputFormatTextBox.Text),
				                        Server.HtmlEncode(outputFormatTextBox.Text),
				                        Server.HtmlEncode(inputSampleTextbox.Text),
				                        Server.HtmlEncode(outputSampleTextbox.Text), null,
				                        Server.HtmlEncode(authorTextBox.Text), pid, tid);
				BaseDb db = DbFactory.ConstructDatabase();
				//TODO:SharpDfTest.DfTest.CreateProblem
				db.UpdateProblem(p);
				db.Close();
			}
			string ret;
			if (Request.QueryString["ret"] != null)
				ret = "~/problem.aspx?pid=" + pid;
			else
				ret = "~/editcontest.aspx?tid=" + tid;
			Response.Redirect(ret);
		}
	}
}