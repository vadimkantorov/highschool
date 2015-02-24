namespace ThermoTools
{
	partial class Settings
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if( disposing && ( components != null ) )
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
			this.nudFreq = new System.Windows.Forms.NumericUpDown();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.ddlPorts = new System.Windows.Forms.ComboBox();
			this.gbCalibration = new System.Windows.Forms.GroupBox();
			this.tbTemp2 = new System.Windows.Forms.TextBox();
			this.tbTemp1 = new System.Windows.Forms.TextBox();
			this.tbVoltage2 = new System.Windows.Forms.TextBox();
			this.tbVoltage1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.nudCapacity = new System.Windows.Forms.NumericUpDown();
			this.nudMaxVoltage = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.rbVolt = new System.Windows.Forms.RadioButton();
			this.rbThermo = new System.Windows.Forms.RadioButton();
			( (System.ComponentModel.ISupportInitialize)( this.nudFreq ) ).BeginInit();
			this.gbCalibration.SuspendLayout();
			( (System.ComponentModel.ISupportInitialize)( this.nudCapacity ) ).BeginInit();
			( (System.ComponentModel.ISupportInitialize)( this.nudMaxVoltage ) ).BeginInit();
			this.SuspendLayout();
			// 
			// nudFreq
			// 
			this.nudFreq.Location = new System.Drawing.Point(92, 32);
			this.nudFreq.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
			this.nudFreq.Name = "nudFreq";
			this.nudFreq.Size = new System.Drawing.Size(76, 20);
			this.nudFreq.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(5, 35);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(52, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Частота:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(5, 9);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 2;
			this.label2.Text = "Порт:";
			// 
			// ddlPorts
			// 
			this.ddlPorts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.ddlPorts.FormattingEnabled = true;
			this.ddlPorts.Location = new System.Drawing.Point(91, 5);
			this.ddlPorts.Name = "ddlPorts";
			this.ddlPorts.Size = new System.Drawing.Size(77, 21);
			this.ddlPorts.TabIndex = 3;
			// 
			// gbCalibration
			// 
			this.gbCalibration.Controls.Add(this.tbTemp2);
			this.gbCalibration.Controls.Add(this.tbTemp1);
			this.gbCalibration.Controls.Add(this.tbVoltage2);
			this.gbCalibration.Controls.Add(this.tbVoltage1);
			this.gbCalibration.Controls.Add(this.label5);
			this.gbCalibration.Controls.Add(this.label6);
			this.gbCalibration.Controls.Add(this.label4);
			this.gbCalibration.Controls.Add(this.label3);
			this.gbCalibration.Location = new System.Drawing.Point(93, 159);
			this.gbCalibration.Name = "gbCalibration";
			this.gbCalibration.Size = new System.Drawing.Size(381, 68);
			this.gbCalibration.TabIndex = 4;
			this.gbCalibration.TabStop = false;
			this.gbCalibration.Text = "Калибровка";
			// 
			// tbTemp2
			// 
			this.tbTemp2.Location = new System.Drawing.Point(292, 45);
			this.tbTemp2.Name = "tbTemp2";
			this.tbTemp2.Size = new System.Drawing.Size(81, 20);
			this.tbTemp2.TabIndex = 9;
			// 
			// tbTemp1
			// 
			this.tbTemp1.Location = new System.Drawing.Point(293, 16);
			this.tbTemp1.Name = "tbTemp1";
			this.tbTemp1.Size = new System.Drawing.Size(81, 20);
			this.tbTemp1.TabIndex = 8;
			// 
			// tbVoltage2
			// 
			this.tbVoltage2.Location = new System.Drawing.Point(95, 45);
			this.tbVoltage2.Name = "tbVoltage2";
			this.tbVoltage2.Size = new System.Drawing.Size(81, 20);
			this.tbVoltage2.TabIndex = 7;
			// 
			// tbVoltage1
			// 
			this.tbVoltage1.Location = new System.Drawing.Point(96, 16);
			this.tbVoltage1.Name = "tbVoltage1";
			this.tbVoltage1.Size = new System.Drawing.Size(81, 20);
			this.tbVoltage1.TabIndex = 6;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(201, 48);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(86, 13);
			this.label5.TabIndex = 5;
			this.label5.Text = "Температура 2:";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(6, 48);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(83, 13);
			this.label6.TabIndex = 4;
			this.label6.Text = "Напряжение 2:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(201, 16);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(86, 13);
			this.label4.TabIndex = 3;
			this.label4.Text = "Температура 1:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(83, 13);
			this.label3.TabIndex = 2;
			this.label3.Text = "Напряжение 1:";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(255, 239);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(73, 27);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(176, 239);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(73, 27);
			this.btnOk.TabIndex = 5;
			this.btnOk.Text = "OK";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(5, 64);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(76, 13);
			this.label7.TabIndex = 7;
			this.label7.Text = "Разрядность:";
			// 
			// nudCapacity
			// 
			this.nudCapacity.Location = new System.Drawing.Point(116, 62);
			this.nudCapacity.Maximum = new decimal(new int[] {
            32,
            0,
            0,
            0});
			this.nudCapacity.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
			this.nudCapacity.Name = "nudCapacity";
			this.nudCapacity.Size = new System.Drawing.Size(52, 20);
			this.nudCapacity.TabIndex = 8;
			this.nudCapacity.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
			// 
			// nudMaxVoltage
			// 
			this.nudMaxVoltage.DecimalPlaces = 2;
			this.nudMaxVoltage.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
			this.nudMaxVoltage.Location = new System.Drawing.Point(116, 87);
			this.nudMaxVoltage.Name = "nudMaxVoltage";
			this.nudMaxVoltage.Size = new System.Drawing.Size(52, 20);
			this.nudMaxVoltage.TabIndex = 10;
			this.nudMaxVoltage.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(5, 89);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(105, 13);
			this.label8.TabIndex = 9;
			this.label8.Text = "Макс. напряжение:";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(5, 114);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(45, 13);
			this.label9.TabIndex = 11;
			this.label9.Text = "Режим:";
			// 
			// rbVolt
			// 
			this.rbVolt.AutoSize = true;
			this.rbVolt.Location = new System.Drawing.Point(93, 114);
			this.rbVolt.Name = "rbVolt";
			this.rbVolt.Size = new System.Drawing.Size(80, 17);
			this.rbVolt.TabIndex = 12;
			this.rbVolt.TabStop = true;
			this.rbVolt.Text = "Вольтметр";
			this.rbVolt.UseVisualStyleBackColor = true;
			// 
			// rbThermo
			// 
			this.rbThermo.AutoSize = true;
			this.rbThermo.Location = new System.Drawing.Point(93, 136);
			this.rbThermo.Name = "rbThermo";
			this.rbThermo.Size = new System.Drawing.Size(83, 17);
			this.rbThermo.TabIndex = 13;
			this.rbThermo.TabStop = true;
			this.rbThermo.Text = "Термометр";
			this.rbThermo.UseVisualStyleBackColor = true;
			this.rbThermo.CheckedChanged += new System.EventHandler(this.rdThermo_CheckedChanged);
			// 
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(486, 273);
			this.ControlBox = false;
			this.Controls.Add(this.rbThermo);
			this.Controls.Add(this.rbVolt);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.nudMaxVoltage);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.nudCapacity);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.gbCalibration);
			this.Controls.Add(this.ddlPorts);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudFreq);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Settings";
			this.Text = "Настройки";
			this.Load += new System.EventHandler(this.Settings_Load);
			( (System.ComponentModel.ISupportInitialize)( this.nudFreq ) ).EndInit();
			this.gbCalibration.ResumeLayout(false);
			this.gbCalibration.PerformLayout();
			( (System.ComponentModel.ISupportInitialize)( this.nudCapacity ) ).EndInit();
			( (System.ComponentModel.ISupportInitialize)( this.nudMaxVoltage ) ).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown nudFreq;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox ddlPorts;
		private System.Windows.Forms.GroupBox gbCalibration;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tbTemp2;
		private System.Windows.Forms.TextBox tbTemp1;
		private System.Windows.Forms.TextBox tbVoltage2;
		private System.Windows.Forms.TextBox tbVoltage1;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudCapacity;
		private System.Windows.Forms.NumericUpDown nudMaxVoltage;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.RadioButton rbVolt;
		private System.Windows.Forms.RadioButton rbThermo;
	}
}