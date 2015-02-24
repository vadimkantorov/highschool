using log4net;

namespace Tester.Consumers
{
	public static class Logger
	{
		public static readonly ILog Log = LogManager.GetLogger(typeof (Logger));
	}
}