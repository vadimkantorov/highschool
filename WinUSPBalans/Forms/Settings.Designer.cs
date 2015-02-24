using System;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Threading;

namespace WinBalans.Forms
{
	partial class Settings
	{
		/// <summary>
	/// Summary description for Settings.
	/// </summary>
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private Button setDefaultsButton;
		private TabPage Plugs;
		private ListView pluginListView;
		private ColumnHeader nameColumnHeader2;
		private ColumnHeader descriptionColumnHeader2;
		private ColumnHeader versionColumnHeader2;
		private Button pluginPropertiesButton;
		private Button deletePluginButton;
		private Button openPluginButton;
		private TabPage formSet;
		private GroupBox groupBox1;
		private NumericUpDown intervalNumeric;
		private Label label7;
		private CheckBox timerEnabledCheckbox;
		private TabControl tabControl1;
		private Label _userDescriptionLabel;
		private PictureBox findPluginsPictureBox;
		private IContainer components;
		private Label userDescriptionLabel;
		private ToolTip userDescToolTip_;
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.okButton = new System.Windows.Forms.Button();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.cancelButton = new System.Windows.Forms.Button();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.setDefaultsButton = new System.Windows.Forms.Button();
			this.Plugs = new System.Windows.Forms.TabPage();
			this.label1 = new System.Windows.Forms.Label();
			this.findPluginsPictureBox = new System.Windows.Forms.PictureBox();
			this._userDescriptionLabel = new System.Windows.Forms.Label();
			this.userDescriptionLabel = new System.Windows.Forms.Label();
			this.pluginListView = new System.Windows.Forms.ListView();
			this.nameColumnHeader2 = new System.Windows.Forms.ColumnHeader("");
			this.descriptionColumnHeader2 = new System.Windows.Forms.ColumnHeader("");
			this.versionColumnHeader2 = new System.Windows.Forms.ColumnHeader("");
			this.pluginPropertiesButton = new System.Windows.Forms.Button();
			this.deletePluginButton = new System.Windows.Forms.Button();
			this.openPluginButton = new System.Windows.Forms.Button();
			this.formSet = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.intervalNumeric = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.timerEnabledCheckbox = new System.Windows.Forms.CheckBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.userDescToolTip_ = new System.Windows.Forms.ToolTip(this.components);
			this.Plugs.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.findPluginsPictureBox)).BeginInit();
			this.formSet.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.SuspendLayout();
// 
// okButton
// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.okButton.AutoRelocate = true;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(12, 267);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(72, 24);
			this.okButton.TabIndex = 7;
			this.okButton.Text = "Ок";
			this.okButton.Click += new System.EventHandler(this.Exit);
// 
// cancelButton
// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancelButton.AutoRelocate = true;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(108, 267);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 24);
			this.cancelButton.TabIndex = 11;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.Click += new System.EventHandler(this.Exit);
// 
// openDialog
// 
			this.openDialog.Filter = "WinBalans Plug-ins (*.dll)|*.dll|All files (*.*)|*.*";
// 
// setDefaultsButton
// 
			this.setDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.setDefaultsButton.AutoRelocate = true;
			this.setDefaultsButton.Location = new System.Drawing.Point(398, 267);
			this.setDefaultsButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.setDefaultsButton.Name = "setDefaultsButton";
			this.setDefaultsButton.Size = new System.Drawing.Size(117, 24);
			this.setDefaultsButton.TabIndex = 12;
			this.setDefaultsButton.Text = "По умолчанию";
			this.setDefaultsButton.Click += new System.EventHandler(this.setDefaultsButton_Click);
// 
// Plugs
// 
			this.Plugs.Controls.Add(this.label1);
			this.Plugs.Controls.Add(this.findPluginsPictureBox);
			this.Plugs.Controls.Add(this._userDescriptionLabel);
			this.Plugs.Controls.Add(this.userDescriptionLabel);
			this.Plugs.Controls.Add(this.pluginListView);
			this.Plugs.Controls.Add(this.pluginPropertiesButton);
			this.Plugs.Controls.Add(this.deletePluginButton);
			this.Plugs.Controls.Add(this.openPluginButton);
			this.Plugs.Location = new System.Drawing.Point(4, 22);
			this.Plugs.Name = "Plugs";
			this.Plugs.Size = new System.Drawing.Size(505, 227);
			this.Plugs.TabIndex = 3;
			this.Plugs.Text = "Подключаемые модули";
// 
// label1
// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Location = new System.Drawing.Point(375, 108);
			this.label1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 39);
			this.label1.TabIndex = 24;
			this.label1.Text = "Жмите лупу \r\nчтобы обновить \r\n     список";
// 
// findPluginsPictureBox
// 
			this.findPluginsPictureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.findPluginsPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("findPluginsPictureBox.Image")));
			this.findPluginsPictureBox.Location = new System.Drawing.Point(464, 108);
			this.findPluginsPictureBox.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			this.findPluginsPictureBox.Name = "findPluginsPictureBox";
			this.findPluginsPictureBox.Size = new System.Drawing.Size(32, 32);
			this.findPluginsPictureBox.TabIndex = 23;
			this.findPluginsPictureBox.TabStop = false;
			this.findPluginsPictureBox.Visible = false;
			this.findPluginsPictureBox.Click += new System.EventHandler(this.findPluginsPictureBox_Click);
// 
// _userDescriptionLabel
// 
			this._userDescriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this._userDescriptionLabel.AutoSize = true;
			this._userDescriptionLabel.Location = new System.Drawing.Point(375, 147);
			this._userDescriptionLabel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 1);
			this._userDescriptionLabel.Name = "_userDescriptionLabel";
			this._userDescriptionLabel.Size = new System.Drawing.Size(89, 14);
			this._userDescriptionLabel.TabIndex = 22;
			this._userDescriptionLabel.Text = "Ваше описание:";
// 
// userDescriptionLabel
// 
			this.userDescriptionLabel.AutoEllipsis = true;
			this.userDescriptionLabel.AutoRelocate = true;
			this.userDescriptionLabel.AutoSize = true;
			this.userDescriptionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.userDescriptionLabel.Location = new System.Drawing.Point(375, 165);
			this.userDescriptionLabel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.userDescriptionLabel.Name = "userDescriptionLabel";
			this.userDescriptionLabel.Size = new System.Drawing.Size(0, 0);
			this.userDescriptionLabel.TabIndex = 21;
// 
// pluginListView
// 
			this.pluginListView.AllowColumnReorder = true;
			this.pluginListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pluginListView.CheckBoxes = true;
			this.pluginListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nameColumnHeader2,
            this.descriptionColumnHeader2,
            this.versionColumnHeader2});
			this.pluginListView.Location = new System.Drawing.Point(8, 8);
			this.pluginListView.MultiSelect = false;
			this.pluginListView.Name = "pluginListView";
			this.pluginListView.Size = new System.Drawing.Size(360, 212);
			this.pluginListView.TabIndex = 20;
			this.pluginListView.View = System.Windows.Forms.View.Details;
			this.pluginListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.pluginListView_ItemCheck);
// 
// nameColumnHeader2
// 
			this.nameColumnHeader2.Text = "Название";
			this.nameColumnHeader2.Width = 80;
// 
// descriptionColumnHeader2
// 
			this.descriptionColumnHeader2.Text = "Описание разработчика";
			this.descriptionColumnHeader2.Width = 215;
// 
// versionColumnHeader2
// 
			this.versionColumnHeader2.Text = "Версия";
// 
// pluginPropertiesButton
// 
			this.pluginPropertiesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pluginPropertiesButton.Location = new System.Drawing.Point(375, 74);
			this.pluginPropertiesButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.pluginPropertiesButton.Name = "pluginPropertiesButton";
			this.pluginPropertiesButton.Size = new System.Drawing.Size(128, 32);
			this.pluginPropertiesButton.TabIndex = 19;
			this.pluginPropertiesButton.Text = "Свойства плагина";
			this.pluginPropertiesButton.Click += new System.EventHandler(this.pluginPropertiesButton_Click);
// 
// deletePluginButton
// 
			this.deletePluginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.deletePluginButton.Location = new System.Drawing.Point(375, 42);
			this.deletePluginButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.deletePluginButton.Name = "deletePluginButton";
			this.deletePluginButton.Size = new System.Drawing.Size(128, 32);
			this.deletePluginButton.TabIndex = 18;
			this.deletePluginButton.Text = "Удалить плагин";
			this.deletePluginButton.Click += new System.EventHandler(this.deletePluginButton_Click);
// 
// openPluginButton
// 
			this.openPluginButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.openPluginButton.Location = new System.Drawing.Point(375, 8);
			this.openPluginButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.openPluginButton.Name = "openPluginButton";
			this.openPluginButton.Size = new System.Drawing.Size(126, 32);
			this.openPluginButton.TabIndex = 17;
			this.openPluginButton.Text = "Открыть плагин";
			this.openPluginButton.Click += new System.EventHandler(this.openPluginButton_Click);
// 
// formSet
// 
			this.formSet.Controls.Add(this.groupBox1);
			this.formSet.Location = new System.Drawing.Point(4, 22);
			this.formSet.Name = "formSet";
			this.formSet.Size = new System.Drawing.Size(505, 227);
			this.formSet.TabIndex = 2;
			this.formSet.Text = "Основные настройки программы";
// 
// groupBox1
// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.intervalNumeric);
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.timerEnabledCheckbox);
			this.groupBox1.Location = new System.Drawing.Point(-4, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(506, 227);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Свойства основного окна";
// 
// intervalNumeric
// 
			this.intervalNumeric.Location = new System.Drawing.Point(12, 74);
			this.intervalNumeric.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
			this.intervalNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.intervalNumeric.Name = "intervalNumeric";
			this.intervalNumeric.Size = new System.Drawing.Size(72, 20);
			this.intervalNumeric.TabIndex = 21;
			this.intervalNumeric.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
			this.intervalNumeric.ValueChanged += new System.EventHandler(this.intervalNumeric_ValueChanged);
// 
// label7
// 
			this.label7.Location = new System.Drawing.Point(12, 58);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(111, 16);
			this.label7.TabIndex = 20;
			this.label7.Text = "Интервал в минутах";
// 
// timerEnabledCheckbox
// 
			this.timerEnabledCheckbox.Location = new System.Drawing.Point(12, 20);
			this.timerEnabledCheckbox.Name = "timerEnabledCheckbox";
			this.timerEnabledCheckbox.Size = new System.Drawing.Size(160, 32);
			this.timerEnabledCheckbox.TabIndex = 16;
			this.timerEnabledCheckbox.TabStop = false;
			this.timerEnabledCheckbox.Text = "Запускать получение статистики по таймеру";
// 
// tabControl1
// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.AutoRelocate = true;
			this.tabControl1.Controls.Add(this.formSet);
			this.tabControl1.Controls.Add(this.Plugs);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(513, 253);
			this.tabControl1.TabIndex = 10;
// 
// Settings
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(525, 314);
			this.ControlBox = false;
			this.Controls.Add(this.setDefaultsButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.okButton);
			this.MinimumSize = new System.Drawing.Size(533, 322);
			this.Name = "Settings";
			this.Text = "Settings";
			this.VisibleChanged += new System.EventHandler(this.Settings_VisibleChanged);
			this.Plugs.ResumeLayout(false);
			this.Plugs.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.findPluginsPictureBox)).EndInit();
			this.formSet.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.intervalNumeric)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private Label label1;
	}	
}