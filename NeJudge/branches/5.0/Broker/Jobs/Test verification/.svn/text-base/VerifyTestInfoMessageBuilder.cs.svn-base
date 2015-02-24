using System;
using Broker.Common.Messages.Tester;
using Broker.Interfaces;
using Broker.Scheduling;
using Model.Testing;

namespace Broker.Jobs.TestVerification
{
	public class VerifyTestInfoMessageBuilder : IJobMessageBuilder<Guid>
	{
		public object BuildMessage(Job job)
		{
			return new VerifyTestInfo { JobId = job.Id, TestInfoId = (Guid)job.WorkItem };
		}
	}
}