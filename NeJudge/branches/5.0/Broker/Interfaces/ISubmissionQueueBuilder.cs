using Broker.Scheduling;

namespace Broker
{
	public interface ISubmissionQueueBuilder
	{
		void FillQueue(IJobQueue queue);
	}
}