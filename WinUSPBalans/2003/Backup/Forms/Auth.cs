using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans
{
	/// <summary>
	/// Summary description for Auth.
	/// </summary>
	public class Auth : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Button OkButton;
		private System.Windows.Forms.Button button1;

		private System.Windows.Forms.TextBox passwordTextbox;
		private System.Windows.Forms.TextBox usernameTextbox;
		private bool isOk;
		//Свойства
		public bool IsOk
		{
			get
			{
				return isOk;
			}
		}
		public string username
		{
			get
			{
				return usernameTextbox.Text;
			}
		}
		public string password
		{
			get
			{
				return passwordTextbox.Text;
			}
		}
		//

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public Auth()
		{
			//
			// Required for Windows Form Designer support
			//
			this.isOk = false;
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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
			this.OkButton = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// passwordTextbox
			// 
			this.passwordTextbox.Location = new System.Drawing.Point(128, 48);
			this.passwordTextbox.Name = "passwordTextbox";
			this.passwordTextbox.PasswordChar = '*';
			this.passwordTextbox.Size = new System.Drawing.Size(80, 20);
			this.passwordTextbox.TabIndex = 7;
			this.passwordTextbox.Text = "";
			// 
			// usernameTextbox
			// 
			this.usernameTextbox.Location = new System.Drawing.Point(16, 48);
			this.usernameTextbox.Name = "usernameTextbox";
			this.usernameTextbox.Size = new System.Drawing.Size(80, 20);
			this.usernameTextbox.TabIndex = 6;
			this.usernameTextbox.Text = "";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(144, 32);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(48, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Пароль";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 32);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(112, 16);
			this.label1.TabIndex = 4;
			this.label1.Text = "Имя пользователя";
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label3.Location = new System.Drawing.Point(8, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(240, 32);
			this.label3.TabIndex = 8;
			this.label3.Text = "Введите имя\\пароль для получения статистики";
			// 
			// OkButton
			// 
			this.OkButton.Location = new System.Drawing.Point(72, 80);
			this.OkButton.Name = "OkButton";
			this.OkButton.Size = new System.Drawing.Size(40, 23);
			this.OkButton.TabIndex = 9;
			this.OkButton.Text = "Ok";
			this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(120, 80);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(56, 24);
			this.button1.TabIndex = 10;
			this.button1.Text = "Отмена";
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Auth
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(242, 112);
			this.ControlBox = false;
			this.Controls.Add(this.button1);
			this.Controls.Add(this.OkButton);
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

		}
		#endregion

		private void OkButton_Click(object sender, System.EventArgs e)
		{
			if(usernameTextbox.Text!=""&&passwordTextbox.Text!="")
			{
				this.isOk = true;
				this.Hide();
			}
			else
				MessageBox.Show("Введите  имя пользователя и пароль","Ошибка аутентификации",MessageBoxButtons.OK,MessageBoxIcon.Error);
		
		}

		private void button1_Click(object sender, System.EventArgs e)
		{
			this.Hide();
		}
	}
}
