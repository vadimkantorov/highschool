using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database.Classes;

namespace Ne.Judge
{
	public partial class SelectContest : UserControl
	{
		private string address;
		private bool past, current, forthcoming;

		public bool Past
		{
			set { past = value; }
		}

		public bool Forthcoming
		{
			set { forthcoming = value; }
		}

		public bool Current
		{
			set { current = value; }
		}

		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		protected void Page_Load(object sender, EventArgs e)
		{
			if ( !IsPostBack )
			{
				ContestTime type = ContestTime.None;
				if ( current )
					type |= ContestTime.Current;
				if ( past )
					type |= ContestTime.Past;
				if ( forthcoming )
					type |= ContestTime.Forthcoming;
				contestsDDL.DataSource = Contest.GetContests(type);
				contestsDDL.DataBind();

				if( contestsDDL.Items.Count == 0 )
				{
					errmessTR.Visible = true;
					ddlTR.Visible = false;
				}
			}
		}

		public int ContestID
		{
			set
			{
				contestsDDL.SelectedValue = value.ToString();
			}
		}
	}
}