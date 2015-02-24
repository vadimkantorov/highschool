using System;
using Broker.Common;
using DataAccess;
using Model;
using Tests;
using Xunit;

namespace Broker.Tests
{
	public class SubmissionQueueBuilderTests
	{
		[Fact]
		public void adds_all_waiting_submissions_to_the_queue()
		{
			const int submitted = 11;
			var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();
			using(var scope = new SessionScope(factory))
			{
				var contest = new Contest();
				contest.Beginning = contest.Ending = new DateTime(1990, 7, 7);

				var problem = new Problem {Contest = contest};

				scope.Session.Save(contest);
				scope.Session.Save(problem);

				for (int i = 0; i < submitted; i++)
					scope.Session.Save(ConstructSubmission(i, problem));
			}

			using(var scope = new SessionScope(factory))
			{
				var builder = new SubmissionQueueBuilder(scope.Session);
				var queue = new TestQueue();
				builder.FillQueue(queue);

				Assert.Equal(submitted, queue.Count);
			}
		}

		static Submission ConstructSubmission(int i, Problem p)
		{
			var someDateTime = new DateTime(1990-i,7,7);
			return new Submission
				{
					Problem = p,
					Source = new ProgramSource {LanguageId = i.ToString(), Code = "abc"},
					SubmittedAt = someDateTime,
					Type = SubmissionType.Contestant,
					TestingStatus = SubmissionTestingStatus.Waiting
				};
		}
	}
}