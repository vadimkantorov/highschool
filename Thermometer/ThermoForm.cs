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
		double abserror = 0;
		double val = 0;

		public ThermoForm()
		{
			InitializeComponent();
		}

		void ToggleForm()
		{
			if( Width != 389 )
			{
				Width = 389;
				btnMore.Text = "<<";
			}
			else
			{
				Width = 180;
				btnMore.Text = ">>";
			}
		}

		void UpdateRelativeError()
		{
			lblRelError.Text = Math.Round(abserror / val, 3).ToString();
		}
		
		void ApplySettings()
		{
			timer.Interval = (int)( 1000.0 / Properties.Config.Default.Frequency );
			if( Properties.Config.Default.Type == 'V' )
				Text = "Вольтметр";
			else
				Text = "Термометр";
			abserror = Properties.Config.Default.MaxVoltage / ( 1 << Properties.Config.Default.Capacity );
			lblAbsError.Text = Math.Round(abserror, 3).ToString();
		}

		void SetTemperature(int t)
		{
			LEDdisplay.LED[] leds = { led4, led3, led2, led1 };
			
			bool minus = t < 0;
			t = Math.Abs(t);
			int i = 0;
			while( t != 0 )
			{
				leds[i++].SetNumber(t % 10);
				t /= 10;
			}
			if( minus )
				leds[i].SetNumber(-1);
		}

		static double FractionalPart(double x)
		{
			return x - Math.Floor(x);
		}

		void SetVoltage(double p)
		{
			LEDdisplay.LED[] leds = { led1, led2, led3, led4 };
			double c = FractionalPart(p);
			int t = (int)Math.Floor(p);
			int i = 0;

			if( c != 0 )
			{
				while( t > 9 )
				{
					leds[i++].SetNumber(t % 10);
					t /= 10;
				}
				if( i == 3 )
					leds[i].SetNumber(t % 10);
				else
				{
					leds[i++].SetNumber(10 + ( t % 10 ));
					while( i != 4 )
					{
						leds[i++].SetNumber((int)Math.Floor(c * 10));
						c *= 10;
						c = FractionalPart(c);
					}
				}
			}
			else
			{
				SetTemperature((int)t);
			}
		}
		
		private void btnSettings_Click(object sender, EventArgs e)
		{
			new Settings().ShowDialog();
			ApplySettings();
		}

		private void ThermoForm_Load(object sender, EventArgs e)
		{
			ToggleForm();
			ApplySettings();
			UpdateRelativeError();
		}

		private void timer_Tick(object sender, EventArgs e)
		{
			if( Properties.Config.Default.Type == 'V' )
			{
				val = Measure.GetVoltage();
				SetVoltage(val);				
				fp.AddPoint(DateTime.Now, val);
			}
			else
			{
				val = Measure.GetTemperature();
				fp.AddPoint(DateTime.Now, val);
				SetTemperature((int)val);
			}
			UpdateRelativeError();
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
			}
			else
			{
				timer.Stop();
				btnStartStop.Text = "Начать измерения";
			}
		}

		private void btnErrors_Click(object sender, EventArgs e)
		{
			ToggleForm();
		}
	}
}