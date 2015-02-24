using System.Linq;
using Broker.Common;
using Broker.Scheduling;
using DataAccess.Queries.Submissions;
using Model;
using NHibernate;

namespace Broker
{
	public class SubmissionQueueBuilder : ISubmissionQueueBuilder
	{
		public void FillQueue(IJobQueue queue)
		{
			var submissionInfos = new WaitingSubmissionsSortedByTimeAsc()
				.Load(session)
				.List<Submission>()
				.Select(x => new SubmissionInfo(x));
			foreach(var submissionInfo in submissionInfos)
				queue.Enqueue(submissionInfo, Priority.Normal);
		}

		public SubmissionQueueBuilder(ISession session)
		{
			this.session = session;
		}

		readonly ISession session;
	}
}