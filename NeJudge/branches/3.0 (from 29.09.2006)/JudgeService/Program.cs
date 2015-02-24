using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Ne.Judge.Server
{
	static class Program
	{
		static void Main(string[] args)
		{
			ServiceBase[] toRun = { new Service() };
			ServiceBase.Run(toRun);
		}
	}
}