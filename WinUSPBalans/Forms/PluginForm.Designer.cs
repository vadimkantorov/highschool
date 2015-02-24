using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WinBalans.Forms
{
	partial class PluginForm
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
		private void InitializeComponent()
		{
			this.statLabel = new System.Windows.Forms.Label();
			this.contextMenu1 = new System.Windows.Forms.ContextMenu();
			this.updateStatMenuItem = new System.Windows.Forms.MenuItem();
			this.closeFormMenuItem = new System.Windows.Forms.MenuItem();
			this.settingsMenuItem = new System.Windows.Forms.MenuItem();
			this.paintdMenuItem = new System.Windows.Forms.MenuItem();
			this.changeBordersMenuItem = new System.Windows.Forms.MenuItem();
			this.menuItem1 = new System.Windows.Forms.MenuItem();
			this.pluginsMenuItem = new System.Windows.Forms.MenuItem();
			this.exitMenuItem = new System.Windows.Forms.MenuItem();
			this.updateAllStatMenuItem = new System.Windows.Forms.MenuItem();
			this.helpMenuItem = new System.Windows.Forms.MenuItem();
			this.moveButton = new System.Windows.Forms.Button();
			this.exitButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
// 
// statLabel
// 
			this.statLabel.BackColor = System.Drawing.Color.Transparent;
			this.statLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.statLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.statLabel.Location = new System.Drawing.Point(-2, 16);
			this.statLabel.Margin = new System.Windows.Forms.Padding(1, 2, 3, 3);
			this.statLabel.Name = "statLabel";
			this.statLabel.Size = new System.Drawing.Size(147, 36);
			this.statLabel.TabIndex = 1;
			this.statLabel.Text = "Здесь появится состояние счёта";
			this.statLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.statLabel.DoubleClick += new System.EventHandler(this.updateAllStatMenuItem_Click);
// 
// contextMenu1
// 
			this.contextMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.updateStatMenuItem,
            this.closeFormMenuItem,
            this.settingsMenuItem,
            this.paintdMenuItem,
            this.changeBordersMenuItem,
            this.menuItem1,
            this.pluginsMenuItem,
            this.exitMenuItem,
            this.updateAllStatMenuItem,
            this.helpMenuItem});
			this.contextMenu1.Name = "contextMenu1";
			this.contextMenu1.Popup += new System.EventHandler(this.UpdateMenuList);
// 
// updateStatMenuItem
// 
			this.updateStatMenuItem.Index = 0;
			this.updateStatMenuItem.Name = "updateStatMenuItem";
			this.updateStatMenuItem.Text = "Получить статистику(этого плагина)";
			this.updateStatMenuItem.Click += new System.EventHandler(this.updateStatMenuItem_Click);
// 
// closeFormMenuItem
// 
			this.closeFormMenuItem.Index = 1;
			this.closeFormMenuItem.Name = "closeFormMenuItem";
			this.closeFormMenuItem.Text = "Закрыть это окно";
			this.closeFormMenuItem.Click += new System.EventHandler(this.closeFormMenuItem_Click);
// 
// settingsMenuItem
// 
			this.settingsMenuItem.Index = 2;
			this.settingsMenuItem.Name = "settingsMenuItem";
			this.settingsMenuItem.Text = "Параметры";
			this.settingsMenuItem.Click += new System.EventHandler(this.setUIParametersMenuItem_Click);
// 
// paintdMenuItem
// 
			this.paintdMenuItem.Index = 3;
			this.paintdMenuItem.Name = "paintdMenuItem";
			this.paintdMenuItem.Text = "Перерисовать рабочий стол";
			this.paintdMenuItem.Click += new System.EventHandler(this.paintdMenuItem_Click);
// 
// changeBordersMenuItem
// 
			this.changeBordersMenuItem.Index = 4;
			this.changeBordersMenuItem.Name = "changeBordersMenuItem";
			this.changeBordersMenuItem.Text = "Сменить границы";
			this.changeBordersMenuItem.Click += new System.EventHandler(this.changeBordersMenuItem_Click);
// 
// menuItem1
// 
			this.menuItem1.Index = 5;
			this.menuItem1.Name = "menuItem1";
			this.menuItem1.Text = "-";
// 
// pluginsMenuItem
// 
			this.pluginsMenuItem.Index = 6;
			this.pluginsMenuItem.Name = "pluginsMenuItem";
			this.pluginsMenuItem.Text = "Плагины";
// 
// exitMenuItem
// 
			this.exitMenuItem.Index = 7;
			this.exitMenuItem.Name = "exitMenuItem";
			this.exitMenuItem.Text = "Выход";
			this.exitMenuItem.Click += new System.EventHandler(this.exitMenuItem_Click);
// 
// updateAllStatMenuItem
// 
			this.updateAllStatMenuItem.Index = 8;
			this.updateAllStatMenuItem.Name = "updateAllStatMenuItem";
			this.updateAllStatMenuItem.Text = "Получить статистику(всех плагинов)";
			this.updateAllStatMenuItem.Click += new System.EventHandler(this.updateAllStatMenuItem_Click);
// 
// helpMenuItem
// 
			this.helpMenuItem.Index = 9;
			this.helpMenuItem.Name = "helpMenuItem";
			this.helpMenuItem.Text = "Справка";
			this.helpMenuItem.Click += new System.EventHandler(this.helpMenuItem_Click);
// 
// moveButton
// 
			this.moveButton.BackColor = System.Drawing.Color.DarkGoldenrod;
			this.moveButton.BorderColor = System.Drawing.Color.DarkGoldenrod;
			this.moveButton.Cursor = System.Windows.Forms.Cursors.SizeAll;
			this.moveButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.moveButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.moveButton.Location = new System.Drawing.Point(0, 0);
			this.moveButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 1);
			this.moveButton.MouseOverBackColor = System.Drawing.Color.DarkGoldenrod;
			this.moveButton.Name = "moveButton";
			this.moveButton.Size = new System.Drawing.Size(10, 10);
			this.moveButton.TabIndex = 3;
			this.moveButton.MouseUp += new System.Windows.Forms.MouseEventHandler(this.moveButton_MouseUp);
			this.moveButton.MouseMove += new System.Windows.Forms.MouseEventHandler(this.moveButton_MouseMove);
			this.moveButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.moveButton_MouseDown);
// 
// exitButton
// 
			this.exitButton.Location = new System.Drawing.Point(116, 2);
			this.exitButton.Name = "exitButton";
			this.exitButton.Size = new System.Drawing.Size(19, 14);
			this.exitButton.TabIndex = 4;
			this.exitButton.Text = "X";
			this.exitButton.Click += new System.EventHandler(this.closeFormMenuItem_Click);
// 
// PluginForm
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.Brown;
			this.ClientSize = new System.Drawing.Size(137, 53);
			this.ContextMenu = this.contextMenu1;
			this.ControlBox = false;
			this.Controls.Add(this.exitButton);
			this.Controls.Add(this.moveButton);
			this.Controls.Add(this.statLabel);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.Name = "PluginForm";
			this.Opacity = 0.5;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "WinUSPBalans 1.0";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		private System.Windows.Forms.Label statLabel;
		private System.Windows.Forms.ContextMenu contextMenu1;
		private System.Windows.Forms.MenuItem settingsMenuItem;
		private System.Windows.Forms.MenuItem helpMenuItem;
		private System.Windows.Forms.MenuItem exitMenuItem;
		private System.Windows.Forms.MenuItem pluginsMenuItem;
		private System.Windows.Forms.MenuItem changeBordersMenuItem;
		private Button moveButton;
		private Button exitButton;
		private MenuItem menuItem1;
		private MenuItem paintdMenuItem;
		private MenuItem closeFormMenuItem;
		private MenuItem updateAllStatMenuItem;
		private MenuItem updateStatMenuItem;
	}
}