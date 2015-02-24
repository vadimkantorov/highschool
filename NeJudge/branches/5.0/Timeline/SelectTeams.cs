using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Model;

namespace Timeline
{
	public partial class SelectTeams : Form
	{
		readonly int maxContestants;

		public SelectTeams(IEnumerable<User> teams, int maxContestants)
		{
			this.maxContestants = maxContestants;
			InitializeComponent();
			lblHello.Text = "Выберите не более " + maxContestants + " участников";

			lstTeams.DataSource = teams;
			lstTeams.DisplayMember = "DisplayName";
		}

		public IList<User> SelectedTeams
		{
			get
			{
				return lstTeams.CheckedItems.Cast<User>().ToList();
			}
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void lstTeams_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			if (lstTeams.CheckedItems.Count + 1 > maxContestants)
				e.NewValue = CheckState.Unchecked;
		}
	}
}
