using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans
{
	/// <summary>
	/// Summary description for Settings.
	/// </summary>
	public class Settings : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button ColorChoose;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button FontChoose;
		private System.Windows.Forms.Button ForeColorChose;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage mainSet;
		private System.Windows.Forms.TabPage textSet;
		private System.Windows.Forms.TabPage formSet;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		
		private System.Windows.Forms.Button MyCancelButton;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button Color2Chose;
		private System.Windows.Forms.Button Color1Chose;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ColorDialog MColorDialog;
		private System.Windows.Forms.ColorDialog TextColorDialog;
		private System.Windows.Forms.ColorDialog DeskColorDialog1;
		private System.Windows.Forms.ColorDialog DeskColorDialog2;
		private System.Windows.Forms.CheckBox IsTimerCheckbox;
		private System.Windows.Forms.NumericUpDown DeskXnumeric;
		private System.Windows.Forms.NumericUpDown DeskYnumeric;
		private System.Windows.Forms.Button DeskFontChoose;
		private System.Windows.Forms.FontDialog DeskFontDialog;
		private System.Windows.Forms.NumericUpDown MOpacitynumeric;
		private System.Windows.Forms.FontDialog TextFontDialog;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.RadioButton Vertical;
		private System.Windows.Forms.RadioButton Horizontal;
		private System.Windows.Forms.RadioButton ForwardDiagonal;
		private System.Windows.Forms.RadioButton BackwardDiagonal;
		private System.Windows.Forms.TabPage Plugs;
		private System.Windows.Forms.Button DescrButton;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button DeleteButton;
		private System.Windows.Forms.ListBox MCurPluginListBox;
		private System.Windows.Forms.OpenFileDialog PluginInfoOpenDialog;
		private System.Windows.Forms.OpenFileDialog PluginOpenDialog;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.RadioButton radioButton1;
		private System.Windows.Forms.RadioButton radioButton2;
		private ArrayList pluginlist;
		private PluginInfo pi;
		private System.Windows.Forms.SaveFileDialog PluginInfoSaveDialog;
		private System.Windows.Forms.NumericUpDown intervalNumericUpDown;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Button Open_SaveButton;
		public bool Apply;
		public Settings()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			Apply = false;
			pluginlist = new ArrayList();
			pi = new PluginInfo();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.MColorDialog = new System.Windows.Forms.ColorDialog();
			this.ColorChoose = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.OkButton = new System.Windows.Forms.Button();
			this.FontChoose = new System.Windows.Forms.Button();
			this.TextFontDialog = new System.Windows.Forms.FontDialog();
			this.ForeColorChose = new System.Windows.Forms.Button();
			this.TextColorDialog = new System.Windows.Forms.ColorDialog();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.mainSet = new System.Windows.Forms.TabPage();
			this.radioButton2 = new System.Windows.Forms.RadioButton();
			this.radioButton1 = new System.Windows.Forms.RadioButton();
			this.intervalNumericUpDown = new System.Windows.Forms.NumericUpDown();
			this.label7 = new System.Windows.Forms.Label();
			this.IsTimerCheckbox = new System.Windows.Forms.CheckBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.username = new System.Windows.Forms.TextBox();
			this.textSet = new System.Windows.Forms.TabPage();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.Vertical = new System.Windows.Forms.RadioButton();
			this.Horizontal = new System.Windows.Forms.RadioButton();
			this.ForwardDiagonal = new System.Windows.Forms.RadioButton();
			this.BackwardDiagonal = new System.Windows.Forms.RadioButton();
			this.DeskFontChoose = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.Color2Chose = new System.Windows.Forms.Button();
			this.Color1Chose = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.DeskXnumeric = new System.Windows.Forms.NumericUpDown();
			this.DeskYnumeric = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.formSet = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.MOpacitynumeric = new System.Windows.Forms.NumericUpDown();
			this.Plugs = new System.Windows.Forms.TabPage();
			this.SaveButton = new System.Windows.Forms.Button();
			this.Open_SaveButton = new System.Windows.Forms.Button();
			this.DeleteButton = new System.Windows.Forms.Button();
			this.MCurPluginListBox = new System.Windows.Forms.ListBox();
			this.AddButton = new System.Windows.Forms.Button();
			this.DescrButton = new System.Windows.Forms.Button();
			this.MyCancelButton = new System.Windows.Forms.Button();
			this.DeskColorDialog1 = new System.Windows.Forms.ColorDialog();
			this.DeskColorDialog2 = new System.Windows.Forms.ColorDialog();
			this.DeskFontDialog = new System.Windows.Forms.FontDialog();
			this.PluginInfoOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.PluginOpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.PluginInfoSaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.tabControl1.SuspendLayout();
			this.mainSet.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.intervalNumericUpDown)).BeginInit();
			this.textSet.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DeskXnumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DeskYnumeric)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.formSet.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MOpacitynumeric)).BeginInit();
			this.Plugs.SuspendLayout();
			this.SuspendLayout();
			// 
			// ColorChoose
			// 
			this.ColorChoose.Location = new System.Drawing.Point(16, 72);
			this.ColorChoose.Name = "ColorChoose";
			this.ColorChoose.Size = new System.Drawing.Size(120, 24);
			this.ColorChoose.TabIndex = 4;
			this.ColorChoose.Text = "������� ����";
			this.ColorChoose.Click += new System.EventHandler(this.ColorChoose_Click);
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(24, 24);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 5;
			this.label3.Text = "������������";
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(304, 256);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(72, 24);
			this.OkButton.TabIndex = 7;
			this.OkButton.Text = "Ok";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// FontChoose
			// 
			this.FontChoose.Location = new System.Drawing.Point(8, 32);
			this.FontChoose.Name = "FontChoose";
			this.FontChoose.Size = new System.Drawing.Size(136, 24);
			this.FontChoose.TabIndex = 8;
			this.FontChoose.Text = "������� ����� ������";
			this.FontChoose.Click += new System.EventHandler(this.FontChoose_Click);
			// 
			// ForeColorChose
			// 
			this.ForeColorChose.Location = new System.Drawing.Point(8, 64);
			this.ForeColorChose.Name = "ForeColorChose";
			this.ForeColorChose.Size = new System.Drawing.Size(136, 24);
			this.ForeColorChose.TabIndex = 9;
			this.ForeColorChose.Text = "������� ���� ������";
			this.ForeColorChose.Click += new System.EventHandler(this.ForeColorChose_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.mainSet);
			this.tabControl1.Controls.Add(this.textSet);
			this.tabControl1.Controls.Add(this.formSet);
			this.tabControl1.Controls.Add(this.Plugs);
			this.tabControl1.Location = new System.Drawing.Point(8, 8);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(728, 240);
			this.tabControl1.TabIndex = 10;
			// 
			// mainSet
			// 
			this.mainSet.Controls.Add(this.radioButton2);
			this.mainSet.Controls.Add(this.radioButton1);
			this.mainSet.Controls.Add(this.intervalNumericUpDown);
			this.mainSet.Controls.Add(this.label7);
			this.mainSet.Controls.Add(this.IsTimerCheckbox);
			this.mainSet.Controls.Add(this.label1);
			this.mainSet.Controls.Add(this.label2);
			this.mainSet.Controls.Add(this.password);
			this.mainSet.Controls.Add(this.username);
			this.mainSet.Location = new System.Drawing.Point(4, 22);
			this.mainSet.Name = "mainSet";
			this.mainSet.Size = new System.Drawing.Size(720, 214);
			this.mainSet.TabIndex = 0;
			this.mainSet.Text = "��������� �������";
			// 
			// radioButton2
			// 
			this.radioButton2.Location = new System.Drawing.Point(216, 72);
			this.radioButton2.Name = "radioButton2";
			this.radioButton2.Size = new System.Drawing.Size(80, 16);
			this.radioButton2.TabIndex = 19;
			this.radioButton2.Text = "�������";
			// 
			// radioButton1
			// 
			this.radioButton1.Location = new System.Drawing.Point(216, 48);
			this.radioButton1.Name = "radioButton1";
			this.radioButton1.Size = new System.Drawing.Size(80, 16);
			this.radioButton1.TabIndex = 18;
			this.radioButton1.Text = "��������";
			// 
			// intervalNumericUpDown
			// 
			this.intervalNumericUpDown.Location = new System.Drawing.Point(136, 64);
			this.intervalNumericUpDown.Maximum = new System.Decimal(new int[] {
																				  10000,
																				  0,
																				  0,
																				  0});
			this.intervalNumericUpDown.Minimum = new System.Decimal(new int[] {
																				  3,
																				  0,
																				  0,
																				  0});
			this.intervalNumericUpDown.Name = "intervalNumericUpDown";
			this.intervalNumericUpDown.Size = new System.Drawing.Size(72, 20);
			this.intervalNumericUpDown.TabIndex = 17;
			this.intervalNumericUpDown.Value = new System.Decimal(new int[] {
																				3,
																				0,
																				0,
																				0});
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(136, 48);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(64, 16);
			this.label7.TabIndex = 16;
			this.label7.Text = "�������� �";
			// 
			// IsTimerCheckbox
			// 
			this.IsTimerCheckbox.Location = new System.Drawing.Point(136, 8);
			this.IsTimerCheckbox.Name = "IsTimerCheckbox";
			this.IsTimerCheckbox.Size = new System.Drawing.Size(160, 32);
			this.IsTimerCheckbox.TabIndex = 15;
			this.IsTimerCheckbox.Text = "��������� ��������� ���������� �� �������";
			this.IsTimerCheckbox.CheckedChanged += new System.EventHandler(this.IsTimerCheckbox_CheckedChanged);
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(24, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 11;
			this.label1.Text = "��� ������������";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(24, 56);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 12;
			this.label2.Text = "������";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(24, 80);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(80, 20);
			this.password.TabIndex = 14;
			this.password.Text = "";
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point(24, 32);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(80, 20);
			this.username.TabIndex = 13;
			this.username.Text = "";
			// 
			// textSet
			// 
			this.textSet.Controls.Add(this.groupBox2);
			this.textSet.Controls.Add(this.groupBox3);
			this.textSet.Location = new System.Drawing.Point(4, 22);
			this.textSet.Name = "textSet";
			this.textSet.Size = new System.Drawing.Size(720, 214);
			this.textSet.TabIndex = 1;
			this.textSet.Text = "�������� ������";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.Vertical);
			this.groupBox2.Controls.Add(this.Horizontal);
			this.groupBox2.Controls.Add(this.ForwardDiagonal);
			this.groupBox2.Controls.Add(this.BackwardDiagonal);
			this.groupBox2.Controls.Add(this.DeskFontChoose);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.Color2Chose);
			this.groupBox2.Controls.Add(this.Color1Chose);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.DeskXnumeric);
			this.groupBox2.Controls.Add(this.DeskYnumeric);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Location = new System.Drawing.Point(16, 16);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(528, 192);
			this.groupBox2.TabIndex = 14;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "�������� ������� �� ������� �����";
			// 
			// Vertical
			// 
			this.Vertical.Location = new System.Drawing.Point(392, 120);
			this.Vertical.Name = "Vertical";
			this.Vertical.Size = new System.Drawing.Size(88, 24);
			this.Vertical.TabIndex = 19;
			this.Vertical.Text = "Vertical";
			// 
			// Horizontal
			// 
			this.Horizontal.Location = new System.Drawing.Point(392, 96);
			this.Horizontal.Name = "Horizontal";
			this.Horizontal.Size = new System.Drawing.Size(88, 16);
			this.Horizontal.TabIndex = 18;
			this.Horizontal.Text = "Horizontal";
			// 
			// ForwardDiagonal
			// 
			this.ForwardDiagonal.Location = new System.Drawing.Point(392, 64);
			this.ForwardDiagonal.Name = "ForwardDiagonal";
			this.ForwardDiagonal.Size = new System.Drawing.Size(112, 24);
			this.ForwardDiagonal.TabIndex = 17;
			this.ForwardDiagonal.Text = "ForwardDiagonal";
			// 
			// BackwardDiagonal
			// 
			this.BackwardDiagonal.Location = new System.Drawing.Point(392, 40);
			this.BackwardDiagonal.Name = "BackwardDiagonal";
			this.BackwardDiagonal.Size = new System.Drawing.Size(120, 24);
			this.BackwardDiagonal.TabIndex = 16;
			this.BackwardDiagonal.Text = "BackwardDiagonal";
			// 
			// DeskFontChoose
			// 
			this.DeskFontChoose.Location = new System.Drawing.Point(8, 112);
			this.DeskFontChoose.Name = "DeskFontChoose";
			this.DeskFontChoose.Size = new System.Drawing.Size(120, 24);
			this.DeskFontChoose.TabIndex = 15;
			this.DeskFontChoose.Text = "������� �����";
			this.DeskFontChoose.Click += new System.EventHandler(this.DeskFontChoose_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(392, 16);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 16);
			this.label6.TabIndex = 14;
			this.label6.Text = "���������� �������";
			// 
			// Color2Chose
			// 
			this.Color2Chose.Location = new System.Drawing.Point(288, 48);
			this.Color2Chose.Name = "Color2Chose";
			this.Color2Chose.Size = new System.Drawing.Size(88, 24);
			this.Color2Chose.TabIndex = 12;
			this.Color2Chose.Text = "���� 2";
			this.Color2Chose.Click += new System.EventHandler(this.Color2Chose_Click);
			// 
			// Color1Chose
			// 
			this.Color1Chose.Location = new System.Drawing.Point(288, 16);
			this.Color1Chose.Name = "Color1Chose";
			this.Color1Chose.Size = new System.Drawing.Size(88, 24);
			this.Color1Chose.TabIndex = 11;
			this.Color1Chose.Text = "���� 1";
			this.Color1Chose.Click += new System.EventHandler(this.Color1Chose_Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(280, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "X ������ �������� ���� ������� �� ������� �����";
			// 
			// DeskXnumeric
			// 
			this.DeskXnumeric.Location = new System.Drawing.Point(8, 32);
			this.DeskXnumeric.Maximum = new System.Decimal(new int[] {
																		 1024,
																		 0,
																		 0,
																		 0});
			this.DeskXnumeric.Name = "DeskXnumeric";
			this.DeskXnumeric.TabIndex = 7;
			// 
			// DeskYnumeric
			// 
			this.DeskYnumeric.Location = new System.Drawing.Point(8, 72);
			this.DeskYnumeric.Maximum = new System.Decimal(new int[] {
																		 768,
																		 0,
																		 0,
																		 0});
			this.DeskYnumeric.Name = "DeskYnumeric";
			this.DeskYnumeric.TabIndex = 8;
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(8, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(272, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Y ������ �������� ���� ������� �� ������� �����";
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.FontChoose);
			this.groupBox3.Controls.Add(this.ForeColorChose);
			this.groupBox3.Location = new System.Drawing.Point(552, 16);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(152, 192);
			this.groupBox3.TabIndex = 15;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "�������� ������� �� ������� ����";
			// 
			// formSet
			// 
			this.formSet.Controls.Add(this.groupBox1);
			this.formSet.Location = new System.Drawing.Point(4, 22);
			this.formSet.Name = "formSet";
			this.formSet.Size = new System.Drawing.Size(720, 214);
			this.formSet.TabIndex = 2;
			this.formSet.Text = "��������� ��������� ����";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.MOpacitynumeric);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.ColorChoose);
			this.groupBox1.Location = new System.Drawing.Point(8, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(704, 200);
			this.groupBox1.TabIndex = 12;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "�������� ��������� ����";
			// 
			// MOpacitynumeric
			// 
			this.MOpacitynumeric.Increment = new System.Decimal(new int[] {
																			  1,
																			  0,
																			  0,
																			  65536});
			this.MOpacitynumeric.Location = new System.Drawing.Point(16, 40);
			this.MOpacitynumeric.Maximum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			0});
			this.MOpacitynumeric.Minimum = new System.Decimal(new int[] {
																			1,
																			0,
																			0,
																			65536});
			this.MOpacitynumeric.Name = "MOpacitynumeric";
			this.MOpacitynumeric.Size = new System.Drawing.Size(112, 20);
			this.MOpacitynumeric.TabIndex = 6;
			this.MOpacitynumeric.Value = new System.Decimal(new int[] {
																		  1,
																		  0,
																		  0,
																		  65536});
			// 
			// Plugs
			// 
			this.Plugs.Controls.Add(this.SaveButton);
			this.Plugs.Controls.Add(this.Open_SaveButton);
			this.Plugs.Controls.Add(this.DeleteButton);
			this.Plugs.Controls.Add(this.MCurPluginListBox);
			this.Plugs.Controls.Add(this.AddButton);
			this.Plugs.Controls.Add(this.DescrButton);
			this.Plugs.Location = new System.Drawing.Point(4, 22);
			this.Plugs.Name = "Plugs";
			this.Plugs.Size = new System.Drawing.Size(720, 214);
			this.Plugs.TabIndex = 3;
			this.Plugs.Text = "������������ ������";
			// 
			// SaveButton
			// 
			this.SaveButton.Location = new System.Drawing.Point(512, 176);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(200, 32);
			this.SaveButton.TabIndex = 18;
			this.SaveButton.Text = "��������� ���������";
			this.SaveButton.Click += new System.EventHandler(this.button2_Click);
			// 
			// Open_SaveButton
			// 
			this.Open_SaveButton.Location = new System.Drawing.Point(512, 136);
			this.Open_SaveButton.Name = "Open_SaveButton";
			this.Open_SaveButton.Size = new System.Drawing.Size(200, 32);
			this.Open_SaveButton.TabIndex = 17;
			this.Open_SaveButton.Text = "������� ������ � ������� ��� ���������";
			this.Open_SaveButton.Click += new System.EventHandler(this.button1_Click);
			// 
			// DeleteButton
			// 
			this.DeleteButton.Location = new System.Drawing.Point(512, 96);
			this.DeleteButton.Name = "DeleteButton";
			this.DeleteButton.Size = new System.Drawing.Size(200, 32);
			this.DeleteButton.TabIndex = 16;
			this.DeleteButton.Text = "������� ��������� �������";
			this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
			// 
			// MCurPluginListBox
			// 
			this.MCurPluginListBox.Location = new System.Drawing.Point(16, 8);
			this.MCurPluginListBox.Name = "MCurPluginListBox";
			this.MCurPluginListBox.Size = new System.Drawing.Size(488, 199);
			this.MCurPluginListBox.Sorted = true;
			this.MCurPluginListBox.TabIndex = 12;
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(512, 16);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(200, 32);
			this.AddButton.TabIndex = 13;
			this.AddButton.Text = "�������� ��������� �������";
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// DescrButton
			// 
			this.DescrButton.Location = new System.Drawing.Point(512, 56);
			this.DescrButton.Name = "DescrButton";
			this.DescrButton.Size = new System.Drawing.Size(200, 32);
			this.DescrButton.TabIndex = 15;
			this.DescrButton.Text = "�������� ������� � ��� ���������";
			// 
			// MyCancelButton
			// 
			this.MyCancelButton.Location = new System.Drawing.Point(400, 256);
			this.MyCancelButton.Name = "MyCancelButton";
			this.MyCancelButton.Size = new System.Drawing.Size(72, 24);
			this.MyCancelButton.TabIndex = 11;
			this.MyCancelButton.Text = "������";
			this.MyCancelButton.Click += new System.EventHandler(this.MyCancelButton_Click);
			// 
			// PluginInfoOpenDialog
			// 
			this.PluginInfoOpenDialog.Filter = "WinBalans Plug-ins (*.dll)|*.dll|All files (*.*)|*.*";
			// 
			// PluginOpenDialog
			// 
			this.PluginOpenDialog.Filter = "WinBalans Plug-ins (*.dll)|*.dll|All files (*.*)|*.*";
			// 
			// Settings
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(746, 288);
			this.ControlBox = false;
			this.Controls.Add(this.MyCancelButton);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.OkButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Settings";
			this.Text = "Settings";
			this.tabControl1.ResumeLayout(false);
			this.mainSet.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.intervalNumericUpDown)).EndInit();
			this.textSet.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DeskXnumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DeskYnumeric)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.formSet.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MOpacitynumeric)).EndInit();
			this.Plugs.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
		public void ChangeValues(ViewParameters vp, PluginInfo pi)
		{
			pluginlist.Add(pi);
			UpdateList();
			this.MCurPluginListBox.SelectedItem = pi.pathToAssembly;
			this.username.Text = pi.username;
			this.password.Text = pi.password;
			this.IsTimerCheckbox.Checked = Convert.ToBoolean(pi.timerEnabled);
			this.intervalNumericUpDown.Value = (decimal)pi.interval;
			
			this.MOpacitynumeric.Value = (decimal)vp.MOpacity;
			this.MColorDialog.Color = vp.MColor;
			
			this.DeskColorDialog1.Color = vp.DeskColor1;
			this.DeskColorDialog2.Color = vp.DeskColor2;
			this.DeskXnumeric.Value = (decimal)vp.DeskX;
			this.DeskYnumeric.Value = (decimal)vp.DeskY;
			this.DeskFontDialog.Font = vp.DeskFont;
			
			this.TextColorDialog.Color = vp.TextColor;
			this.TextFontDialog.Font = vp.TextFont;
			if(vp.DeskEffect == LinearGradientMode.BackwardDiagonal)
				this.BackwardDiagonal.Checked = true;
			if(vp.DeskEffect == LinearGradientMode.ForwardDiagonal)
				this.ForwardDiagonal.Checked = true;
			if(vp.DeskEffect == LinearGradientMode.Horizontal)
				this.Horizontal.Checked = true;
			if(vp.DeskEffect == LinearGradientMode.Vertical)
				this.Vertical.Checked = true;
		
			
		}
		public ViewParameters GetViewParameters()
		{
			ViewParameters pr = new ViewParameters();
			pr.MOpacity		= (double)this.MOpacitynumeric.Value;
			pr.MColor		= this.MColorDialog.Color;
			
			pr.DeskColor1	= this.DeskColorDialog1.Color;
			pr.DeskColor2	= this.DeskColorDialog2.Color;
			pr.DeskX		= (float)this.DeskXnumeric.Value;
			pr.DeskY		= (float)this.DeskYnumeric.Value;
			pr.DeskFont		= this.DeskFontDialog.Font;
			if(this.BackwardDiagonal.Checked)
				pr.DeskEffect = LinearGradientMode.BackwardDiagonal;
			if(this.ForwardDiagonal.Checked)
				pr.DeskEffect = LinearGradientMode.ForwardDiagonal;
			if(this.Horizontal.Checked)
				pr.DeskEffect = LinearGradientMode.Horizontal;
			if(this.Vertical.Checked)
				pr.DeskEffect = LinearGradientMode.Vertical;
			
			pr.TextColor	= this.TextColorDialog.Color;
			pr.TextFont		= this.TextFontDialog.Font;
			return pr;
		}
		public PluginInfo GetPluginInfo()
		{
			PluginInfo pi = new PluginInfo();
				pi.pathToAssembly = MCurPluginListBox.SelectedItem.ToString();
				pi.timerEnabled = Convert.ToInt32(this.IsTimerCheckbox.Checked);
				pi.username = username.Text;
				pi.password = password.Text;
				pi.interval = (double)intervalNumericUpDown.Value;
			return pi;
		}
		private void ColorChoose_Click(object sender, System.EventArgs e)
		{
			this.MColorDialog.ShowDialog();
		}

		private void OkButton_Click(object sender, System.EventArgs e)
		{
			this.Apply = true;
			this.Hide();
		}

		private void FontChoose_Click(object sender, System.EventArgs e)
		{
			this.TextFontDialog.ShowDialog();
		}

		private void ForeColorChose_Click(object sender, System.EventArgs e)
		{
			this.TextColorDialog.ShowDialog();
		}

		private void MyCancelButton_Click(object sender, System.EventArgs e)
		{
			this.Apply = false;
			this.Hide();
		}

		private void Color1Chose_Click(object sender, System.EventArgs e)
		{
			this.DeskColorDialog1.ShowDialog();
		}

		private void Color2Chose_Click(object sender, System.EventArgs e)
		{
			this.DeskColorDialog2.ShowDialog();
		}

		private void DeskFontChoose_Click(object sender, System.EventArgs e)
		{
			this.DeskFontDialog.ShowDialog();
		}

		private void AddButton_Click(object sender, System.EventArgs e)
		{
			PluginInfoOpenDialog.ShowDialog();
			pluginlist.Add(PluginInfo.Load(PluginInfoOpenDialog.FileName));
			UpdateList();
		}
		private void UpdateList()
		{
			foreach(PluginInfo p in pluginlist)
				MCurPluginListBox.Items.Add(p.pathToAssembly);
		}

		private void DeleteButton_Click(object sender, System.EventArgs e)
		{
			if(MCurPluginListBox.Items != null)
                pluginlist.Remove(MCurPluginListBox.SelectedItem.ToString());
			UpdateList();
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			PluginOpenDialog.ShowDialog();
			string file = PluginOpenDialog.FileName;
			string dir = PluginOpenDialog.InitialDirectory;
			
			PluginInfo pl = new PluginInfo(file);
			pluginlist.Add(pl);
			UpdateList();
		}

		private void IsTimerCheckbox_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.IsTimerCheckbox.Checked == true)
			{
				intervalNumericUpDown.Enabled = true;
				radioButton1.Enabled = true;
				radioButton2.Enabled = true;
			}
			else
			{
				intervalNumericUpDown.Enabled = false;
				radioButton1.Enabled = false;
				radioButton2.Enabled = false;
			}
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			this.PluginInfoSaveDialog.ShowDialog();
			string filename = PluginInfoSaveDialog.FileName;
		}
	}
}
