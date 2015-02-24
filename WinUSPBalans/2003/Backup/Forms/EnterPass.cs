using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans
{
	/// <summary>
	/// Summary description for EnterPass.
	/// </summary>
	public class EnterPass : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button button2;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public string Key
		{
			get
			{
				return this.textBox1.Text;
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.button2 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(8, 56);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(168, 20);
			this.textBox1.TabIndex = 0;
			this.textBox1.Text = "";
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(184, 40);
			this.label1.TabIndex = 1;
			this.label1.Text = "Введите пароль для безопасного хранения идентификационной информации Вашего прова" +
				"йдера";
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(64, 80);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(56, 24);
			this.button2.TabIndex = 3;
			this.button2.Text = "Ок";
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// EnterPass
			// 
			this.AcceptButton = this.button2;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(186, 112);
			this.ControlBox = false;
			this.Controls.Add(this.button2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.textBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "EnterPass";
			this.Text = "Введите пароль";
			this.ResumeLayout(false);

		}
		public void ChangeValues(string text)
		{
			this.label1.Text = text;
			this.textBox1.Focus();
		}
		#endregion

		private void button2_Click(object sender, System.EventArgs e)
		{
			if(textBox1.Text == "")
				MessageBox.Show("Ключ не может быть пустым. Введите нормальный ключ.", "Ошибка при вводе ключа",MessageBoxButtons.OK,MessageBoxIcon.Information);
			else
				this.Hide();
		}


	}
}
