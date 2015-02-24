using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	[RequireProblemId]
	public class ask : UserControl
	{
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
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustomValidator1_ServerValidate);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		protected TextBox qTextBox;
		protected Button Button1;
		protected System.Web.UI.WebControls.CustomValidator CustomValidator1;
		protected System.Web.UI.HtmlControls.HtmlTable hidabletable;
		protected SelectProblem selprob;

		private void Page_Load(object sender, EventArgs e)
		{
			if (!BaseDb.IsUser(Page.User))
				throw new NeJudgeSecurityException("User");

			if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();
				selprob.AddHidableControl(hidabletable);
				if ( rp.PidDefined )
					selprob.PID = rp.ProblemID;
			}
		}

		private void Button1_Click(object sender, EventArgs e)
		{
			int pid = selprob.PID;
			using(BaseDb db = DbFactory.ConstructDatabase())
			{
				int tid = db.GetTid(pid);
				if (db.GetContest(tid).Now)
				{
					if(Page.IsValid)
					{
						db.AddQuestion(pid, db.GetUid(Page.User.Identity.Name), tid, qTextBox.Text);
						Response.Redirect("~/questions.aspx?tid=" + tid,false);
					}
				}
				else
				{
					throw new NeJudgeInvalidParametersException("pid"); //"—оревнование либо закончилось, либо ещЄ не начиналась."
				}
			}
		}

		private void CustomValidator1_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			if(qTextBox.Text.Length < 10)
				args.IsValid = false;
			else if(qTextBox.Text.Length > 60)
			{
				CustomValidator1.ErrorMessage = "¬ведите текст, длиной не больше 60 символов";
				args.IsValid = false;
			}
		}
	}
}