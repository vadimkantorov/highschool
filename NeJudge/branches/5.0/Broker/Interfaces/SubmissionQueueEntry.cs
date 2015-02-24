using System;
using Broker.Common;

namespace Broker
{
	/*public class SubmissionQueueEntry
	{
		public SubmissionQueueEntry(SubmissionInfo info, DateTime enqueuedAt)
		{
			Info = info;
			EnqueuedAt = enqueuedAt;
		}

		public SubmissionQueueEntry(SubmissionInfo info) 
			: this(info, info.SubmittedAt)
		{ }

		public SubmissionQueueEntry Delay(int seconds)
		{
			return new SubmissionQueueEntry(Info, EnqueuedAt.AddSeconds(seconds));
		}

		public readonly SubmissionInfo Info;

		public readonly DateTime EnqueuedAt;
	}*/
}