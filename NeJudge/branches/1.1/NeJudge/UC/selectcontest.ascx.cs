using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ne.Database;

namespace Ne.Judge
{
	/// <summary>
	///		Summary description for SelectContest.
	/// </summary>
	public class SelectContest : UserControl
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.goButton.Click += new EventHandler(this.goButton_Click);
			this.Init += new EventHandler(this.Page_Init);

		}

		#endregion

		protected Button goButton;
		protected DropDownList tidDropDownList;
		private string address;
		private bool now, future, old;

		public bool Now
		{
			set { now = value; }
		}

		public bool Future
		{
			set { future = value; }
		}

		public bool Old
		{
			set { old = value; }
		}

		public string Address
		{
			get { return address; }
			set { address = value; }
		}

		private void Page_Init(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				BaseDb db = DbFactory.ConstructDatabase();
				Contest t;
				if (now)
				{
					int[] nowtids = db.GetNowTids();
					for (int i = 0; i < nowtids.Length; i++)
					{
						t = db.GetContest(nowtids[i]);
						tidDropDownList.Items.Add(new ListItem(t.Name, nowtids[i].ToString()));
					}
				}
				if (old)
				{
					int[] oldtids = db.GetOldTids();
					for (int i = 0; i < oldtids.Length; i++)
					{
						t = db.GetContest(oldtids[i]);
						tidDropDownList.Items.Add(new ListItem(t.Name, oldtids[i].ToString()));
					}
				}
				if (future)
				{
					int[] futuretids = db.GetFutureTids();
					for (int i = 0; i < futuretids.Length; i++)
					{
						t = db.GetContest(futuretids[i]);
						tidDropDownList.Items.Add(new ListItem(t.Name, futuretids[i].ToString()));
					}
				}
				db.Close();
			}
		}

		public int TID
		{
			set
			{
				tidDropDownList.SelectedValue = value.ToString();
			}
			get
			{
				return int.Parse(tidDropDownList.SelectedValue);
			}
		}

		private void goButton_Click(object sender, EventArgs e)
		{
			Response.Redirect(address + "?tid=" + TID);
		}
	}
}