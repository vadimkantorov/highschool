using Broker.Common.Messages.Tester;
using Rhino.ServiceBus;

namespace Tester.Consumers
{
	public class TestInfoEnvelopeConsumer : ConsumerOf<TestInfoEnvelope>
	{
		public TestInfoEnvelopeConsumer(ITestInfoCache testInfoCache)
		{
			this.testInfoCache = testInfoCache;
		}

		public void Consume(TestInfoEnvelope message)
		{
			if (testInfoCache.TryGet(message.TestInfo.Id) == null)
			{
				Logger.Log.InfoFormat("Сохраняем TestInfo (id = {0})", message.TestInfo.Id);
				testInfoCache.Put(message.TestInfo);
			}
		}

		private readonly ITestInfoCache testInfoCache;
	}
}