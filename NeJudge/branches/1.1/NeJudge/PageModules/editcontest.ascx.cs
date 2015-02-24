using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	public class editcontest : UserControl
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
			this.beginningCustomValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.beginningCustomValidator_ServerValidate);
			this.endingCustomValidator.ServerValidate += new System.Web.UI.WebControls.ServerValidateEventHandler(this.endingCustomValidator_ServerValidate);
			this.addButton.Click += new System.EventHandler(this.addButton_Click);
			this.finishButton.Click += new System.EventHandler(this.finishButton_Click);
			this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
			this.Unload += new System.EventHandler(this.Page_Unload);
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		protected TextBox beginningTextBox;
		protected TextBox endingTextBox;
		protected HtmlTable Table2;
		protected Repeater Repeater1;
		protected TextBox nameTextbox;
		protected Table Table1;
		protected HtmlTable Table3;
		protected HtmlGenericControl outerror;
		protected Button finishButton;
		protected Button deleteButton;
		protected Button addButton;
		protected HtmlTable Table4;
		protected CustomValidator beginningCustomValidator;
		protected CustomValidator endingCustomValidator;
		protected RequiredFieldValidator RequiredFieldValidator1;

		private BaseDb db = DbFactory.ConstructDatabase();

		private int tid = -1;

/*
		private void Hide(string mess)
		{
			outerror.InnerHtml = "<FONT COLOR='Red'><b>" + mess + "</b></FONT>";
			Table3.Visible = false;
			Table4.Visible = false;
		}
*/

		private void Page_Load(object sender, EventArgs e)
		{
			if (!BaseDb.IsAdmin(Page.User))
				throw new NeJudgeSecurityException("Administrator");
			if (!IsPostBack)
			{
				try
				{
					if (tid == -1)
						tid = int.Parse(Request.QueryString["tid"]);
				}
				catch
				{
					tid = -1;
				}
				if (db.CheckTid(tid))
				{
					Contest t = db.GetContest(tid);
					beginningTextBox.Text = t.Beginning.ToString("dd.MM.yyyy H:mm");
					endingTextBox.Text = t.Ending.ToString("dd.MM.yyyy H:mm");
					nameTextbox.Text = t.Name;
					if (t.Now)
					{
						beginningTextBox.Enabled = false;
						//deleteButton.Visible = false;
						//HideRemoveCheckBox();
					}
					if (t.Old)
					{
						beginningTextBox.Enabled = false;
						endingTextBox.Enabled = false;
						addButton.Visible = false;
						//deleteButton.Visible = false;
						//HideRemoveCheckBox();
					}
					RedrawRepeater();
				}
				else
				{
					tid = -1;
					addButton.Visible = false;
					deleteButton.Visible = false;
					finishButton.Text = "—оздать соревнование";
				}
			}
		}

		private void Page_Unload(object sender, EventArgs e)
		{
			db.Close();
		}

/*
		private void HideRemoveCheckBox()
		{
			Repeater1.ItemTemplate = LoadTemplate("../templates/noremoveritem.ascx");
			Repeater1.HeaderTemplate = LoadTemplate("../templates/noremoverheader.ascx");
		}
*/

		private void UpdateDatabase()
		{
			for (int i = 0; i < Repeater1.Items.Count; i++)
			{
				CheckBox remove = (CheckBox) Repeater1.Items[i].FindControl("Remove");
				if (remove.Checked)
				{
					int pid = int.Parse(((Literal) Repeater1.Items[i].FindControl("PID")).Text);
					db.RemoveProblem(pid);
				}
			}
		}

		private int InsertContest(Contest t)
		{
			try
			{
				return db.AddContest(t);
			}
			catch (SystemException ex)
			{
				outerror.InnerHtml = "<b>An exception of type " + ex.GetType() +
					" was encountered while attempting to roll back the transaction. Message:</b> <br/><br/> <i>" + ex.Message + "</i>";
			}
			return -1;
		}

		private void UpdateContest(Contest t)
		{
			try
			{
				db.UpdateContest(t, tid);
			}
			catch (SystemException ex)
			{
				outerror.InnerHtml = "<b>An exception of type " + ex.GetType() +
					" was encountered while attempting to roll back the transaction. Message:</b> <br/><br/> <i>" + ex.Message + "</i>";
			}
		}

		private void deleteButton_Click(object sender, EventArgs e)
		{
			UpdateDatabase();
			RedrawRepeater();
		}

		protected override object SaveViewState()
		{
			return tid;
		}

		protected override void LoadViewState(object st)
		{
			tid = (int) st;
		}

		private void RedrawRepeater()
		{
			Repeater1.DataSource = db.GetTable(TableType.foredit, tid).DefaultView;
			Repeater1.DataBind();
		}

		private void addButton_Click(object sender, EventArgs e)
		{
			Response.Redirect("~/editproblem.aspx?tid=" + tid);
		}

		private void finishButton_Click(object sender, EventArgs e)
		{
			if (Page.IsValid)
			{
				if (tid == -1)
				{
					Contest t = new Contest(DateTime.Parse(beginningTextBox.Text), DateTime.Parse(endingTextBox.Text), nameTextbox.Text.Trim());
					tid = InsertContest(t);
					addButton.Visible = true;
					finishButton.Text = "«акончить";
				}
				else
				{
					Contest t = new Contest(DateTime.Parse(beginningTextBox.Text), DateTime.Parse(endingTextBox.Text), nameTextbox.Text.Trim());
					UpdateContest(t);
					Response.Redirect("~/contest.aspx?tid=" + tid);
				}
			}
		}

		private DateTime ParseDate(string date)
		{
			DateTime dt;
			try
			{
				dt = DateTime.ParseExact(date, "dd.MM.yyyy H:mm", new CultureInfo("ru-RU"));
			}
			catch
			{
				dt = new DateTime(0);
			}
			return dt;
		}

		//к валидаторам не прописан ControlToValidate, чтобы они не пропускали через себ€ пустую строку
		private void beginningCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			DateTime dt = ParseDate(beginningTextBox.Text);

			if (dt == new DateTime(0))
			{
				args.IsValid = false;
			}
			else if (dt <= DateTime.Now)
			{
				beginningCustomValidator.ErrorMessage = "ƒата начала не должна быть прошедшей";
				args.IsValid = false;
			}
		}

		private void endingCustomValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			if (Page.IsValid)
			{
				DateTime dt1 = ParseDate(beginningTextBox.Text);
				DateTime dt2 = ParseDate(endingTextBox.Text);

				if (dt2 == new DateTime(0))
				{
					args.IsValid = false;
				}
				else if (dt2 <= dt1)
				{
					endingCustomValidator.ErrorMessage = "ƒата окончани€ должна быть больше даты начала";
					args.IsValid = false;
				}
			}
		}
	}
}