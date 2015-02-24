using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinBalans
{
	/// <summary>
	/// Summary description for ChoosePlugin.
	/// </summary>
	public class ChoosePlugin : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox pluginListBox;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ChoosePlugin()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/*public Plugin SelectedPlugin
		{
			get
			{
				return null;
			}
			set
			{

			}
		}*/
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
			this.pluginListBox = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// pluginListBox
			// 
			this.pluginListBox.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.pluginListBox.Location = new System.Drawing.Point(8, 8);
			this.pluginListBox.Name = "pluginListBox";
			this.pluginListBox.Size = new System.Drawing.Size(136, 225);
			this.pluginListBox.Sorted = true;
			this.pluginListBox.TabIndex = 0;
			// 
			// ChoosePlugin
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(152, 254);
			this.ControlBox = false;
			this.Controls.Add(this.pluginListBox);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "ChoosePlugin";
			this.Text = "ChoosePlugin";
			this.ResumeLayout(false);

		}
		#endregion
	}
}
