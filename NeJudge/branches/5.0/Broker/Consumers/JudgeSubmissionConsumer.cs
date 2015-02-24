using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Scheduling;
using DataAccess.Queries.Submissions;
using Model;
using NHibernate;
using NHibernate.Criterion;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class JudgeSubmissionConsumer : ConsumerOf<JudgeSubmission>
	{
		public void Consume(JudgeSubmission msg)
		{
			var submission = session.Get<Submission>(msg.SubmissionId);
			var submissionInfo = new SubmissionInfo(submission);
			
			queue.Enqueue(submissionInfo, Priority.High);
		}

		public JudgeSubmissionConsumer(IJobQueue queue, ISession session)
		{
			this.queue = queue;
			this.session = session;
		}

		readonly ISession session;
		readonly IJobQueue queue;
	}
}