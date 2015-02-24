using System.Linq;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using DataAccess.Queries.TestInfos;
using Model;
using Model.Testing;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class TestInfoRequestConsumer : ConsumerOf<TestInfoRequest>
	{
		public void Consume(TestInfoRequest msg)
		{
			var testInfoFromDb = new TestInfoById { Id = msg.TestInfoId }.Load(session);
			var testInfo = testInfoFromDb == null ? null:
				new TestInfo
			{
                Checker = testInfoFromDb.Checker,
                CheckerArguments = testInfoFromDb.CheckerArguments,
                TestVerifier = testInfoFromDb.TestVerifier,
				Id = testInfoFromDb.Id,
				Tests = testInfoFromDb.Tests.Select(x => new Test
				                                         	{
				                                         		Description = x.Description,
																Input = x.Input,
																Output = x.Output,
																IsValid = x.IsValid,
																Points = x.Points
				                                         	}).ToArray()
			};

			bus.Reply(new TestInfoEnvelope {TestInfo = testInfo});
		}

		public TestInfoRequestConsumer(IServiceBus bus, ISession session)
		{
			this.bus = bus;
			this.session = session;
		}

		readonly IServiceBus bus;
		readonly ISession session;
	}
}