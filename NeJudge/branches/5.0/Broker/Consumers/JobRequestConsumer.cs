using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Broker.Interfaces;
using Broker.Scheduling;
using Castle.Windsor;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class JobRequestConsumer : ConsumerOf<JobRequest>
	{
		public void Consume(JobRequest message)
		{
			object msg = new NoJobAvailable();

			var bestEntry = queue.Dequeue();
			if (bestEntry != null && bestEntry.SecondsDelayed < 3*delaySeconds) // removed poisonous submission
			{
				queue.EnqueueDelayed(bestEntry, delaySeconds);
				msg = BuildMessage(bestEntry);
			}
            
			bus.Reply(msg);
		}

		object BuildMessage(Job entry)
		{
			var msgBuilderType = typeof(IJobMessageBuilder<>).MakeGenericType(entry.WorkItem.GetType());
			var builder = (IJobMessageBuilder)container.Resolve(msgBuilderType);
			return builder.BuildMessage(entry);
		}

		public JobRequestConsumer(IJobQueue queue, IServiceBus bus, int delaySeconds, IWindsorContainer container)
		{
			this.queue = queue;
			this.bus = bus;
			this.delaySeconds = delaySeconds;
			this.container = container;
		}

		readonly IJobQueue queue;
		readonly IServiceBus bus;
		readonly int delaySeconds;
		readonly IWindsorContainer container;
	}
}