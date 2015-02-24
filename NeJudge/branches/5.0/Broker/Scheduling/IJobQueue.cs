using System;

namespace Broker.Scheduling
{
	public interface IJobQueue
	{
		void Enqueue(object workItem, Priority priority);

		void EnqueueDelayed(Job descriptor, int seconds);

		Job Dequeue();

		bool Remove(Guid id);
	}
}