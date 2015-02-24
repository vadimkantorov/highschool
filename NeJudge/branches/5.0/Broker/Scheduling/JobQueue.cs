using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Utils;

namespace Broker.Scheduling
{
	public class JobQueue : IJobQueue
	{
		class Key : IComparable<Key>
		{
			public DateTime Timestamp { get; private set; }
			public Guid Id { get; private set; }

			public Key(DateTime timestamp, Guid id)
			{
				Timestamp = timestamp;
				Id = id;
			}

			public int CompareTo(Key other)
			{
				var cmp = Timestamp.CompareTo(other.Timestamp);
				if (cmp != 0)
					return cmp;
				return Id.CompareTo(other.Id);
			}
		}
		
		public JobQueue(IClock clock, IEnumerable<Priority> prioritySequence)
		{
			this.clock = clock;
			this.prioritySequence = prioritySequence;

			foreach (var priority in EnumUtils.Values<Priority>())
				queues[priority] = new SortedDictionary<Key, Job>();
		}

		public void Enqueue(object workItem, Priority priority)
		{
			EnqueueDelayed(new Job(workItem, priority), 0);
		}

		public void EnqueueDelayed(Job descriptor, int seconds)
		{
			descriptor.SecondsDelayed += seconds;
			var id = descriptor.Id;
			lock(locker)
			{
				if (timestamps.ContainsKey(id))
					throw new ArgumentException("Повторное добавление решения с ключом " + id);

				timestamps[id] = clock.CurrentTime.AddSeconds(seconds);
				queues[descriptor.Priority].Add(new Key(timestamps[id], descriptor.Id), descriptor);
			}
		}

		public bool Remove(Guid id)
		{
			lock(locker)
				return NonLockingRemove(id);
		}

		public Job Dequeue()
		{
			lock(locker)
			{
				if (queues.Values.All(q => !CanTakeFromQueue(q)))
					return null;
				
				var nonEmptyQueues =
					from priority in prioritySequence
					let queue = queues[priority]
					where CanTakeFromQueue(queue)
					select queue;

				var firstNonEmptyQueue = nonEmptyQueues.First();
				var descriptor = firstNonEmptyQueue.First().Value;

				NonLockingRemove(descriptor.Id);
				return descriptor;
			}
		}

		bool CanTakeFromQueue(IDictionary<Key, Job> queue)
		{
			return queue.Count > 0 && queue.First().Key.Timestamp <= clock.CurrentTime; 
		}

		bool NonLockingRemove(Guid id)
		{
			if (!timestamps.ContainsKey(id))
				return false;

			var key = new Key(timestamps[id], id);
			timestamps.Remove(id);
			foreach (var q in queues)
			{
				Job info;
				if (q.Value.TryGetValue(key, out info) && info.Id == id)
				{
					q.Value.Remove(key);
				}
			}

			return true;
		}

		readonly IClock clock;
		readonly IEnumerable<Priority> prioritySequence;

		readonly Dictionary<Priority, SortedDictionary<Key, Job>> queues = new Dictionary<Priority, SortedDictionary<Key, Job>>();
		readonly Dictionary<Guid, DateTime> timestamps = new Dictionary<Guid, DateTime>();
		readonly object locker = new object();
	}
}