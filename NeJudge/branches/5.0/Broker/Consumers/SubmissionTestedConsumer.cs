using Broker.Common.Messages.Broker;
using Broker.ContestTypeHandlers;
using Broker.Scheduling;
using Model;
using Model.Factories;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class SubmissionTestedConsumer : ConsumerOf<SubmissionTested>
	{
		public void Consume(SubmissionTested message)
		{
			var submission = session.Get<Submission>(message.SubmissionId);

			if (queue.Remove(message.JobId) && submission.TestingStatus != SubmissionTestingStatus.Finished )
			{
				var handler = handlerFactory.Find(submission.Problem.Contest.Type);

				submission.TestLog = message.TestLog;
				submission.Result = handler.BuildSubmissionResult(submission.Problem.TestInfo.Tests, message.TestLog);
				submission.TestingStatus = SubmissionTestingStatus.Finished;

				session.Save(submission);
			}
		}

		public SubmissionTestedConsumer(IJobQueue queue, ISession session, IFactory<IContestTypeHandler> handlerFactory)
		{
			this.queue = queue;
			this.session = session;
			this.handlerFactory = handlerFactory;
		}

		readonly IJobQueue queue;
		readonly ISession session;
		readonly IFactory<IContestTypeHandler> handlerFactory;
	}
}