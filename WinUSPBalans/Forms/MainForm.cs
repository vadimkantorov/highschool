#region Using directives

using System;
using Ini;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#endregion

namespace WinBalans.Forms
{
	partial class MainForm : Form
	{
		Logger l;
		//Parameters ParametersInstance;
		Settings s;
		//Hashtable FormList = new Hashtable();
		public MainForm()
		{
			InitializeComponent();

			//TODO: инициализация будущих коллекций и парсинг конфигов
			try
			{
				l = new Logger("log.txt");
			}
			catch (IOException e)
			{
				MessageBox.Show("Ошибка при открытии лога: \r\n\r\n" + e.Message);
				l = new Logger(null);
			}
			Config cn = Config.Load("config", l);
			PluginCollection pc = PluginCollection.FindPlugins();
			((Plugin)pc[0]).Checked = true;

			TempParameters.ParametersInstance = Parameters.Parse(cn);
			TempParameters.ParametersInstance.PluginList = pc;
			//TempParameters.ParametersInstance.PluginList = PluginCollection.Synchronize(TempParameters.ParametersInstance.PluginList, pc);
			s = new Settings(TempParameters.ParametersInstance);
			
			foreach (Plugin pl in TempParameters.ParametersInstance.PluginList.SelectedPlugins)
				TempParameters.CreateWindow(pl);
		}
		private void settingsMenuItem_Click(object sender, EventArgs e)
		{
			s.ShowDialog();
			TempParameters.ParametersInstance = s.GetParameters();
			foreach (Plugin pl in TempParameters.ParametersInstance.PluginList)
				TempParameters.CreateWindow(pl);
			//TODO: закрыть ненужные окна
		}

		private void helpMenuItem_Click(object sender, EventArgs e)
		{
			new About().ShowDialog();
		}

		private void updateAllStatMenuItem_Click(object sender, EventArgs e)
		{
			UpdateAllStat();
		}
		
		private void CreateWindow(object sender, EventArgs e)
		{
			MenuItem parent = ((MenuItem)((MenuItem)sender).Parent);
			Plugin pl = TempParameters.ParametersInstance.PluginList[parent.Text];
			TempParameters.CreateWindow(pl);
		}
		private void exitMenuItem_Click(object sender, System.EventArgs e)
		{
			//RestoreDesktop();
			//pm.Save(fp.pathToViewParameters);
			Application.Exit();
		}

		private void UpdateStat(object sender, EventArgs e)
		{
			MenuItem parent = ((MenuItem)((MenuItem)sender).Parent);
			TempParameters.ParametersInstance.PluginList[parent.Text].Update();
		}

		private void PlugProp(object sender, EventArgs e)
		{
			MenuItem parent = ((MenuItem)((MenuItem)sender).Parent);
			Plugin pl = TempParameters.ParametersInstance.PluginList[parent.Text];
			PluginProperties pf = new PluginProperties(pl);
			pf.ShowDialog(this);
			TempParameters.ParametersInstance.PluginList[TempParameters.ParametersInstance.PluginList.IndexOf(pl)] =  pf.GetPlugin();
		}

		private void selectPluginInContextMenu_Click(object sender, EventArgs e)
		{
			MenuItem _sender = (MenuItem)sender;
			int index = TempParameters.ParametersInstance.PluginList.GetIndex(((MenuItem)_sender.Parent).Text);
			foreach (MenuItem m in _sender.Parent.MenuItems)
				m.Checked = false;
			_sender.Checked = true;
			//ChangeValues(null, (Plugin)TempParameters.ParametersInstance.PluginList[index]);
		}
		private void UpdateAllStat()
		{
			foreach (Plugin pl in TempParameters.ParametersInstance.PluginList)
				pl.Update();
		}
		private void UpdateMenuList(object sender, EventArgs e)
		{
			if (TempParameters.ParametersInstance.PluginList.Count == 0)
			{
				pluginsMenuItem.Enabled = false;
				return;
			}
			else
				pluginsMenuItem.Enabled = true;
			pluginsMenuItem.MenuItems.Clear();
			foreach (Plugin pl in TempParameters.ParametersInstance.PluginList)
			{
				MenuItem m = new MenuItem(pl.Name);
					MenuItem u = new MenuItem("Обновить", new EventHandler(UpdateStat));
					MenuItem pr = new MenuItem("Свойства", new EventHandler(PlugProp));
					MenuItem cr = new MenuItem("Создать окно", new EventHandler(CreateWindow));
					MenuItem sel = new MenuItem("Кликнуть", new EventHandler(selectPluginInContextMenu_Click));
				m.Checked = pl.Checked;
				m.MenuItems.AddRange(new MenuItem[] { u, pr, cr, sel });
				pluginsMenuItem.MenuItems.Add(m);
			}
		}
		
		static void Main()
		{
			if (!Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Plugins\"))
				Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"Plugins\");
			new MainForm();
			Application.EnableVisualStyles();
			Application.DoEvents();
			Application.Run();
		}
	}
}