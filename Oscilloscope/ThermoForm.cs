using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ThermoTools
{
	public partial class ThermoForm : Form
	{
		FrameProcessor fp = new FrameProcessor();
		double val = 0;

		public ThermoForm()
		{
			InitializeComponent();
		}

		void ApplySettings()
		{
			timer.Interval = (int)( 1000.0 / Properties.Config.Default.Frequency );
		}

		private void btnSettings_Click(object sender, EventArgs e)
		{
			new Settings().ShowDialog();
			ApplySettings();
		}

		private void ThermoForm_Load(object sender, EventArgs e)
		{
			sfd.InitialDirectory = Environment.CurrentDirectory;
			ApplySettings();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			val = Measure.GetVoltage();
			fp.AddPoint(DateTime.Now, val);
		}

		private void btnVis_Click(object sender, EventArgs e)
		{
			new Visualizer(fp).ShowDialog();
		}

		private void btnStartStop_Click(object sender, EventArgs e)
		{
			if( btnStartStop.Text == "Начать измерения" )
			{
				Measure.Start();
				timer.Start();
				btnStartStop.Text = "Остановить измерения";
				lblState.Text = "Идут измерения";
			}
			else
			{
				timer.Stop();
				btnStartStop.Text = "Начать измерения";
				lblState.Text = "Нет измерений";
			}
		}

		private void lblSaveToFile_Click(object sender, EventArgs e)
		{
			if( sfd.ShowDialog() == DialogResult.OK )
				fp.SaveToFile(sfd.FileName);
		}
	}
}