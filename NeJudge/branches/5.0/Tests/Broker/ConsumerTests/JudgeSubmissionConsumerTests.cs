using System;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Consumers;
using DataAccess;
using Model;
using Model.Testing;
using NHibernate;
using Tests;
using Xunit;

namespace Broker.Tests.ConsumerTests
{
	public class JudgeSubmissionConsumerTests
	{
		[Fact]
		public void enqueues_submission_to_the_queue()
		{
			var queue = new TestQueue();
			var submission = new Submission
				{
					Source = new ProgramSource {Code = "test"},
					SubmittedAt = new DateTime(1990, 7, 7),
					Problem = new Problem
						{
							TestInfo = new TestInfo(),
							Contest = new Contest
								{
									Beginning = new DateTime(1990, 7, 7),
									Ending = new DateTime(1992, 7, 7)
								}
						}
            	};

			var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();
			
			using(var scope = new SessionScope(factory))
				scope.Session.Save(submission);
			using (var scope = new SessionScope(factory))
			{
				var consumer = new JudgeSubmissionConsumer(queue, scope.Session);
				consumer.Consume(new JudgeSubmission {SubmissionId = submission.Id});
			}

			Assert.Equal(1, queue.Count);
			Assert.Equal(submission.Source.Code, ((SubmissionInfo) queue.Dequeue().WorkItem).Source.Code);
		}
	}
}