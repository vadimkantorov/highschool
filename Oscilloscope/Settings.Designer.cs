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
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.nudCapacity = new System.Windows.Forms.NumericUpDown();
			this.nudMaxVoltage = new System.Windows.Forms.NumericUpDown();
			this.label8 = new System.Windows.Forms.Label();
			( (System.ComponentModel.ISupportInitialize)( this.nudFreq ) ).BeginInit();
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
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(92, 116);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(73, 27);
			this.btnCancel.TabIndex = 6;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(13, 116);
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
			// Settings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(176, 149);
			this.ControlBox = false;
			this.Controls.Add(this.nudMaxVoltage);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.nudCapacity);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.ddlPorts);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudFreq);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "Settings";
			this.Text = "Настройки";
			this.Load += new System.EventHandler(this.Settings_Load);
			( (System.ComponentModel.ISupportInitialize)( this.nudFreq ) ).EndInit();
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
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.NumericUpDown nudCapacity;
		private System.Windows.Forms.NumericUpDown nudMaxVoltage;
		private System.Windows.Forms.Label label8;
	}
}