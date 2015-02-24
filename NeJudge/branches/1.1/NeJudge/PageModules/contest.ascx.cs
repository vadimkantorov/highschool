using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	[RequireContestId]
	public class contest : UserControl
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
			this.Load += new System.EventHandler(this.Page_Load);

		}

		#endregion

		protected Repeater Repeater1;
		protected HyperLink quesHyperLink;
		protected HyperLink subHyperLink;
		protected HyperLink monHyperLink;
		protected HyperLink editHyperLink;
		protected HyperLink printHyperLink;
		protected Literal Literal2;
		protected HtmlTable Table2;
		protected SelectContest selcon;
		protected ErrorMessage ErrorMessage;

		private void RedrawRepeater(int tid)
		{
			DataTable dt = null;//Неуверен, не лучше ли здесь использовать db.Close()
			using(BaseDb db = DbFactory.ConstructDatabase())
				dt = db.GetTable(TableType.forset, tid);

			if (dt.Rows.Count != 0)
			{
				Repeater1.DataSource = dt;
				Repeater1.DataBind();
			}
			else
				Hide("В этом соревновании нет задач");
		}

		private void Hide(string mess)
		{
			ErrorMessage.Message = "<br>"+mess;
			Repeater1.Visible = false;
			monHyperLink.Visible = false;
			subHyperLink.Visible = false;
			quesHyperLink.Visible = false;
			printHyperLink.Visible = false;
		}

		private void InitLinks(int tid)
		{
			subHyperLink.NavigateUrl += tid;
			quesHyperLink.NavigateUrl += tid;
			monHyperLink.NavigateUrl += tid;
			editHyperLink.NavigateUrl += tid;
			printHyperLink.NavigateUrl += tid;
		}

		private void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType(), Context);
				rp.ProcessRequirements();
				using( BaseDb db = DbFactory.ConstructDatabase() )
				{
					if ( rp.TidDefined )
					{
						selcon.TID = rp.ContestID;
					
						#region Ссылочки (Visible в зависимости от роли)
						InitLinks(rp.ContestID);
						if(BaseDb.IsAnonymous(Page.User))
						{
							subHyperLink.Visible = false;
							quesHyperLink.Visible = false;
						}
						if(BaseDb.IsAdmin(Page.User))
							editHyperLink.Visible = true;
						#endregion

						if (db.GetContest(rp.ContestID).Future && !BaseDb.IsAdmin(Page.User))
							throw new NeJudgeInvalidParametersException("tid");//Hide("Это соревнование начнется через " + HtmlFunctions.BeautifyTimeSpan(db.GetContest(tid).Beginning - DateTime.Now, false) + ". Сейчас задачи посмотреть нельзя.");
						else
							RedrawRepeater(rp.ContestID);
					}
					else
						Hide("");
				}
			}
		}
	}
}