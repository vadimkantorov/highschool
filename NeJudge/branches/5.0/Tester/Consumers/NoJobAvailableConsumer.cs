using System;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Rhino.ServiceBus;

namespace Tester.Consumers
{
	public class NoJobAvailableConsumer : ConsumerOf<NoJobAvailable>
	{
		public NoJobAvailableConsumer(IServiceBus bus)
		{
			this.bus = bus;
		}

		public void Consume(NoJobAvailable message)
		{
			bus.DelaySend(DateTime.Now.AddSeconds(30), new JobRequest());
		}

		readonly IServiceBus bus;
	}
}