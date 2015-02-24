using System;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Broker.Consumers;
using Broker.Interfaces;
using Broker.Jobs.SubmissionTesting;
using Broker.Scheduling;
using Castle.Windsor;
using Rhino.Mocks;
using Rhino.ServiceBus;
using Xunit;

namespace Broker.Tests.ConsumerTests
{
	public class JobRequestConsumerTests
	{
		public JobRequestConsumerTests()
		{
			originalSubmittedAt = new DateTime(1990, 7, 7, 0, 0, 0);
			queue = new TestQueue();
			
			bus = MockRepository.GenerateMock<IServiceBus>();
			var container = new WindsorContainer()
				.AddComponent<IJobMessageBuilder<SubmissionInfo>, TestSubmissionMessageBuilder>();
			consumer = new JobRequestConsumer(queue, bus, 10, container);
		}

        [Fact]
		public void replies_with_null_if_the_queue_is_empty()
		{
			consumer.Consume(new JobRequest());
			bus.AssertWasCalled(x => x.Reply(Arg<object[]>.Matches(xs => xs.Length == 1 && xs[0] is NoJobAvailable)));
		}

		[Fact]
		public void doesnt_decrease_queue_size_because_of_failover_enqueueing()
		{
			PushOneEntryIntoTheQueue();

			consumer.Consume(new JobRequest());
			
			Assert.Equal(1, queue.Count);
			Assert.True(queue.Dequeue().SecondsDelayed > 0);
		}

        [Fact]
		public void replies_with_dequeued_submission()
		{
			PushOneEntryIntoTheQueue();
			consumer.Consume(new JobRequest());
			bus.AssertWasCalled(x => x.Reply(Arg<object[]>.Matches(s=>((TestSubmission)s[0]).SubmissionInfo.SubmissionId == 31337)));
		}

		[Fact]
		public void replies_with_single_TestSubmission_message()
		{
			PushOneEntryIntoTheQueue();
			consumer.Consume(new JobRequest());
			bus.AssertWasCalled(x => x.Reply(Arg<object[]>.Matches(s => s.Length == 1 && s[0] is TestSubmission)));
		}

		void PushOneEntryIntoTheQueue()
		{
			queue.Enqueue(new SubmissionInfo { SubmissionId = 31337, SubmittedAt = originalSubmittedAt }, Priority.High);
		}

		readonly JobRequestConsumer consumer;
		readonly IServiceBus bus;
		readonly TestQueue queue;
		readonly DateTime originalSubmittedAt;
	}
}