using System;
using System.Collections.Generic;
using Broker.Common;
using Broker.Scheduling;
using Model;
using Model.Utils;
using Xunit;

namespace Broker.Tests
{
	public class SubmissionQueueTests
	{
		public SubmissionQueueTests()
		{
			typeSequence = new BiasedPrioritySequence();
			queue = new JobQueue(new FastClock(), typeSequence);
		}

		[Fact]
		public void should_dequeue_with_all_priorities()
		{
			//Arrange
			FillQueue();
			var types = new Dictionary<SubmissionType, int>();
			foreach (var type in EnumUtils.Values<SubmissionType>())
				types[type] = 0;

			//Act
			while(true)
			{
				var top = queue.Dequeue();
				if(top == null)
					break;
				//TODO: чёкаво с приведениями типов
				types[((SubmissionInfo)top.WorkItem).Type]++;
			}

			//Assert
			foreach (var type in EnumUtils.Values<SubmissionType>())
				Assert.NotEqual(types[type], 0);
		}

		[Fact]
		public void should_dequeue_nothing_when_queue_is_empty()
		{
			Assert.Null(queue.Dequeue());
		}

		[Fact]
		public void nothing_is_lost()
		{
			//Arrange
			FillQueue();

			//Act
			int dequeuedSubmissionCount = 0;
			while (true)
			{
				var submissionInfo = queue.Dequeue();
				if (submissionInfo == null)
					break;
				dequeuedSubmissionCount++;
			}

			//Assert
			Assert.Equal(EnqueuedSubmissionCount, dequeuedSubmissionCount);
		}

		[Fact]
		public void nothing_remains_if_dequeued_all_enqueued_submissions()
		{
			//Arrange
			FillQueue();

			//Act
			for (int i = 0; i < EnqueuedSubmissionCount; i++)
				queue.Dequeue();

			//Assert
			Assert.Null(queue.Dequeue());
		}

		[Fact]
		public void failover_job_isnt_instantly_dequeued()
		{
			queue.Enqueue(1, Priority.High);
			var job = queue.Dequeue();
			queue.EnqueueDelayed(job, 100);

			Assert.Null(queue.Dequeue());
		}

		[Fact]
		public void untested_submission_is_tested_in_predefined_interval()
		{
			//Arrange
			var first = 1;
			var second = 2;

			queue.Enqueue(first, Priority.High);
			queue.Enqueue(second, Priority.High);

			//Act
			var firstJob = queue.Dequeue();
			queue.EnqueueDelayed(firstJob, 1);

			var secondJob = queue.Dequeue();
			firstJob = queue.Dequeue();

			//Assert
			Assert.Equal(second, secondJob.WorkItem);
			Assert.Equal(first, firstJob.WorkItem);
		}

		void FillQueue()
		{
			for (int i = 1; i <= EnqueuedSubmissionCount; i++)
				queue.Enqueue(ConstructSubmissionInfo(i), Priority.High);
		}

		SubmissionInfo ConstructSubmissionInfo(int submissionId)
		{
			var submissionType = (SubmissionType) (submissionId % EnumUtils.Values<SubmissionType>().Length);
			var createdAt = new DateTime(1990 - submissionId, 7, 7);
			var resourceUsage = new ResourceUsage { MemoryInBytes = 64, TimeInMilliseconds = 2 };
			return new SubmissionInfo
				{
					SubmissionId = submissionId,
					SubmissionLimits = resourceUsage,
					TestInfoId = someGuid,
					Source = new ProgramSource { Code = APlusBCppSource },
					RunAllTests = false,
					Type = submissionType,
					SubmittedAt = createdAt
				};
		}

		const string APlusBCppSource =
			@"#include <cstdio>

			int main()
			{
				int a,b;
				scanf(""%d%d"",&a,&b);
				printf(""%d"",a+b);

				return 0;
			}
			";

		const int EnqueuedSubmissionCount = 14;

		readonly Guid someGuid = new Guid("41D68F1E-B611-41B1-9D46-3DC15C39AE88");

		readonly JobQueue queue;
		readonly BiasedPrioritySequence typeSequence;
	}
}