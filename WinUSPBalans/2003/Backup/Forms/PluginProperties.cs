using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans.Forms
{
	/// <summary>
	/// Summary description for PluginProperties.
	/// </summary>
	public class PluginProperties : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox password;
		private System.Windows.Forms.TextBox username;
		private System.Windows.Forms.TextBox decription;
		private System.Windows.Forms.TextBox userdescription;
		private Plugin p2;
		private Plugin p1;
		private System.Windows.Forms.Label pluginname;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.Button okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public PluginProperties(Plugin p)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			p1 = p2 = p;
			username.Text = p.Username;
			password.Text = p.Password;
			description.Text = p.Description;
			userdescription.Text = p.UserDescription;
			pluginname.Text = p.Name;
		}
		
		public Plugin GetPlugin()
		{
			if(this.DialogResult = DialogResult.OK)
				return p2;
			return p1;
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.password = new System.Windows.Forms.TextBox();
			this.username = new System.Windows.Forms.TextBox();
			this.decription = new System.Windows.Forms.TextBox();
			this.userdescription = new System.Windows.Forms.TextBox();
			this.pluginname = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Имя пользователя";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(16, 64);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Пароль";
			// 
			// password
			// 
			this.password.Location = new System.Drawing.Point(16, 88);
			this.password.Name = "password";
			this.password.PasswordChar = '*';
			this.password.Size = new System.Drawing.Size(176, 20);
			this.password.TabIndex = 18;
			this.password.Text = "";
			this.password.TextChanged += new System.EventHandler(this.password_TextChanged);
			// 
			// username
			// 
			this.username.Location = new System.Drawing.Point(16, 40);
			this.username.Name = "username";
			this.username.Size = new System.Drawing.Size(176, 20);
			this.username.TabIndex = 17;
			this.username.Text = "";
			this.username.TextChanged += new System.EventHandler(this.username_TextChanged);
			// 
			// decription
			// 
			this.decription.AutoSize = false;
			this.decription.Location = new System.Drawing.Point(16, 120);
			this.decription.Name = "decription";
			this.decription.Size = new System.Drawing.Size(184, 64);
			this.decription.TabIndex = 19;
			this.decription.Text = "textBox1";
			this.decription.TextChanged += new System.EventHandler(this.decription_TextChanged);
			// 
			// userdescription
			// 
			this.userdescription.AutoSize = false;
			this.userdescription.Location = new System.Drawing.Point(16, 192);
			this.userdescription.Name = "userdescription";
			this.userdescription.ReadOnly = true;
			this.userdescription.Size = new System.Drawing.Size(184, 64);
			this.userdescription.TabIndex = 20;
			this.userdescription.Text = "textBox2";
			// 
			// pluginname
			// 
			this.pluginname.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.pluginname.Location = new System.Drawing.Point(128, 0);
			this.pluginname.Name = "pluginname";
			this.pluginname.Size = new System.Drawing.Size(112, 24);
			this.pluginname.TabIndex = 21;
			// 
			// cancelButton
			// 
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(128, 272);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(104, 32);
			this.cancelButton.TabIndex = 22;
			this.cancelButton.Text = "Отмена";
			// 
			// okButton
			// 
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(8, 272);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(104, 32);
			this.okButton.TabIndex = 23;
			this.okButton.Text = "Ок";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
			// 
			// PluginProperties
			// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.cancelButton;
			this.ClientSize = new System.Drawing.Size(240, 310);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.pluginname);
			this.Controls.Add(this.userdescription);
			this.Controls.Add(this.decription);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.password);
			this.Controls.Add(this.username);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PluginProperties";
			this.Text = "PluginProperties";
			this.ResumeLayout(false);

		}
		#endregion

		private void okButton_Click(object sender, System.EventArgs e)
		{
			Hide();
		}

		private void username_TextChanged(object sender, System.EventArgs e)
		{
			p2.Username = this.Text;
		}

		private void password_TextChanged(object sender, System.EventArgs e)
		{
			p2.Password = this.Text;
		}

		private void decription_TextChanged(object sender, System.EventArgs e)
		{
			p2.UserDescription = this.Text;
		}

	}
}
