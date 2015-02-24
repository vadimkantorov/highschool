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
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
				Properties.Config.Default.Frequency = (int)nudFreq.Value;
				Properties.Config.Default.PortName = (string)ddlPorts.SelectedValue;
				Properties.Config.Default.Capacity = (int)nudCapacity.Value;
				Properties.Config.Default.MaxVoltage = (double)nudMaxVoltage.Value;
				
				Properties.Config.Default.Save();
		}
	}
}