using WinBalans;
namespace WinBalans.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.settingsMenuItem = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.updateAllStatMenuItem = new System.Windows.Forms.MenuItem();
			this.pluginsMenuItem = new System.Windows.Forms.MenuItem();
// 
// notifyIcon1
// 
			this.notifyIcon1.ContextMenu = this.contextMenu1;
			this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
			this.notifyIcon1.Text = "WinUSPBalans";
			this.notifyIcon1.Visible = true;
// 
// contextMenu1
// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.settingsMenuItem,
            this.helpMenuItem,
            this.exitMenuItem,
            this.updateAllStatMenuItem,
            this.pluginsMenuItem});
			this.contextMenu1.Name = "contextMenu1";
			this.contextMenu1.Popup += new System.EventHandler(this.UpdateMenuList);
// 
// settingsMenuItem
// 
			this.settingsMenuItem.Index = 0;
			this.settingsMenuItem.Name = "settingsMenuItem";
			this.settingsMenuItem.Text = "Настройки";
			this.settingsMenuItem.Click += new System.EventHandler(this.settingsMenuItem_Click);
// 
// helpMenuItem
// 
			this.helpMenuItem.Index = 1;
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.Text = "Справка";
			this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
// 
// exitMenuItem
// 
			this.exitMenuItem.Index = 2;
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Text = "Выход";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
// 
// updateAllStatMenuItem
// 
			this.updateAllStatMenuItem.Index = 3;
			this.updateAllStatMenuItem.Name = "updateAllStatMenuItem";
			this.updateAllStatMenuItem.Text = "Получить статистику";
			this.updateAllStatMenuItem.Click += new System.EventHandler(this.updateAllStatMenuItem_Click);
// 
// pluginsMenuItem
// 
			this.pluginsMenuItem.Index = 4;
			this.pluginsMenuItem.Name = "pluginsMenuItem";
			this.pluginsMenuItem.Text = "Плагины";
// 
// MainForm
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(110, 15);
			this.Name = "MainForm";
			this.ShowInTaskbar = false;
			this.Text = "MainForm";

		}

		#endregion

		private System.Windows.Forms.NotifyIcon notifyIcon1;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem settingsMenuItem;
		private System.Windows.Forms.MenuItem helpMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem updateAllStatMenuItem;
		private System.Windows.Forms.MenuItem pluginsMenuItem;
	}
}