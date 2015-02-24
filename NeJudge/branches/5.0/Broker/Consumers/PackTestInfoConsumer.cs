using Broker.Common.Messages.Broker;
using Broker.Interfaces;
using DataAccess.Queries.TestInfos;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class PackTestInfoConsumer : ConsumerOf<PackTestInfo>
	{
		public void Consume(PackTestInfo msg)
		{
			var testInfo = new TestInfoById{Id = msg.TestInfoId}.Load(session);
			var zipArchive = zipper.ZipTests(testInfo.Tests);
			var archiveUrl = archivedTestInfoRepository.StoreTestInfoArchive(msg.TestInfoId, zipArchive);
			
			bus.Notify(new PackTestInfoCompleted {ArchiveUrl = archiveUrl, TestInfoId = msg.TestInfoId});
		}

		public PackTestInfoConsumer(IServiceBus bus, ITestsZipper zipper, IArchivedTestInfoRepository archivedTestInfoRepository, ISession session)
		{
			this.bus = bus;
			this.zipper = zipper;
			this.archivedTestInfoRepository = archivedTestInfoRepository;
			this.session = session;
		}

		private readonly IServiceBus bus;
		readonly ITestsZipper zipper;
		readonly IArchivedTestInfoRepository archivedTestInfoRepository;
		readonly ISession session;
	}
}