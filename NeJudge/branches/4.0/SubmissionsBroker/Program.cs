using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace Ne.SubmissionsBroker
{
	static class Program
	{
		static void Main()
		{
			ServiceBase[] toRun = { new SubmissionsBrokerService() };

			ServiceBase.Run(toRun);
		}
	}
}