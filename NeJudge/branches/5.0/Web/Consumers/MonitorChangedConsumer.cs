using System;
using Broker.Common.Messages.Broker;
using Rhino.ServiceBus;
using Web.Controllers;

namespace Web.Consumers
{
	public class MonitorChangedConsumer : ConsumerOf<MonitorChanged>
	{
		public void Consume(MonitorChanged message)
		{
			monitorCache.Put(message.ContestId, message.Monitor);
		}

		public MonitorChangedConsumer(IMonitorCache monitorCache)
		{
			this.monitorCache = monitorCache;
		}

		readonly IMonitorCache monitorCache;
	}
}