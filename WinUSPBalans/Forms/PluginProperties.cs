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
        private System.Windows.Forms.TextBox usernameTextBox;
		private System.Windows.Forms.TextBox descriptionTextBox;
		private System.Windows.Forms.TextBox userDescriptionTextBox;
		Plugin p2;
		Plugin p1;
		private System.Windows.Forms.Label pluginNameLabel;
		private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.Button okButton;
		private TextBox passwordTextBox;
		private Label label3;
		private Label label4;
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

			p1 = (Plugin)p.Clone();
			p2 = (Plugin)p.Clone();
			descriptionTextBox.Text = p2.Description;
			usernameTextBox.Text = p2.Username;
			passwordTextBox.Text = p2.Password;
			userDescriptionTextBox.Text = p.UserDescription;
			p2.UserDescription = p1.UserDescription = p.UserDescription;
			pluginNameLabel.Text = p2.Name;
		}
		
		public Plugin GetPlugin()
		{
			return p2;
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
			this.usernameTextBox = new System.Windows.Forms.TextBox();
			this.descriptionTextBox = new System.Windows.Forms.TextBox();
			this.userDescriptionTextBox = new System.Windows.Forms.TextBox();
			this.pluginNameLabel = new System.Windows.Forms.Label();
			this.cancelButton = new System.Windows.Forms.Button();
			this.okButton = new System.Windows.Forms.Button();
			this.passwordTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.SuspendLayout();
// 
// label1
// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(13, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(104, 16);
			this.label1.TabIndex = 15;
			this.label1.Text = "Имя пользователя";
// 
// label2
// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label2.Location = new System.Drawing.Point(13, 67);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(80, 16);
			this.label2.TabIndex = 16;
			this.label2.Text = "Пароль";
// 
// usernameTextBox
// 
			this.usernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.usernameTextBox.Location = new System.Drawing.Point(16, 40);
			this.usernameTextBox.Name = "usernameTextBox";
			this.usernameTextBox.Size = new System.Drawing.Size(206, 20);
			this.usernameTextBox.TabIndex = 17;
			this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
// 
// descriptionTextBox
// 
			this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.descriptionTextBox.AutoSize = false;
			this.descriptionTextBox.Location = new System.Drawing.Point(16, 136);
			this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
			this.descriptionTextBox.Multiline = true;
			this.descriptionTextBox.Name = "descriptionTextBox";
			this.descriptionTextBox.ReadOnly = true;
			this.descriptionTextBox.Size = new System.Drawing.Size(206, 64);
			this.descriptionTextBox.TabIndex = 19;
// 
// userDescriptionTextBox
// 
			this.userDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.userDescriptionTextBox.AutoSize = false;
			this.userDescriptionTextBox.Location = new System.Drawing.Point(16, 230);
			this.userDescriptionTextBox.Multiline = true;
			this.userDescriptionTextBox.Name = "userDescriptionTextBox";
			this.userDescriptionTextBox.Size = new System.Drawing.Size(206, 64);
			this.userDescriptionTextBox.TabIndex = 20;
			this.userDescriptionTextBox.TextChanged += new System.EventHandler(this.userDescriptionTextBox_TextChanged);
// 
// pluginNameLabel
// 
			this.pluginNameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.pluginNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.pluginNameLabel.Location = new System.Drawing.Point(3, -3);
			this.pluginNameLabel.Name = "pluginNameLabel";
			this.pluginNameLabel.Size = new System.Drawing.Size(103, 24);
			this.pluginNameLabel.TabIndex = 21;
// 
// cancelButton
// 
			this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(118, 301);
			this.cancelButton.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Size = new System.Drawing.Size(104, 32);
			this.cancelButton.TabIndex = 22;
			this.cancelButton.Text = "Отмена";
			this.cancelButton.Click += new System.EventHandler(this.exitButton_Click);
// 
// okButton
// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(13, 301);
			this.okButton.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(104, 32);
			this.okButton.TabIndex = 23;
			this.okButton.Text = "Ок";
			this.okButton.Click += new System.EventHandler(this.exitButton_Click);
// 
// passwordTextBox
// 
			this.passwordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.passwordTextBox.Location = new System.Drawing.Point(13, 90);
			this.passwordTextBox.Name = "passwordTextBox";
			this.passwordTextBox.PasswordChar = '●';
			this.passwordTextBox.Size = new System.Drawing.Size(206, 20);
			this.passwordTextBox.TabIndex = 18;
			this.passwordTextBox.UseSystemPasswordChar = true;
			this.passwordTextBox.TextChanged += new System.EventHandler(this.passwordTextBox_TextChanged);
// 
// label3
// 
			this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label3.Location = new System.Drawing.Point(13, 117);
			this.label3.Margin = new System.Windows.Forms.Padding(3, 3, 3, 1);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(64, 16);
			this.label3.TabIndex = 24;
			this.label3.Text = "Описание";
// 
// label4
// 
			this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label4.Location = new System.Drawing.Point(16, 207);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(90, 16);
			this.label4.TabIndex = 25;
			this.label4.Text = "Ваше описание";
// 
// PluginProperties
// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(234, 345);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.cancelButton);
			this.Controls.Add(this.pluginNameLabel);
			this.Controls.Add(this.userDescriptionTextBox);
			this.Controls.Add(this.descriptionTextBox);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.passwordTextBox);
			this.Controls.Add(this.usernameTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PluginProperties";
			this.Text = "Свойства плагина";
			this.VisibleChanged += new System.EventHandler(this.PluginProperties_VisibleChanged);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion

		private void exitButton_Click(object sender, System.EventArgs e)
		{
			Hide();
		}
		private void ChangeValues(Plugin p)
		{
			p2 = (Plugin)p.Clone();
			descriptionTextBox.Text = p2.Description;
			pluginNameLabel.Text = p2.Name;

			usernameTextBox.Text = p2.Username;
			passwordTextBox.Text = p2.Password;
			userDescriptionTextBox.Text = p2.UserDescription;
		}

		private void PluginProperties_VisibleChanged(object sender, EventArgs e)
		{
			if (Visible == false)
			
				if (DialogResult == DialogResult.OK)
					p1 = (Plugin)p2.Clone();
				else
					p2 = (Plugin)p1.Clone();
			else
				ChangeValues(p2);
		}

		private void usernameTextBox_TextChanged(object sender, EventArgs e)
		{
			p2.Username = usernameTextBox.Text;
		}

		private void passwordTextBox_TextChanged(object sender, EventArgs e)
		{
			p2.Password = passwordTextBox.Text;
		}

		private void userDescriptionTextBox_TextChanged(object sender, EventArgs e)
		{
			p2.UserDescription = userDescriptionTextBox.Text;
		}
	}
}
