namespace ThermoTools
{
	partial class ThermoForm
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
			this.components = new System.ComponentModel.Container();
			this.btnSettings = new System.Windows.Forms.Button();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.btnVis = new System.Windows.Forms.Button();
			this.btnStartStop = new System.Windows.Forms.Button();
			this.led4 = new LEDdisplay.LED();
			this.led3 = new LEDdisplay.LED();
			this.led2 = new LEDdisplay.LED();
			this.led1 = new LEDdisplay.LED();
			this.btnMore = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lblAbsError = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblRelError = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnSettings
			// 
			this.btnSettings.Location = new System.Drawing.Point(33, 74);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(109, 21);
			this.btnSettings.TabIndex = 3;
			this.btnSettings.Text = "Настройки";
			this.btnSettings.UseVisualStyleBackColor = true;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// btnVis
			// 
			this.btnVis.Location = new System.Drawing.Point(33, 101);
			this.btnVis.Name = "btnVis";
			this.btnVis.Size = new System.Drawing.Size(109, 21);
			this.btnVis.TabIndex = 4;
			this.btnVis.Text = "Зависимость";
			this.btnVis.UseVisualStyleBackColor = true;
			this.btnVis.Click += new System.EventHandler(this.btnVis_Click);
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(19, 128);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(141, 22);
			this.btnStartStop.TabIndex = 5;
			this.btnStartStop.Text = "Начать измерения";
			this.btnStartStop.UseVisualStyleBackColor = true;
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// led4
			// 
			this.led4.Location = new System.Drawing.Point(120, 12);
			this.led4.Name = "led4";
			this.led4.Size = new System.Drawing.Size(40, 56);
			this.led4.TabIndex = 6;
			// 
			// led3
			// 
			this.led3.Location = new System.Drawing.Point(86, 12);
			this.led3.Name = "led3";
			this.led3.Size = new System.Drawing.Size(37, 56);
			this.led3.TabIndex = 2;
			// 
			// led2
			// 
			this.led2.Location = new System.Drawing.Point(49, 12);
			this.led2.Name = "led2";
			this.led2.Size = new System.Drawing.Size(40, 56);
			this.led2.TabIndex = 1;
			// 
			// led1
			// 
			this.led1.Location = new System.Drawing.Point(12, 12);
			this.led1.Name = "led1";
			this.led1.Size = new System.Drawing.Size(40, 56);
			this.led1.TabIndex = 0;
			// 
			// btnMore
			// 
			this.btnMore.Location = new System.Drawing.Point(149, 74);
			this.btnMore.Name = "btnMore";
			this.btnMore.Size = new System.Drawing.Size(21, 37);
			this.btnMore.TabIndex = 7;
			this.btnMore.Text = "<<";
			this.btnMore.UseVisualStyleBackColor = true;
			this.btnMore.Click += new System.EventHandler(this.btnErrors_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.lblRelError);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lblAbsError);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Location = new System.Drawing.Point(176, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(197, 131);
			this.groupBox1.TabIndex = 8;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Погрешности";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 25);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(165, 31);
			this.label1.TabIndex = 0;
			this.label1.Text = "Абсолютная (Макс. значение напряжения/(2^разрядность)):";
			// 
			// lblAbsError
			// 
			this.lblAbsError.AutoSize = true;
			this.lblAbsError.Location = new System.Drawing.Point(10, 56);
			this.lblAbsError.Name = "lblAbsError";
			this.lblAbsError.Size = new System.Drawing.Size(55, 13);
			this.lblAbsError.TabIndex = 1;
			this.lblAbsError.Text = "Значение";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(10, 74);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(185, 30);
			this.label2.TabIndex = 2;
			this.label2.Text = "Относительная (Абсолютная/Показания прибора):";
			// 
			// lblRelError
			// 
			this.lblRelError.AutoSize = true;
			this.lblRelError.Location = new System.Drawing.Point(10, 104);
			this.lblRelError.Name = "lblRelError";
			this.lblRelError.Size = new System.Drawing.Size(55, 13);
			this.lblRelError.TabIndex = 3;
			this.lblRelError.Text = "Значение";
			// 
			// ThermoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(383, 155);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnMore);
			this.Controls.Add(this.led4);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.btnVis);
			this.Controls.Add(this.btnSettings);
			this.Controls.Add(this.led3);
			this.Controls.Add(this.led2);
			this.Controls.Add(this.led1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ThermoForm";
			this.Text = "ThermoForm";
			this.Load += new System.EventHandler(this.ThermoForm_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private LEDdisplay.LED led1;
		private LEDdisplay.LED led2;
		private LEDdisplay.LED led3;
		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Button btnVis;
		private System.Windows.Forms.Button btnStartStop;
		private LEDdisplay.LED led4;
		private System.Windows.Forms.Button btnMore;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lblAbsError;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblRelError;
	}
}