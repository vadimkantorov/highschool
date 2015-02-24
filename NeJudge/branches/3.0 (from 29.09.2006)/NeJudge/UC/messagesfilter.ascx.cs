using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ne.Database.Classes;

namespace Ne.Judge
{
	public partial class MessagesFilterControl : UserControl
	{
		MessagesFilter filt;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				if ( Page.User.IsInRole("Judge") )
				{
					usersDDL.Items.Add(new ListItem("--Ыўсющ--", "ALL"));
					foreach( User u in User.GetUsers() )
						if((u.Role & SystemRole.User) != 0)
							usersDDL.Items.Add(new ListItem(u.Name, u.ID));
					usersDDL.SelectedValue = filt.RequiredUserID ? filt.UserID : "ALL";
				}
				else
					userTR.Visible = false;

				selprob.ProblemID = ( filt.ReguiredProblemID ) ? filt.ProblemID : 0;
				selprob.ContestID = filt.ContestID;
				if ( filt.Type != MessageType.JuryMessage )
					ansCB.Checked = !filt.RequiredEmptyJuryMessage;
				else
					ansCB.Visible = false;
			}
			UpdateFilterField();
		}

		protected override object SaveViewState()
		{
			return new Pair(base.SaveViewState(), filt);
		}

		protected override void LoadViewState(object savedState)
		{
			Pair p = (Pair)savedState;
			base.LoadViewState(p.First);
			filt = (MessagesFilter)p.Second;
		}

		public void filterButton_Click(object sender, EventArgs e)
		{
			string redirect_url = Helpers.UrlRenderer.RenderQuestionsUrl(filt.ContestID);
			redirect_url += "&problemID=" + filt.ProblemID;
			if(filt.RequiredUserID && Page.User.IsInRole("Judge"))
				redirect_url += "&userID=" + filt.UserID;
			if ( filt.Type != MessageType.JuryMessage )
				redirect_url += "&answered=" + !filt.RequiredEmptyJuryMessage;
			Page.Response.Redirect(redirect_url);
		}

		void UpdateFilterField()
		{
			if ( filt.Type != MessageType.JuryMessage )
				filt.RequiredEmptyJuryMessage = !ansCB.Checked;
			filt.ContestID = selprob.ContestID;
			filt.ProblemID = selprob.ProblemID;
			if( Page.User.IsInRole("Judge") )
				filt.UserID = usersDDL.SelectedValue;
		}

		public MessagesFilter Filter
		{
			get { return filt; }
			set	{ filt = value;	}
		}
	}
}
