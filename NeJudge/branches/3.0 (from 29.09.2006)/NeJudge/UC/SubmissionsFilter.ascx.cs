using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.Interfaces;
using Ne.Helpers;

namespace Ne.Judge
{
	public partial class SubmissionsFilterControl : UserControl, ICallbackEventHandler
	{
		protected SubmissionsFilter filt;
		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !Page.IsPostBack )
			{
				if ( Page.User.IsInRole("Judge") )
				{
					usersDDL.Items.Add(new ListItem("--Любой--", "ALL"));
					foreach( User u in User.GetUsers() )
						usersDDL.Items.Add(new ListItem(u.Name, u.ID));
					usersDDL.SelectedValue = filt.RequiredUserID ? filt.UserID : "ALL";
				}
				else
					userTR.Visible = false;

				/*outcomesDDL.Items.Add(new ListItem("--Любой--", "ALL"));

				if ( filt.ContestID != 0 )
				{
					ContestTypeHandler h = Factory.GetHandlerInstance(Contest.GetContest(filt.ContestID).Type);
					foreach ( string str in h.OutcomeManager.GetOutcomes() )
						outcomesDDL.Items.Add(new ListItem(h.OutcomeManager.GetPrintableValue(str), str));
				}

				outcomesDDL.SelectedValue = filt.RequiredOutcome ? filt.Outcome : "ALL";*/
								
				selprob.ProblemID = ( filt.RequiredProblemID ) ? filt.ProblemID : 0;
				selprob.ContestID = filt.ContestID;
			}
			else//?
				UpdateFilterField();
			selprob.ContestChangedEventHandler("",Page.ClientScript.GetCallbackEventReference(
				this, "arg", "ProcessOutcomeCallbackResult", "context"));
		}

		public void filterButton_Click(object sender, EventArgs e)
		{
			string redirect_url = Helpers.UrlRenderer.RenderStatusUrl(filt.ContestID);
			redirect_url += "&problemID=" + filt.ProblemID;
			if(filt.RequiredOutcome)
				redirect_url += "&outcome=" + filt.Outcome;
			if(Page.User.IsInRole("Judge"))
				redirect_url += "&userID=" + filt.UserID;
			Page.Response.Redirect(redirect_url);
		}

		void UpdateFilterField()
		{
			filt.ContestID = selprob.ContestID;
			filt.ProblemID = selprob.ProblemID;
			filt.Outcome = Request.Form[outcomesDDL.UniqueID];
			if( Page.User.IsInRole("Judge") )
				filt.UserID = usersDDL.SelectedValue;
		}

		public SubmissionsFilter Filter
		{
		 	set	{ filt = value;	}
		}

		protected override object SaveViewState()
		{
			return new Pair(base.SaveViewState(), filt);
		}

		protected override void LoadViewState(object savedState)
		{
			Pair p = (Pair)savedState;
			base.LoadViewState(p.First);
			filt = (SubmissionsFilter)p.Second;
		}

		#region ICallbackEventHandler Members
		string result;
		public string GetCallbackResult()
		{
			return result;
		}

		public void RaiseCallbackEvent(string eventArgument)
		{
			result = "";
			try
			{
				string[] tmp = eventArgument.Split(':');
				if ( tmp.Length == 2 )
				{
					int contestID;
					if ( int.TryParse(tmp[0], out contestID) )
					{
						result += "--Любой--^ALL|";
						int i = 0;
						foreach ( OutcomeInfo outcome in NeJudgeServer.Instance.GetOutcomes(contestID) )
							result += string.Format("{0}^{1}|", outcome.PrintableValue, i++);
					}
					else
						result = "Ошибка в обработке Callback^1";
				}
			}
			catch { result = "Ошибка в обработке Callback^1"; }
		}

		#endregion
	}
}
