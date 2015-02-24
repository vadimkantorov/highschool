using System;
using Broker.Common.Messages.Broker;
using Broker.Interfaces;
using Model;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class RefreshMonitorsConsumer : ConsumerOf<RefreshMonitors>
	{
		public void Consume(RefreshMonitors message)
		{
			bus.DelaySend(clock.CurrentTime.AddSeconds(10), new RefreshMonitors());
			monitorService.UpdateMonitors();
		}

		public RefreshMonitorsConsumer(IClock clock, IServiceBus bus, IMonitorService monitorService)
		{
			this.clock = clock;
			this.bus = bus;
			this.monitorService = monitorService;
		}

		readonly IClock clock;
		readonly IServiceBus bus;
		readonly IMonitorService monitorService;
	}
}