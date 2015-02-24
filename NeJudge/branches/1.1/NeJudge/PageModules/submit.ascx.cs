using System;
using System.IO;
using System.Threading;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;
using SharpDfTest;

namespace Ne.Judge
{
	[RequireProblemId]
	public class submit : UserControl
	{
		protected DropDownList languageDropDownList;
		protected TextBox sourceTextBox;
		protected Button Button1;
		protected HtmlTable Table1;
		protected HtmlInputFile fileBrowser;
		protected System.Web.UI.WebControls.CustomValidator CustomValidator1;
		protected SelectProblem selprob;

		private void Page_Load(object sender, EventArgs e)
		{
			if (!BaseDb.IsUser(Page.User))//TODO: подумать, может надо перенести код в !IsPostBack
				throw new NeJudgeSecurityException("User");
			
			if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();

				selprob.AddHidableControl(Table1);
				if ( rp.PidDefined )
					selprob.PID = rp.ProblemID;
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
			this.CustomValidator1.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.CustomValidator1_ServerValidate);
			this.Button1.Click += new System.EventHandler(this.Button1_Click);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

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
						string extension = "";
						switch (languageDropDownList.SelectedValue)
						{
							case "Pascal":
								extension = "pas";
								break;
							case "C++":
								extension = "cpp";
								break;
							case "C":
								extension = "c";
								break;
							default:
								extension = "cpp";
								break;
						}

						string temp = Path.GetTempFileName();
					
						if (sourceTextBox.Text.Trim() != "")
						{
							StreamWriter sw = null;
							try
							{
								sw = new StreamWriter(temp);
								sw.WriteLine(sourceTextBox.Text.Trim());
							}
							finally
							{
								if (sw != null)
								{
									sw.Close();
								}
							}
						}
						else
						{
							if(fileBrowser.PostedFile != null)
								fileBrowser.PostedFile.SaveAs(temp);
						}

						int uid = db.GetUid(Page.User.Identity.Name); //TODO
						Language l = (Language) Enum.Parse(typeof (Language), extension, true);
						Submission s = new Submission(pid, uid, tid, DateTime.Now, l);
						int sid = db.AddSubmission(s);
						if (Directory.Exists(Path.Combine(Config.SubmissionsDirectory, sid.ToString())))
							Directory.Delete(Path.Combine(Config.SubmissionsDirectory, sid.ToString()));
						Directory.CreateDirectory(Path.Combine(Config.SubmissionsDirectory, sid.ToString()));
						File.Move(temp, Path.Combine(Path.Combine(
							Config.SubmissionsDirectory, sid.ToString()), "sln." + extension)); //TODO:переделать

						DfTest dt = new DfTest(pid.ToString(), sid.ToString(), l.ToString());
						Thread t = new Thread(new ThreadStart(dt.CheckSolution));
						Response.Redirect("~/status.aspx?tid=" + tid, false);
						t.Start();
					}
				}
				else
					throw new NeJudgeInvalidParametersException("pid");//"Соревнование либо закончилась, либо ещё не начиналось.";
			}
		}

		private void CustomValidator1_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
		{
			args.IsValid = (sourceTextBox.Text.Trim() == "" ^ fileBrowser.Value.Trim() == "");
		}
	}
}