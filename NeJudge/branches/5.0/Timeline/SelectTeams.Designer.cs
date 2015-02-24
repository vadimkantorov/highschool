﻿namespace Timeline
{
	partial class SelectTeams
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
			this.lstTeams = new System.Windows.Forms.CheckedListBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.lblHello = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lstTeams
			// 
			this.lstTeams.CheckOnClick = true;
			this.lstTeams.FormattingEnabled = true;
			this.lstTeams.Location = new System.Drawing.Point(12, 29);
			this.lstTeams.Name = "lstTeams";
			this.lstTeams.Size = new System.Drawing.Size(273, 276);
			this.lstTeams.TabIndex = 0;
			this.lstTeams.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstTeams_ItemCheck);
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(12, 320);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(137, 32);
			this.btnOk.TabIndex = 1;
			this.btnOk.Text = "ОК";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(155, 320);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(130, 32);
			this.btnCancel.TabIndex = 2;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// lblHello
			// 
			this.lblHello.AutoSize = true;
			this.lblHello.Location = new System.Drawing.Point(12, 7);
			this.lblHello.Name = "lblHello";
			this.lblHello.Size = new System.Drawing.Size(153, 17);
			this.lblHello.TabIndex = 3;
			this.lblHello.Text = "Выберите участников";
			// 
			// SelectTeams
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(301, 364);
			this.Controls.Add(this.lblHello);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lstTeams);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "SelectTeams";
			this.Text = "Выбор команд";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox lstTeams;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Label lblHello;
	}
}