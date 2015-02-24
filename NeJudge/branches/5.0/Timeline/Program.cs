using System;
using Web;

namespace Timeline
{
	class Program
	{
		[STAThread]
		static void Main()
		{
			new Installer().Install(null);
			//new Printer(@"C:\printerDir").PrintText(File.ReadAllText(@"C:\work\nej\Tests\Tester\TestPrograms\RealProblem\tree_ai.cpp"), "TEAM29");
			/*Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());*/
		}
	}
}
