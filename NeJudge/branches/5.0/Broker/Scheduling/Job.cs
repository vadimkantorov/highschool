using System;

namespace Broker.Scheduling
{
	public class Job
	{
		public Guid Id { get; private set; }

		public object WorkItem { get; private set; }

		public Priority Priority { get; private set; }

		public int SecondsDelayed { get; set; }

		public Job(object workItem, Priority priority)
		{
			Id = Guid.NewGuid();
			WorkItem = workItem;
			Priority = priority;
		}
	}
}