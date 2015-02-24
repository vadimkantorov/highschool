using Model;

namespace Broker.Common.Messages.Broker
{
	public class MonitorChanged
	{
		public int ContestId { get; set; }
		public object Monitor { get; set; }
	}
}