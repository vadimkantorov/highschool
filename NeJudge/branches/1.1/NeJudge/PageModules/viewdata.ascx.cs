using System;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;
using SharpDfTest;

namespace Ne.Judge
{
	public class viewfile : UserControl
	{
		protected DataGrid reportGrid;
		protected Literal fileContent;

		private void ShowError(string text)
		{
			fileContent.Text = "<font color='red'><b>" + text + "</b></font>";
			fileContent.Visible = true;
		}

		private void ShowFile(string path)
		{
			if (!File.Exists(path))
			{
				ShowError("Файл не существует");
				return;
			}
			StreamReader rdr = new StreamReader(path);
			using (rdr)
			{
				string str;
				while ((str = rdr.ReadLine()) != null)
				{
					fileContent.Text += Server.HtmlEncode(str) + "<br>";
				}
			}
		}

		private void Page_Load(object sender, EventArgs e)
		{
			fileContent.Visible = reportGrid.Visible = false;
			int sid = 0;
			string sstr = "";
			try
			{
				sstr = Page.Request.QueryString["sid"];
				sid = int.Parse(sstr);
			}
			catch (FormatException)
			{
				throw new NeJudgeInvalidParametersException("sid");
			}
			catch (ArgumentNullException)
			{
				throw new NeJudgeInvalidParametersException("sid");
			}
			BaseDb db = DbFactory.ConstructDatabase();
			if (!db.CheckSid(sid))
			{
				throw new NeJudgeInvalidParametersException("sid");
			}
			string mode = Page.Request.QueryString["mode"];
			if (mode == null)
			{
				throw new NeJudgeInvalidParametersException("mode");
			}
			mode = mode.ToLower();
			Submission s = db.GetSubmission(sid);
			if (mode == "comp-report")
			{
				fileContent.Visible = true;
				if (s.Result.Code == Result.WAIT || s.Result.Code == Result.RU)
				{
					ShowError("Решение еще проверяется");
					return;
				}
				if (!BaseDb.IsAdmin(Page.User))
				{
					if (s.UID != db.GetUid(Page.User.Identity.Name) || s.Result.Code != Result.CE)
					{
						throw new NeJudgeSecurityException("Identity"); //TODO: срочно пределать
					}
				}
				string report = DfTest.GetReportFilename(sid.ToString());
				ShowFile(report);
			}
			else if (mode == "error-report")
			{
				fileContent.Visible = true;
				if (s.UID != db.GetUid(Page.User.Identity.Name) || s.Result.Code != Result.FA)
				{
					throw new NeJudgeSecurityException("Identity");
				}
				string errfile = Path.Combine(
					Path.Combine(Config.SubmissionsDirectory, sid.ToString()), "error.desc");
				ShowFile(errfile);
			}
			else if (mode == "test-report")
			{
			}
			else
			{
				throw new NeJudgeInvalidParametersException("mode");
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.Load += new EventHandler(this.Page_Load);

		}

		#endregion
	}
}