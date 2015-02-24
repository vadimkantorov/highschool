using System;
using System.Collections.Generic;
using System.Linq;
using Broker.Scheduling;
using Model;

namespace Broker.Tests
{
	public class TestQueue : IJobQueue
	{
		readonly SortedDictionary<DateTime, Job> queue = new SortedDictionary<DateTime, Job>();
		readonly SystemClock systemClock = new SystemClock();

		public int Count
		{
			get { return queue.Count; }
		}

		public void Enqueue(object workItem, Priority priority)
		{
			EnqueueDelayed(new Job(workItem, priority), 0);
		}

		public void EnqueueDelayed(Job descriptor, int seconds)
		{
			descriptor.SecondsDelayed += seconds;
			var time = systemClock.CurrentTime.AddSeconds(seconds);
			if (queue.ContainsKey(time))
				time = time.AddMilliseconds(Count);
			queue.Add(time, descriptor);
		}

		public Job Dequeue()
		{
			if (queue.Count == 0)
				return null;
			
			var res = queue.FirstOrDefault().Value;
			Remove(res.Id);
			return res;
		}

		public bool Remove(Guid id)
		{
			queue.Remove(queue.Single(x => x.Value.Id == id).Key);
			return true;
		}
	}
}