#region Using directives

using System;
using System.Threading;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

#endregion

namespace WinBalans.Forms
{
	partial class Settings : Form
	{
		Parameters p1;
		Parameters p2;
		int sem = 0;

		public Settings(Parameters p)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			userDescToolTip_.SetToolTip(userDescriptionLabel, userDescriptionLabel.Text);
			p1 = (Parameters)p.Clone();
			p2 = (Parameters)p.Clone();
			//ChangeValues(ParametersInstance);
			UpdatePluginListView();

		}

		public Parameters GetParameters()
		{
			return p2;
		}

		private void UpdatePluginListView()
		{
			pluginListView.Items.Clear();
			foreach (Plugin p in p2.PluginList)
			{
				ListViewItem l = new ListViewItem(new string[] { p.Name, p.Description, p.Version });
				l.Checked = p.Checked;
				pluginListView.Items.Add(l);
			}
		}

		private void timerEnabledCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			intervalNumeric.Enabled = (this.timerEnabledCheckbox.Checked) ? true : false;
		}
		private void Exit(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void ChangeValues(Parameters p)
		{
			p2 = (Parameters)p.Clone();
			timerEnabledCheckbox.Enabled = p2.TimerEnabled;
			intervalNumeric.Value = (decimal)p2.Interval / (1000 * 60);
			UpdatePluginListView();
		}
		public bool Install(string folder)
		{
			try
			{
				if (!Directory.Exists(folder))
					Directory.CreateDirectory(folder);
				File.Copy(AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName, folder + AppDomain.CurrentDomain.FriendlyName, true);
				MessageBox.Show("Installed");
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		private void openPluginButton_Click(object sender, System.EventArgs e)
		{
			if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Plugins\"))
				Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Plugins\");
			openDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Plugins\";
			openDialog.ShowDialog();
			Plugin pl = new Plugin(openDialog.FileName);
			if (pl.Valid)
			{
				p2.PluginList.Add(pl);
				//pl.Install();
				MessageBox.Show("Вы установили плагин. Если метод 'public bool Install()'\r\nреализован по-человечески, то после нажатия на лупу добавленный плагин появится в обновлённом списке.", "Важная информация !!!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			}
			else
				MessageBox.Show("Плагин, который вы попытались установить, некорректный.\r\nВозможно вы наобум выбрали длл'ину от нечего делать.\r\nПрекратите хулиганить.", "Важная информация !!!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
			//?
			//UpdatePluginListView();
		}

		private void pluginPropertiesButton_Click(object sender, EventArgs e)
		{
			if (pluginListView.SelectedIndices.Count != 0)
			{
				Plugin p = (Plugin)TempParameters.ParametersInstance.PluginList[pluginListView.SelectedIndices[0]];
				PluginProperties pf = new PluginProperties(p);
				pf.ShowDialog(this);
				TempParameters.ParametersInstance.PluginList[TempParameters.ParametersInstance.PluginList.IndexOf(p)] = pf.GetPlugin();
			}
		}
		private void deletePluginButton_Click(object sender, EventArgs e)
		{
			if (pluginListView.SelectedIndices.Count != 0)
			{
				p2.PluginList.RemoveAt(pluginListView.SelectedIndices[0]);
				if(false)
					MessageBox.Show("Вы удалили плагин. Если метод 'public bool Uninstall()'\r\nреализован по-человечески, то удалённый плагин исчезне.", "Важная информация !!!", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
				UpdatePluginListView();
			}
			throw new NotImplementedException();
		}

		
		/*private void pluginListView_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			//установить флажочек в 1, анчекаешь чекбокс, возникает событие, в обра делается проверка, если 1, то ретен, после уст флаж в 0
			if (sem == 0)
			{
				Interlocked.Increment(ref sem);
				if (e.NewValue == CheckState.Unchecked)
					p2.PluginList.ReverseSelection(e.Index);
				if (e.NewValue == CheckState.Checked)
				{
					if (pluginListView.CheckedItems.Count != 0)
					{
						foreach (ListViewItem l in pluginListView.CheckedItems)
							((Plugin)p2.PluginList[l.Index]).Checked = false;
						UpdatePluginListView();
					}
					p2.PluginList.ReverseSelection(e.Index);
				}
				Interlocked.Decrement(ref sem);
			}
			else
				return;
		}*/
		private void setDefaultsButton_Click(object sender, EventArgs e)
		{
			ChangeValues(Parameters.Default);
		}

		private void intervalNumeric_ValueChanged(object sender, EventArgs e)
		{
			p2.Interval = (double)intervalNumeric.Value;
		}

		private void pluginListView_SelectedIndexChanged(object sender, EventArgs e)
		{
			Plugin p = (Plugin)TempParameters.ParametersInstance.PluginList[pluginListView.SelectedIndices[0]];
			userDescriptionLabel.Text = p.UserDescription;
		}

		private void Settings_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible == false)

				if (DialogResult == DialogResult.OK)
					p1 = (Parameters)p2.Clone();
				else
					p2 = (Parameters)p1.Clone();
			else
				ChangeValues(p2);
		}

		private void findPluginsPictureBox_Click(object sender, EventArgs e)
		{
			TempParameters.ParametersInstance.PluginList = PluginCollection.FindPlugins();
			UpdatePluginListView();
		}
	}
}