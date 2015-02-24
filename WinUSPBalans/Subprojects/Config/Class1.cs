using System;
using System.IO;

namespace Ini
{
	public class Class1
	{
		public static void Main(string[] args)
		{
			Logger l = new Logger(@"H:\Elmer\C# Projects\notFinished\Config\Test files\test0.log");
			l.WriteLine("Тест лога",MessageType.Error);
			l.Close();
			Console.WriteLine(new StreamReader(@"H:\Elmer\C# Projects\notFinished\Config\Test files\test0.log").ReadToEnd());
			//////////////////////////////////////////////////////////////////////////
			Config s = new Config(@"H:\Elmer\C# Projects\notFinished\Config\Test files\test1.log");
			Console.WriteLine();
			s = s.Load(@"H:\Elmer\C# Projects\notFinished\Config\Test files\myConf.ini");
			Console.WriteLine(s.ToString());
			s.Close();
			Console.WriteLine(new StreamReader(@"H:\Elmer\C# Projects\notFinished\Config\Test files\test1.log").ReadToEnd());
			Console.ReadLine();
		}
	}
}
