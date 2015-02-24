using System;
using System.Linq;
using System.Threading;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Model.Testing;
using Rhino.ServiceBus;
using Xunit;

namespace Tester.Tests
{
	public class TesterSanityTests : BaseTesterTests, 
		OccasionalConsumerOf<JobRequest>, 
		OccasionalConsumerOf<SubmissionTested>, 
		OccasionalConsumerOf<TestInfoRequest>
	{
		[Fact]
		public void sends_work_item_requests_on_startup()
		{
			using (bus.AddInstanceSubscription(this))
			{
				Assert.True(workItemRequestReceived.WaitOne(TimeSpan.FromSeconds(10)));
			}
		}

		[Fact(Skip = "Тестер реально что-то тестирует. Не будет работать на пустых TestInfo и SubmissionInfo")]
		public void tests_each_submission()
		{
			using (bus.AddInstanceSubscription(this))
			{
				var info = new SubmissionInfo();
				var msgs = Enumerable.Repeat(new TestSubmission {SubmissionInfo = info}, submissionSentCount).ToArray();
				bus.Send(msgs);

				allTestlogsReceived.WaitOne(TimeSpan.FromSeconds(5));
				Assert.Equal(submissionSentCount, testLogsReceived);
			}
		}

		public void Consume(SubmissionTested message)
		{
			Interlocked.Increment(ref testLogsReceived);
			if (testLogsReceived >= submissionSentCount)
				allTestlogsReceived.Set();
		}

		public void Consume(JobRequest message)
		{
			workItemRequestReceived.Set();
		}

		public void Consume(TestInfoRequest message)
		{
			bus.Reply(new TestInfoEnvelope {TestInfo = new TestInfo()});
		}

		int testLogsReceived;
		const int submissionSentCount = 5;
		readonly AutoResetEvent allTestlogsReceived = new AutoResetEvent(false);
		readonly AutoResetEvent workItemRequestReceived = new AutoResetEvent(false);
	}
}