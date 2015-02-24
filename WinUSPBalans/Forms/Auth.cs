using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans.Forms
{
	/// <summary>
	/// Summary description for Auth.
	/// </summary>
	public class Auth : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelbutton;

		private System.Windows.Forms.TextBox passwordTextbox;
		private System.Windows.Forms.TextBox usernameTextbox;
		private bool isOk;
		//Свойства
		public string Username
		{
			get
			{
				return usernameTextbox.Text;
			}
			set
			{
				usernameTextbox.Text = value;
			}
		}
		public string Password
		{
			get
			{
				return passwordTextbox.Text;
			}
			set
			{
				passwordTextbox.Text = value;
			}
		}
		//

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Auth(string username, string password)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			usernameTextbox.Text = username;
			passwordTextbox.Text = password;
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
			this.passwordTextbox = new System.Windows.Forms.TextBox();
			this.usernameTextbox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.cancelbutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
// 
// passwordTextbox
// 
			this.passwordTextbox.Location = new System.Drawing.Point(120, 48);
			this.passwordTextbox.Name = "passwordTextbox";
			this.passwordTextbox.PasswordChar = '*';
			this.passwordTextbox.Size = new System.Drawing.Size(110, 20);
			this.passwordTextbox.TabIndex = 7;
// 
// usernameTextbox
// 
			this.usernameTextbox.Location = new System.Drawing.Point(16, 48);
			this.usernameTextbox.Name = "usernameTextbox";
			this.usernameTextbox.Size = new System.Drawing.Size(96, 20);
			this.usernameTextbox.TabIndex = 6;
// 
// label2
// 
			this.label2.Location = new System.Drawing.Point(120, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Пароль";
// 
// label1
// 
			this.label1.Location = new System.Drawing.Point(16, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Имя пользователя";
// 
// label3
// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(8, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 32);
			this.label3.TabIndex = 8;
			this.label3.Text = "Введите имя\\пароль для получения статистики";
// 
// okButton
// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(72, 80);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(40, 23);
			this.okButton.TabIndex = 9;
			this.okButton.Text = "ОК";
			this.okButton.Click += new System.EventHandler(this.Exit);
// 
// cancelbutton
// 
			this.cancelbutton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelbutton.Location = new System.Drawing.Point(120, 80);
			this.cancelbutton.Name = "cancelbutton";
			this.cancelbutton.Size = new System.Drawing.Size(56, 24);
			this.cancelbutton.TabIndex = 10;
			this.cancelbutton.Text = "Отмена";
			this.cancelbutton.Click += new System.EventHandler(this.Exit);
// 
// Auth
// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelbutton;
			this.ClientSize = new System.Drawing.Size(242, 112);
			this.ControlBox = false;
			this.Controls.Add(this.cancelbutton);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.passwordTextbox);
			this.Controls.Add(this.usernameTextbox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MinimumSize = new System.Drawing.Size(246, 118);
			this.Name = "Auth";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Аутентификация";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void Exit(object sender, EventArgs e)
		{
			if (usernameTextbox.Text != "")
				this.Hide();
			else
				MessageBox.Show("Введите имя пользователя.", "Ошибка аутентификации", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}
