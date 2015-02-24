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
			this.btnStartStop = new System.Windows.Forms.Button();
			this.lblStateText = new System.Windows.Forms.Label();
			this.lblState = new System.Windows.Forms.Label();
			this.lblSaveToFile = new System.Windows.Forms.Button();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			this.SuspendLayout();
			// 
			// btnSettings
			// 
			this.btnSettings.Location = new System.Drawing.Point(18, 116);
			this.btnSettings.Name = "btnSettings";
			this.btnSettings.Size = new System.Drawing.Size(140, 30);
			this.btnSettings.TabIndex = 3;
			this.btnSettings.Text = "Настройки";
			this.btnSettings.UseVisualStyleBackColor = true;
			this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
			// 
			// timer
			// 
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// btnStartStop
			// 
			this.btnStartStop.Location = new System.Drawing.Point(17, 67);
			this.btnStartStop.Name = "btnStartStop";
			this.btnStartStop.Size = new System.Drawing.Size(141, 43);
			this.btnStartStop.TabIndex = 5;
			this.btnStartStop.Text = "Начать измерения";
			this.btnStartStop.UseVisualStyleBackColor = true;
			this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
			// 
			// lblStateText
			// 
			this.lblStateText.AutoSize = true;
			this.lblStateText.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ));
			this.lblStateText.Location = new System.Drawing.Point(6, 9);
			this.lblStateText.Name = "lblStateText";
			this.lblStateText.Size = new System.Drawing.Size(158, 31);
			this.lblStateText.TabIndex = 9;
			this.lblStateText.Text = "Состояние:";
			// 
			// lblState
			// 
			this.lblState.AutoSize = true;
			this.lblState.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( (byte)( 204 ) ));
			this.lblState.Location = new System.Drawing.Point(6, 40);
			this.lblState.Name = "lblState";
			this.lblState.Size = new System.Drawing.Size(165, 25);
			this.lblState.TabIndex = 10;
			this.lblState.Text = "Нет измерений";
			// 
			// lblSaveToFile
			// 
			this.lblSaveToFile.Location = new System.Drawing.Point(19, 152);
			this.lblSaveToFile.Name = "lblSaveToFile";
			this.lblSaveToFile.Size = new System.Drawing.Size(139, 45);
			this.lblSaveToFile.TabIndex = 11;
			this.lblSaveToFile.Text = "Сохранить измерения в файл";
			this.lblSaveToFile.UseVisualStyleBackColor = true;
			this.lblSaveToFile.Click += new System.EventHandler(this.lblSaveToFile_Click);
			// 
			// sfd
			// 
			this.sfd.FileName = "out.txt";
			this.sfd.Filter = "\"Text files|*.txt|All files|*.*\";";
			// 
			// ThermoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(170, 202);
			this.Controls.Add(this.lblSaveToFile);
			this.Controls.Add(this.lblState);
			this.Controls.Add(this.lblStateText);
			this.Controls.Add(this.btnStartStop);
			this.Controls.Add(this.btnSettings);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "ThermoForm";
			this.Text = "ThermoForm";
			this.Load += new System.EventHandler(this.ThermoForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnSettings;
		private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Button btnStartStop;
		private System.Windows.Forms.Label lblStateText;
		private System.Windows.Forms.Label lblState;
		private System.Windows.Forms.Button lblSaveToFile;
		private System.Windows.Forms.SaveFileDialog sfd;
	}
}