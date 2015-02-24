using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans.Forms
{
	/// <summary>
	/// Summary description for EnterPass.
	/// </summary>
	public class EnterPass : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox keyTextBox;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button okButton;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string Key
		{
			get
			{
				return this.keyTextBox.Text;
			}
		}

		public EnterPass()
		{
			//
			// Required for Windows Form Designer support
			//
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
			this.keyTextBox = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.okButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
// 
// keyTextBox
// 
			this.keyTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.keyTextBox.Location = new System.Drawing.Point(8, 56);
			this.keyTextBox.Name = "keyTextBox";
			this.keyTextBox.Size = new System.Drawing.Size(166, 20);
			this.keyTextBox.TabIndex = 0;
// 
// label1
// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(178, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "Введите пароль для шифровки/дешифровки  идентификационной информации Вашего прова" +
				"йдера";
// 
// okButton
// 
			this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
						| System.Windows.Forms.AnchorStyles.Left)
						| System.Windows.Forms.AnchorStyles.Right)));
			this.okButton.Location = new System.Drawing.Point(64, 80);
			this.okButton.Name = "okButton";
			this.okButton.Size = new System.Drawing.Size(56, 24);
			this.okButton.TabIndex = 3;
			this.okButton.Text = "Ок";
			this.okButton.Click += new System.EventHandler(this.okButton_Click);
// 
// EnterPass
// 
			this.AcceptButton = this.okButton;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(186, 112);
			this.ControlBox = false;
			this.Controls.Add(this.okButton);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.keyTextBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EnterPass";
			this.Text = "Введите пароль";
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		public void ChangeValues(string text)
		{
			this.label1.Text = text;
			this.keyTextBox.Focus();
		}
		#endregion

		private void okButton_Click(object sender, System.EventArgs e)
		{
			if(keyTextBox.Text == "")
				MessageBox.Show("Ключ не может быть пустым. Введите нормальный ключ.", "Ошибка при вводе ключа",MessageBoxButtons.OK,MessageBoxIcon.Information);
			else
				this.Hide();
		}


	}
}
