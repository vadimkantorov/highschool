using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace ThermoTools
{
	static class Measure
	{
		public static void Start()
		{
			GetVoltage();
		}
		
		public static int GetTemperature()
		{
			double x1 = Properties.Config.Default.CalibratingVoltage1;
			double x2 = Properties.Config.Default.CalibratingVoltage2;
			double y1 = Properties.Config.Default.CalibratingTemperature1;
			double y2 = Properties.Config.Default.CalibratingTemperature2;
			
			double k = (y2-y1)/(x2-x1);
			double b = y1 - k * x1;
			double u = GetVoltage();
			return (int)(k*u + b);
		}

		public static double GetVoltage()
		{
			string portname = Properties.Config.Default.PortName;
			int cap = Properties.Config.Default.Capacity;
			double scale = Properties.Config.Default.MaxVoltage;

			using( SerialPort com = new SerialPort(portname) )
			{
				com.Open();
				com.DtrEnable = true;
				//�����
				//��� �������
				//Thread.Sleep(1000.0 / 20000.0);

				//������� �������
				com.DtrEnable = false;
				double d = 0;
				for( int i = 0; i <= cap-1; i++ )
				{
					com.RtsEnable = true;
					bool cts = com.CtsHolding;
					com.RtsEnable = false;
					if( cts )
						d += ( 1 << ( cap-1 - i ) );
				}
				d = scale * ( d / ( ( 1 << cap ) - 1 ) );
				com.DtrEnable = true;
				return d;
				//sw.WriteLine(d);
				//Thread.Sleep(1000.0 / 20000.0);
			}
		}
	}
}
