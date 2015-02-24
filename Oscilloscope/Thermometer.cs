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
		
		public static double GetVoltage()
		{
			string portname = Properties.Config.Default.PortName;
			int cap = Properties.Config.Default.Capacity;
			double scale = Properties.Config.Default.MaxVoltage;

			using( SerialPort com = new SerialPort(portname) )
			{
				com.Open();
				com.DtrEnable = true;
				//пауза
				//вкл питание
				//Thread.Sleep(1000.0 / 20000.0);

				//выборка сигнала
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
