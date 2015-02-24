using System;
using System.IO;
using System.Globalization;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ThermoTools
{
	public struct Pair<T1, T2>
	{
		T1 first;
		T2 second;

		public T1 First
		{
			get { return first; }
			set { first = value; }
		}

		public T2 Second
		{
			get { return second; }
			set { second = value; }
		}

		public Pair(T1 first, T2 second)
		{
			this.first = first;
			this.second = second;
		}
	}
	
	class FrameProcessor
	{
		List<Pair<DateTime, double>> data = new List<Pair<DateTime, double>>();

		public void AddPoint(DateTime dt, double val)
		{
			Pair<DateTime, double> p = new Pair<DateTime, double>(dt, val);
			data.Add(p);
		}

		public ReadOnlyCollection<Pair<DateTime, double>> GetData()
		{
			return data.AsReadOnly();
		}
		
		public void SaveToFile(string filename)
		{
			using(StreamWriter sw = new StreamWriter(filename))
			{
				foreach( Pair<DateTime, double> val in data )
					sw.WriteLine("{1} {0}.{2}", val.First, val.Second,val.First.Millisecond);
			}
		}
	}
}
