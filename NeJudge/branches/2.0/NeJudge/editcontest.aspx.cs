 using System;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Ne.Database.Classes;
using Ne.Helpers;
namespace Ne.Judge
{
	[RequireMode("edit", "RequireContestId")]
	[RequireMode("create")]
	public partial class EditContestPage : Page
	{
		protected Contest contest;
		protected void Page_Load(object sender, EventArgs e)
		{
			if( !IsPostBack )
			{
				RequirementsProcessor rp = new RequirementsProcessor(GetType());
				rp.ProcessRequirements();
				if( rp.ModeDefined )
				{
					if( rp.Mode == "edit" )
						contest = Contest.GetContest(rp.ContestID);
					mv.SetActiveView(contestParams);
					monthsDDL.Attributes.Add("onchange", "SelectedMonthChanged(this.options[this.selectedIndex].value)");
				}
				else
				{
					menu.Visible = false;
					mv.Visible = false;
					saveB.Visible = false;
				}
			}
		}
		
		protected void menu_MenuItemClick(object sender, MenuEventArgs e)
		{
			mv.ActiveViewIndex = Convert.ToInt32(e.Item.Value);
		}
		
		protected override object SaveViewState()
		{
			return new Pair(contest,base.SaveViewState());
		}
		protected override void LoadViewState(object st)
		{
			Pair p = (Pair)st;
			contest = p.First as Contest;
			base.LoadViewState(p.Second);
		}
		#region Problems Params
		protected void problemsParams_Activate(object sender, EventArgs e)
		{
			if( mv.Visible )
			{
				DataTable dt = new DataTable();
				dt.Columns.Add("ID");
				dt.Columns.Add("ShortName");
				dt.Columns.Add("Name");
				foreach( Problem p in Problem.GetProblems(contest.ID) )
				{
					DataRow dr = dt.NewRow();
					dr["ID"] = p.ID;
					dr["ShortName"] = p.ShortName;
					dr["Name"] = p.Name;
					dt.Rows.Add(dr);
				}
				problemsGV.DataSource = dt;
				problemsGV.DataBind();
			}
			if( contest.Time != ContestTime.Forthcoming )
				addProblemB.Visible = false;
		}
		protected void upLB_Click(object sender, EventArgs e)
		{
		}
		protected void downLB_Click(object sender, EventArgs e)
		{
		}
		protected void deleteLB_Click(object sender, EventArgs e)
		{
		}
		#endregion
		#region Contest Params
		protected void contestParams_Activate(object sender, EventArgs e)
		{
			if( mv.Visible )
			{
				if( contest != null )
				{
					if( contest.Time == ContestTime.Forthcoming )
					{
						yearsDDL.Items.Add(DateTime.Now.Year.ToString());
						yearsDDL.Items.Add(( DateTime.Now.Year + 1 ).ToString());
						yearsDDL.Items.Add(( DateTime.Now.Year + 2 ).ToString());
						for( int i = 0; i <= 23; i++ )
							hoursDDL.Items.Add(i.ToString());

						yearsDDL.SelectedValue = contest.Beginning.Year.ToString();
						monthsDDL.SelectedValue = contest.Beginning.Month.ToString();
						hoursDDL.SelectedValue = contest.Beginning.Hour.ToString();
						minutesTB.Text = contest.Beginning.Minute.ToString();
						dayF.Value = contest.Beginning.Day.ToString();
						nameTB.Text = contest.Name;
						durHoursTB.Text = contest.Duration.Hours.ToString();
						durMinutesTB.Text = contest.Duration.Minutes.ToString();
					}
					else
					{
						yearsDDL.Items.Add(contest.Beginning.Year.ToString());
						monthsDDL.Items.Add(contest.Beginning.Month.ToString());
						dayF.Value = contest.Beginning.Day.ToString();
						hoursDDL.Items.Add(contest.Beginning.Hour.ToString());
						minutesTB.Text = contest.Beginning.Minute.ToString();
						
						nameTB.Text = contest.Name;

						durHoursTB.Text = contest.Duration.Hours.ToString();
						durMinutesTB.Text = contest.Duration.Minutes.ToString();

						yearsDDL.Enabled = false;
						monthsDDL.Enabled = false;
						hoursDDL.Enabled = false;
						minutesTB.Enabled = false;
						daysDDL.Enabled = false;
						if( contest.Time == ContestTime.Past )
						{
							durHoursTB.Enabled = false;
							durMinutesTB.Enabled = false;
						}
					}
				}
				else
				{
					yearsDDL.Items.Add(DateTime.Now.Year.ToString());
					yearsDDL.Items.Add(( DateTime.Now.Year + 1 ).ToString());
					yearsDDL.Items.Add(( DateTime.Now.Year + 2 ).ToString());
					monthsDDL.SelectedValue = DateTime.Now.Month.ToString();
					dayF.Value = DateTime.Now.Day.ToString();
					for( int i = 0; i <= 23; i++ )
						hoursDDL.Items.Add(i.ToString());
					hoursDDL.SelectedValue = DateTime.Now.Hour.ToString();
					minutesTB.Text = DateTime.Now.Minute.ToString();

					menu.Items[1].Enabled = false;
					menu.Items[2].Enabled = false;
				}
			}
		}
		protected void contestParams_Deactivate(object sender, EventArgs e)
		{
			//TODO: Freeze
			if( contest == null )
				contest = new Contest();
			contest.Name = nameTB.Text;
			contest.Beginning = new DateTime(Convert.ToInt32(yearsDDL.SelectedValue), Convert.ToInt32(monthsDDL.SelectedValue),
				Convert.ToInt32(dayF.Value), Convert.ToInt32(hoursDDL.SelectedValue), Convert.ToInt32(minutesTB.Text), 0);
			contest.Ending = contest.Beginning + new TimeSpan(Convert.ToInt32(durHoursTB.Text), Convert.ToInt32(durMinutesTB.Text), 0);
			contest.Store();
		}
		protected void beginningCV_ServerValidate(object source, ServerValidateEventArgs args)
		{
			DateTime dt = new DateTime(Convert.ToInt32(yearsDDL.SelectedValue), Convert.ToInt32(monthsDDL.SelectedValue),
				Convert.ToInt32(dayF.Value), Convert.ToInt32(hoursDDL.SelectedValue), Convert.ToInt32(minutesTB.Text), 0);
			if( dt <= DateTime.Now )
				args.IsValid = false;
		}
		#endregion
		#region RightsAndContestants Params
		protected void rightsAndParticipantsParams_Activate(object sender, EventArgs e)
		{

		}
		protected void rightsAndParticipantsParams_Deactivate(object sender, EventArgs e)
		{

		}
		#endregion
		protected void saveB_Click(object sender, EventArgs e)
		{
			if( mv.ActiveViewIndex == 0 )
				contestParams_Deactivate(null, null);
			else if( mv.ActiveViewIndex == 2 )
				rightsAndParticipantsParams_Deactivate(null, null);
			
		}
	}
}