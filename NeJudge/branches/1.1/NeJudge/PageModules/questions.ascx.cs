using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	[RequireContestId]
	public class questions : UserControl
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
			this.questionsGrid.CancelCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.questionsGrid_CancelCommand);
			this.questionsGrid.EditCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.questionsGrid_EditCommand);
			this.questionsGrid.UpdateCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.questionsGrid_UpdateCommand);
			this.questionsGrid.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.questionsGrid_ItemDataBound);
			this.Load += new System.EventHandler(this.Page_Load);
			this.Init += new System.EventHandler(this.Page_Init);
		}

		#endregion

		protected SelectContest selcon;
		protected ErrorMessage ErrorMessage;
		protected DataGrid questionsGrid;
		private int tid;

		private void Page_Init(object sender, EventArgs e)
		{
			if (BaseDb.IsAdmin(Page.User))
				questionsGrid.EnableViewState = true;
		}
		
		private void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();
				
				Page.Response.AddHeader("Refresh", "90");
				if ( rp.TidDefined )
				{
					tid = selcon.TID = rp.ContestID;

					questionsGrid.Columns[5].Visible = BaseDb.IsAdmin(Page.User);
								
					using(BaseDb db = DbFactory.ConstructDatabase())
					{
						if (db.GetContest(tid).Future)
							throw new NeJudgeInvalidParametersException("tid");//Hide("Ќельз€ просмотреть вопросы будущего соревновани€");
						else
						{
							Off();
							Bind();
						}
					}
				}
			}
		}
		
		private void Bind()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("QID", typeof (int));
			dt.Columns.Add("PID", typeof (int));
			dt.Columns.Add("Question");
			dt.Columns.Add("Answer");
			DataRow dr;
			int uid = 0;
			ArrayList qs = null;
			
			using(BaseDb db = DbFactory.ConstructDatabase())
			{
				if (!BaseDb.IsAdmin(Page.User) && !BaseDb.IsAnonymous(Page.User))
					uid = db.GetUid(Page.User.Identity.Name);

				qs = db.GetQuestions(tid);
			}
			if (BaseDb.IsAdmin(Page.User))
			{
				foreach (Question q in qs)
				{
					if (!q.Answered)
					{
						dr = dt.NewRow();
						dr[0] = q.Qid;
						dr[1] = q.Pid;
						dr[2] = q.QuestioN;
						dr[3] = q.Answer;
						dt.Rows.Add(dr);
					}
				}
			}
			else
			{
				foreach (Question q in qs)
				{
					if (q.Uid == uid || q.Uid == 0)
					{
						dr = dt.NewRow();
						dr[0] = q.Qid;
						dr[1] = q.Pid;
						dr[2] = q.QuestioN;
						dr[3] = q.Answer;
						dt.Rows.Add(dr);
					}
				}
			}
			questionsGrid.DataSource = dt;
			questionsGrid.DataBind();
			HtmlFunctions.BeautifyDataGrid(questionsGrid);
			if (dt.Rows.Count == 0)
				Hide("ѕо этому соревнованию нет вопросов");
		}

		private void Hide(string mess)
		{
			questionsGrid.Visible = false;
			ErrorMessage.Message = "<br>"+mess;
		}

		private void Off()
		{
			questionsGrid.Columns[4].Visible = false;
		}

		private void questionsGrid_EditCommand(object source, DataGridCommandEventArgs e)
		{
			questionsGrid.EditItemIndex = e.Item.ItemIndex;
			e.Item.FindControl("Yes").Visible = true;
			e.Item.FindControl("No").Visible = true;
			e.Item.FindControl("Nc").Visible = true;
			Bind();
		}

		private void questionsGrid_CancelCommand(object source, DataGridCommandEventArgs e)
		{
			Off();

			questionsGrid.EditItemIndex = -1;
			Bind();
		}

		private void questionsGrid_UpdateCommand(object source, DataGridCommandEventArgs e)
		{
			Off();

			TextBox c = (TextBox) e.Item.Cells[3].Controls[1];
			int sid = int.Parse(e.Item.Cells[0].Text);
			using(BaseDb db = DbFactory.ConstructDatabase())
				db.AddAnswer(sid, c.Text, false);
			questionsGrid.EditItemIndex = -1;
			Bind();
		}

		private void questionsGrid_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.EditItem)
			{
				questionsGrid.Columns[4].Visible = true;
				e.Item.FindControl("Yes").Visible = true;
				e.Item.FindControl("No").Visible = true;
				e.Item.FindControl("Nc").Visible = true;
				TextBox c = (TextBox) e.Item.FindControl("answerTextBox");
				(e.Item.FindControl("Yes") as Button).Attributes.Add("onclick",
				                                                     "document.getElementById('" + c.ClientID + "').value = 'Yes'; return false;");
				(e.Item.FindControl("No") as Button).Attributes.Add("onclick",
				                                                    "document.getElementById('" + c.ClientID + "').value = 'No'; return false;");
				(e.Item.FindControl("Nc") as Button).Attributes.Add("onclick",
				                                                    "document.getElementById('" + c.ClientID + "').value = 'No comments'; return false;");
			}
		}

		protected override object SaveViewState()
		{
			object o = base.SaveViewState();
			if(BaseDb.IsAdmin(Page.User))
				return new Pair(o,tid);
			return o;
		}
		
		protected override void LoadViewState(object savedState)
		{
			if(BaseDb.IsAdmin(Page.User))
			{
				Pair p = savedState as Pair;
				base.LoadViewState(p.First);
				tid = (int)p.Second;
			}
			else
				base.LoadViewState(savedState);
		}
	}
}