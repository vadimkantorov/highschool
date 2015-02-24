using System;
using Broker.Common.Messages.Broker;
using Rhino.ServiceBus;
using Web.Services;

namespace Web.Consumers
{
	public class PackTestInfoCompletedConsumer : ConsumerOf<PackTestInfoCompleted>
	{
		public void Consume(PackTestInfoCompleted msg)
		{
			linkCache.Put(msg.TestInfoId, msg.ArchiveUrl);
		}

		public PackTestInfoCompletedConsumer(ITestInfoArchiveLinkCache linkCache)
		{
			this.linkCache = linkCache;
		}

		private readonly ITestInfoArchiveLinkCache linkCache;
	}
}