using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.Helpers;

namespace Ne.Judge
{
	[RequireContestID]
	[RequireEveryProblemID]
	[RequireUserID]
	public partial class QuestionsPage : Page
	{
		protected void Page_Init(object sender, EventArgs e)
		{
			if ( User.IsInRole("Judge") )
				questionsGV.EnableViewState = true;
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			/*if ( !User.IsInRole("Judge") && !User.IsInRole("User") )
				throw new NeJudgeSecurityException("User, Judge");
			if ( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();

				//Page.Response.AddHeader("Refresh", "90");
				MessagesFilter mf = new MessagesFilter(rp.ContestIDDefined?rp.ContestID:0, MessageType.Question);
				if ( rp.UserIDDefined && User.IsInRole("Judge") )
					mf.UserID = rp.UserID;
				mf.RequiredEmptyJuryMessage = 
					Request.QueryString["answered"] == null ||
					Request.QueryString["answered"].ToLower() != "true";
				if( rp.ContestIDDefined )
				{
					if (Contest.GetContest(rp.ContestID).Time == ContestTime.Forthcoming)
						throw new NeJudgeInvalidParametersException("contestID", "Нельзя просмотреть вопросы будущего соревнования");

					mf.ContestID = rp.ContestID;
					mf.ProblemID = ( rp.ProblemIDDefined ) ? rp.ProblemID : 0;
					selmess.Filter = mf;

					questionsGV.Columns[6].Visible = User.IsInRole("Judge");
					Bind();
				}
				else
				{
					questionsGV.Visible = false;
					selmess.Filter = mf;
				}
			}*/
		}

		#region questionsGV event handlers and methods
		void Bind()
		{
			bool judge = User.IsInRole("Judge");

			List<Message> list = new List<Message>();

			foreach( Message m in Message.GetMessages(selmess.Filter) )
				if( judge || ( !judge && m.UserID == User.Identity.Name ) )
					list.Add(m);

			questionsGV.DataSource = list;
			questionsGV.DataBind();
		}

		protected void questionsGV_RowEditing(object sender, GridViewEditEventArgs e)
		{
			questionsGV.Columns[5].Visible = true;

			questionsGV.EditIndex = e.NewEditIndex;
			Bind();
		}

		protected void questionsGV_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
		{
			questionsGV.Columns[5].Visible = false;

			questionsGV.EditIndex = -1;
			Bind();
		}

		protected void questionsGV_RowUpdating(object sender, GridViewUpdateEventArgs e)
		{
			questionsGV.Columns[5].Visible = false;

			int mid = Convert.ToInt32(questionsGV.DataKeys[e.RowIndex].Value);
			TextBox c = (TextBox)questionsGV.FindControl("answerTB");
			
			Message mess = Message.GetMessage(mid);
			mess.JuryMessage = c.Text;
			mess.Store();

			questionsGV.EditIndex = -1;
			Bind();
		}

		protected void questionsGV_RowDataBound(object sender, GridViewRowEventArgs e)
		{
			if ( (e.Row.RowState & DataControlRowState.Edit) != 0 )
			{
				TextBox c = (TextBox)e.Row.FindControl("answerTB");
				((HtmlButton)e.Row.FindControl("Yes")).Attributes.Add("onclick", "document.getElementById('" + c.ClientID + "').value = 'Да'");
				((HtmlButton)e.Row.FindControl("No")).Attributes.Add("onclick", "document.getElementById('" + c.ClientID + "').value = 'Нет'");
				((HtmlButton)e.Row.FindControl("Nc")).Attributes.Add("onclick", "document.getElementById('" + c.ClientID + "').value = 'Без комментариев'");
			}
		}
		#endregion
	}
}
