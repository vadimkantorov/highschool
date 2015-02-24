namespace ThermoTools
{
	partial class Visualizer
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
			System.Windows.Forms.Label label2;
			this.graph = new ZedGraph.ZedGraphControl();
			this.nudTension = new System.Windows.Forms.NumericUpDown();
			this.btnSave = new System.Windows.Forms.Button();
			this.sfd = new System.Windows.Forms.SaveFileDialog();
			label2 = new System.Windows.Forms.Label();
			( (System.ComponentModel.ISupportInitialize)( this.nudTension ) ).BeginInit();
			this.SuspendLayout();
			// 
			// label2
			// 
			label2.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			label2.AutoSize = true;
			label2.Location = new System.Drawing.Point(395, 396);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(117, 13);
			label2.TabIndex = 7;
			label2.Text = "Кривизна огибающей";
			// 
			// graph
			// 
			this.graph.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( ( ( System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom )
						| System.Windows.Forms.AnchorStyles.Left )
						| System.Windows.Forms.AnchorStyles.Right ) ) );
			this.graph.EditButtons = System.Windows.Forms.MouseButtons.Left;
			this.graph.EditModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
			this.graph.IsAutoScrollRange = false;
			this.graph.IsEnableHEdit = false;
			this.graph.IsEnableHPan = true;
			this.graph.IsEnableHZoom = true;
			this.graph.IsEnableVEdit = false;
			this.graph.IsEnableVPan = true;
			this.graph.IsEnableVZoom = true;
			this.graph.IsPrintFillPage = true;
			this.graph.IsPrintKeepAspectRatio = true;
			this.graph.IsScrollY2 = false;
			this.graph.IsShowContextMenu = true;
			this.graph.IsShowCopyMessage = true;
			this.graph.IsShowCursorValues = false;
			this.graph.IsShowHScrollBar = false;
			this.graph.IsShowPointValues = false;
			this.graph.IsShowVScrollBar = false;
			this.graph.IsSynchronizeXAxes = false;
			this.graph.IsSynchronizeYAxes = false;
			this.graph.IsZoomOnMouseCenter = false;
			this.graph.LinkButtons = System.Windows.Forms.MouseButtons.Left;
			this.graph.LinkModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.None ) ) );
			this.graph.Location = new System.Drawing.Point(0, 0);
			this.graph.Name = "graph";
			this.graph.PanButtons = System.Windows.Forms.MouseButtons.Left;
			this.graph.PanButtons2 = System.Windows.Forms.MouseButtons.Middle;
			this.graph.PanModifierKeys = ( (System.Windows.Forms.Keys)( ( System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.None ) ) );
			this.graph.PanModifierKeys2 = System.Windows.Forms.Keys.None;
			this.graph.PointDateFormat = "g";
			this.graph.PointValueFormat = "G";
			this.graph.ScrollMaxX = 0;
			this.graph.ScrollMaxY = 0;
			this.graph.ScrollMaxY2 = 0;
			this.graph.ScrollMinX = 0;
			this.graph.ScrollMinY = 0;
			this.graph.ScrollMinY2 = 0;
			this.graph.Size = new System.Drawing.Size(650, 386);
			this.graph.TabIndex = 0;
			this.graph.ZoomButtons = System.Windows.Forms.MouseButtons.Left;
			this.graph.ZoomButtons2 = System.Windows.Forms.MouseButtons.None;
			this.graph.ZoomModifierKeys = System.Windows.Forms.Keys.None;
			this.graph.ZoomModifierKeys2 = System.Windows.Forms.Keys.None;
			this.graph.ZoomStepFraction = 0.1;
			// 
			// nudTension
			// 
			this.nudTension.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.nudTension.DecimalPlaces = 2;
			this.nudTension.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
			this.nudTension.Location = new System.Drawing.Point(518, 392);
			this.nudTension.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.nudTension.Name = "nudTension";
			this.nudTension.Size = new System.Drawing.Size(120, 20);
			this.nudTension.TabIndex = 5;
			this.nudTension.Value = new decimal(new int[] {
            5,
            0,
            0,
            65536});
			// 
			// btnSave
			// 
			this.btnSave.Anchor = ( (System.Windows.Forms.AnchorStyles)( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left ) ) );
			this.btnSave.Location = new System.Drawing.Point(4, 412);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(113, 31);
			this.btnSave.TabIndex = 8;
			this.btnSave.Text = "Сохранить в файл";
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// sfd
			// 
			this.sfd.FileName = "out.txt";
			this.sfd.Filter = "\"Text files|*.txt|All files|*.*\";";
			// 
			// Visualizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(650, 446);
			this.Controls.Add(this.btnSave);
			this.Controls.Add(label2);
			this.Controls.Add(this.nudTension);
			this.Controls.Add(this.graph);
			this.Name = "Visualizer";
			this.Text = "Data Visualizer";
			this.Load += new System.EventHandler(this.Form1_Load);
			( (System.ComponentModel.ISupportInitialize)( this.nudTension ) ).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private ZedGraph.ZedGraphControl graph;
		private System.Windows.Forms.NumericUpDown nudTension;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.SaveFileDialog sfd;

	}
}

