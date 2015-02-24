using System;
using Broker.Common;
using Broker.Common.Messages.Tester;
using Broker.Interfaces;
using Broker.Scheduling;

namespace Broker.Jobs.SubmissionTesting
{
	public class TestSubmissionMessageBuilder : IJobMessageBuilder<SubmissionInfo>
	{
		public object BuildMessage(Job job)
		{
			return new TestSubmission {JobId = job.Id, SubmissionInfo = (SubmissionInfo) job.WorkItem};
		}
	}
}