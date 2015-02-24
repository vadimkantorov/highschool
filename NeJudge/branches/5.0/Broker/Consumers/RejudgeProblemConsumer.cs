using System;
using Broker.Common.Messages.Broker;
using Model;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class RejudgeProblemConsumer : ConsumerOf<RejudgeProblem>
	{
		readonly ISession session;

		public void Consume(RejudgeProblem message)
		{
			session
				.CreateQuery("UPDATE Submission s SET s.TestingStatus = :status, s.Type = :type WHERE s.Problem.Id = :problmId")
				.SetParameter("status", SubmissionTestingStatus.Waiting)
				.SetParameter("type", SubmissionType.Rejudge)
				.SetParameter("problemId", message.ProblemId)
				.ExecuteUpdate();
		}

		public RejudgeProblemConsumer(ISession session)
		{
			this.session = session;
		}
	}
}