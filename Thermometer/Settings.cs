using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.IO.Ports;

namespace ThermoTools
{
	public partial class Settings : Form
	{
		public Settings()
		{
			InitializeComponent();
		}

		private void Settings_Load(object sender, EventArgs e)
		{
			string[] ports = SerialPort.GetPortNames();
			Array.Sort<string>(ports, delegate(string a, string b)
			{
				int l = Convert.ToInt32(a.Substring(3));
				int r = Convert.ToInt32(b.Substring(3));
				return Comparer<int>.Default.Compare(l, r);
			});
			ddlPorts.DataSource = ports;

			nudFreq.Value = Properties.Config.Default.Frequency;
			ddlPorts.SelectedItem = Properties.Config.Default.PortName;
			nudCapacity.Value = Properties.Config.Default.Capacity;
			nudMaxVoltage.Value = (decimal)Properties.Config.Default.MaxVoltage;
			
			if( Properties.Config.Default.Type == 'V' )
			{
				rbVolt.Checked = true;
				gbCalibration.Enabled = false;
			}
			else
			{
				rbThermo.Checked = true;
				gbCalibration.Enabled = true;
			}

			tbTemp1.Text = Properties.Config.Default.CalibratingTemperature1.ToString();
			tbTemp2.Text = Properties.Config.Default.CalibratingTemperature2.ToString();
			tbVoltage1.Text = Properties.Config.Default.CalibratingVoltage1.ToString();
			tbVoltage2.Text = Properties.Config.Default.CalibratingVoltage2.ToString();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			double v1 = 0, v2 = 0, t1 = 0, t2 = 0;
			
			try
			{
				v1 = Convert.ToDouble(tbVoltage1.Text);
				v2 = Convert.ToDouble(tbVoltage2.Text);
				t1 = Convert.ToDouble(tbTemp1.Text);
				t2 = Convert.ToDouble(tbTemp2.Text);
			}
			catch(Exception ex)
			{
				if( ex is OverflowException || ex is FormatException )
				{
					MessageBox.Show("Ошибка при вводе калибровочных данных. Введите корректные числа.", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
					throw;
			}

			if( v1 == v2 || t1 == t2 )
			{
				MessageBox.Show("Ошибка при вводе калибровочных данных. Введите различные значения для температур и различные для напряжений",
					"Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
			{
				Properties.Config.Default.CalibratingTemperature1 = t1;
				Properties.Config.Default.CalibratingTemperature2 = t2;
				Properties.Config.Default.CalibratingVoltage1 = v1;
				Properties.Config.Default.CalibratingVoltage2 = v2;
				Properties.Config.Default.Frequency = (int)nudFreq.Value;
				Properties.Config.Default.PortName = (string)ddlPorts.SelectedValue;
				Properties.Config.Default.Capacity = (int)nudCapacity.Value;
				Properties.Config.Default.MaxVoltage = (double)nudMaxVoltage.Value;
				if( rbVolt.Checked )
					Properties.Config.Default.Type = 'V';
				else
					Properties.Config.Default.Type = 'T';
				Properties.Config.Default.Save();
			}
		}

		private void rdThermo_CheckedChanged(object sender, EventArgs e)
		{
			gbCalibration.Enabled = rbThermo.Checked;
		}
	}
}