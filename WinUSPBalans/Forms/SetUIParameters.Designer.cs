namespace WinBalans.Forms
{
	partial class SetUIParameters
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
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.desktopLabelEffectListBox = new System.Windows.Forms.ListBox();
			this.desktopChooseFontButton = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.color2ChooseButton = new System.Windows.Forms.Button();
			this.color1ChooseButton = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.deskXnumeric = new System.Windows.Forms.NumericUpDown();
			this.deskYnumeric = new System.Windows.Forms.NumericUpDown();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.mainTextFontChooseButton = new System.Windows.Forms.Button();
			this.mainTextColorChooseButton = new System.Windows.Forms.Button();
			this.opacityNumeric = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.colorChooseButton = new System.Windows.Forms.Button();
			this.setDefaultsButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.openDialog = new System.Windows.Forms.OpenFileDialog();
			this.saveDialog = new System.Windows.Forms.SaveFileDialog();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.fontDialog = new System.Windows.Forms.FontDialog();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.deskXnumeric)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.deskYnumeric)).BeginInit();
			this.groupBox3.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.opacityNumeric)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
// 
// groupBox2
// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.desktopLabelEffectListBox);
			this.groupBox2.Controls.Add(this.desktopChooseFontButton);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.color2ChooseButton);
			this.groupBox2.Controls.Add(this.color1ChooseButton);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.deskXnumeric);
			this.groupBox2.Controls.Add(this.deskYnumeric);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Location = new System.Drawing.Point(0, 0);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 3, 1, 2);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(282, 183);
			this.groupBox2.TabIndex = 16;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Параметры надписи на рабочем столе";
// 
// desktopLabelEffectListBox
// 
			this.desktopLabelEffectListBox.FormattingEnabled = true;
			this.desktopLabelEffectListBox.Items.AddRange(new object[] {
            "BackwardDiagonal",
            "ForwardDiagonal",
            "Horizontal",
            "Vertical"});
			this.desktopLabelEffectListBox.Location = new System.Drawing.Point(156, 121);
			this.desktopLabelEffectListBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
			this.desktopLabelEffectListBox.Name = "desktopLabelEffectListBox";
			this.desktopLabelEffectListBox.Size = new System.Drawing.Size(112, 56);
			this.desktopLabelEffectListBox.TabIndex = 20;
			this.desktopLabelEffectListBox.SelectedIndexChanged += new System.EventHandler(this.desktopLabelEffectListBox_SelectedIndexChanged);
// 
// desktopChooseFontButton
// 
			this.desktopChooseFontButton.Location = new System.Drawing.Point(8, 104);
			this.desktopChooseFontButton.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.desktopChooseFontButton.Name = "desktopChooseFontButton";
			this.desktopChooseFontButton.Size = new System.Drawing.Size(112, 32);
			this.desktopChooseFontButton.TabIndex = 15;
			this.desktopChooseFontButton.Text = "Выбрать шрифт";
			this.desktopChooseFontButton.Click += new System.EventHandler(this.chooseDeskFontButton_Click);
// 
// label6
// 
			this.label6.Location = new System.Drawing.Point(156, 101);
			this.label6.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(120, 16);
			this.label6.TabIndex = 14;
			this.label6.Text = "Спецэффект надписи";
// 
// color2ChooseButton
// 
			this.color2ChooseButton.Location = new System.Drawing.Point(67, 141);
			this.color2ChooseButton.Name = "color2ChooseButton";
			this.color2ChooseButton.Size = new System.Drawing.Size(53, 35);
			this.color2ChooseButton.TabIndex = 12;
			this.color2ChooseButton.Text = "Цвет 2";
			this.color2ChooseButton.Click += new System.EventHandler(this.color2Chose_Click);
// 
// color1ChooseButton
// 
			this.color1ChooseButton.BackColor = System.Drawing.SystemColors.Control;
			this.color1ChooseButton.ForeColor = System.Drawing.SystemColors.ControlText;
			this.color1ChooseButton.Location = new System.Drawing.Point(7, 140);
			this.color1ChooseButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.color1ChooseButton.Name = "color1ChooseButton";
			this.color1ChooseButton.Size = new System.Drawing.Size(53, 35);
			this.color1ChooseButton.TabIndex = 11;
			this.color1ChooseButton.Text = "Цвет 1";
			this.color1ChooseButton.Click += new System.EventHandler(this.color1Chose_Click_1);
// 
// label4
// 
			this.label4.Location = new System.Drawing.Point(8, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(271, 16);
			this.label4.TabIndex = 9;
			this.label4.Text = "X левого верхнего угла надписи на рабочем столе";
// 
// deskXnumeric
// 
			this.deskXnumeric.Location = new System.Drawing.Point(8, 32);
			this.deskXnumeric.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.deskXnumeric.Name = "deskXnumeric";
			this.deskXnumeric.Size = new System.Drawing.Size(112, 20);
			this.deskXnumeric.TabIndex = 7;
			this.deskXnumeric.ValueChanged += new System.EventHandler(this.deskXnumeric_ValueChanged);
// 
// deskYnumeric
// 
			this.deskYnumeric.Location = new System.Drawing.Point(8, 72);
			this.deskYnumeric.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.deskYnumeric.Maximum = new decimal(new int[] {
            768,
            0,
            0,
            0});
			this.deskYnumeric.Name = "deskYnumeric";
			this.deskYnumeric.Size = new System.Drawing.Size(112, 20);
			this.deskYnumeric.TabIndex = 8;
			this.deskYnumeric.ValueChanged += new System.EventHandler(this.deskYnumeric_ValueChanged);
// 
// label5
// 
			this.label5.Location = new System.Drawing.Point(8, 56);
			this.label5.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(271, 13);
			this.label5.TabIndex = 10;
			this.label5.Text = "Y левого верхнего угла надписи на рабочем столе";
// 
// groupBox3
// 
			this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox3.Controls.Add(this.mainTextFontChooseButton);
			this.groupBox3.Controls.Add(this.mainTextColorChooseButton);
			this.groupBox3.Location = new System.Drawing.Point(282, 0);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 2);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(213, 80);
			this.groupBox3.TabIndex = 17;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Параметры надписи на главном окне";
// 
// mainTextFontChooseButton
// 
			this.mainTextFontChooseButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.mainTextFontChooseButton.Location = new System.Drawing.Point(7, 13);
			this.mainTextFontChooseButton.Name = "mainTextFontChooseButton";
			this.mainTextFontChooseButton.Size = new System.Drawing.Size(136, 33);
			this.mainTextFontChooseButton.TabIndex = 8;
			this.mainTextFontChooseButton.Text = "Выбрать шрифт текста";
			this.mainTextFontChooseButton.Click += new System.EventHandler(this.mainFontChoose_Click);
// 
// mainTextColorChooseButton
// 
			this.mainTextColorChooseButton.Location = new System.Drawing.Point(7, 44);
			this.mainTextColorChooseButton.Name = "mainTextColorChooseButton";
			this.mainTextColorChooseButton.Size = new System.Drawing.Size(136, 33);
			this.mainTextColorChooseButton.TabIndex = 9;
			this.mainTextColorChooseButton.Text = "Выбрать цвет текста";
			this.mainTextColorChooseButton.Click += new System.EventHandler(this.mainTextColor_Click);
// 
// opacityNumeric
// 
			this.opacityNumeric.DecimalPlaces = 1;
			this.opacityNumeric.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.opacityNumeric.Location = new System.Drawing.Point(7, 41);
			this.opacityNumeric.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.opacityNumeric.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            65536});
			this.opacityNumeric.Name = "opacityNumeric";
			this.opacityNumeric.Size = new System.Drawing.Size(83, 20);
			this.opacityNumeric.TabIndex = 20;
			this.opacityNumeric.Value = new decimal(new int[] {
            4,
            0,
            0,
            65536});
			this.opacityNumeric.ValueChanged += new System.EventHandler(this.opacityNumeric_ValueChanged);
// 
// label3
// 
			this.label3.Location = new System.Drawing.Point(7, 20);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 17);
			this.label3.TabIndex = 19;
			this.label3.Text = "Прозрачность";
// 
// colorChooseButton
// 
			this.colorChooseButton.Location = new System.Drawing.Point(5, 68);
			this.colorChooseButton.Name = "colorChooseButton";
			this.colorChooseButton.Size = new System.Drawing.Size(97, 23);
			this.colorChooseButton.TabIndex = 18;
			this.colorChooseButton.Text = "Выбрать цвет";
			this.colorChooseButton.Click += new System.EventHandler(this.colorChooseButton_Click);
// 
// setDefaultsButton
// 
			this.setDefaultsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.setDefaultsButton.AutoRelocate = true;
			this.setDefaultsButton.Location = new System.Drawing.Point(350, 187);
			this.setDefaultsButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.setDefaultsButton.Name = "setDefaultsButton";
			this.setDefaultsButton.Size = new System.Drawing.Size(117, 24);
			this.setDefaultsButton.TabIndex = 20;
			this.setDefaultsButton.Text = "По умолчанию";
			this.setDefaultsButton.Click += new System.EventHandler(this.setDefaultsButton_Click);
// 
// cancelButton
// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cancelButton.AutoRelocate = true;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(178, 186);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(72, 24);
			this.cancelButton.TabIndex = 19;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.Click += new System.EventHandler(this.Exit);
// 
// okButton
// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.okButton.AutoRelocate = true;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(77, 186);
			this.okButton.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(72, 24);
			this.okButton.TabIndex = 18;
			this.okButton.Text = "Ок";
			this.okButton.Click += new System.EventHandler(this.Exit);
// 
// openDialog
// 
			this.openDialog.Filter = "WinBalans Plug-ins (*.dll)|*.dll|All files (*.*)|*.*";
// 
// groupBox1
// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.groupBox1.Controls.Add(this.opacityNumeric);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.colorChooseButton);
			this.groupBox1.Location = new System.Drawing.Point(282, 84);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(0, 3, 3, 3);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(213, 99);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Параметры главного окна";
// 
// SetUIParameters
// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(507, 212);
			this.ControlBox = false;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.setDefaultsButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox3);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "SetUIParameters";
			this.Text = "Параметры";
			this.VisibleChanged += new System.EventHandler(this.SetUIParameters_VisibleChanged);
			this.groupBox2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.deskXnumeric)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.deskYnumeric)).EndInit();
			this.groupBox3.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.opacityNumeric)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button desktopChooseFontButton;
		private System.Windows.Forms.Button color2ChooseButton;
		private System.Windows.Forms.Button color1ChooseButton;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown deskXnumeric;
		private System.Windows.Forms.NumericUpDown deskYnumeric;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button mainTextFontChooseButton;
		private System.Windows.Forms.Button mainTextColorChooseButton;
		private System.Windows.Forms.NumericUpDown opacityNumeric;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button colorChooseButton;
		private System.Windows.Forms.Button setDefaultsButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.OpenFileDialog openDialog;
		private System.Windows.Forms.SaveFileDialog saveDialog;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.FontDialog fontDialog;
		private System.Windows.Forms.ListBox desktopLabelEffectListBox;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.GroupBox groupBox1;
	}
}