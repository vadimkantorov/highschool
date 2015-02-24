using Broker.Scheduling;

namespace Broker.Interfaces
{
	public interface IJobMessageBuilder
	{
		object BuildMessage(Job job);
	}

	public interface IJobMessageBuilder<T> : IJobMessageBuilder
	{
	}
}