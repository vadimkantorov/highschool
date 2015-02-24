using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Submissions
{
	public class SubmissionsSortedByTimeDesc : ICriteriaQuery<Submission>
	{
		public ICriteria Load(ISession session)
		{
			var crit = session
				.CreateCriteria<Submission>()
				.AddOrder(Order.Desc("SubmittedAt"));
			crit
				.CreateCriteria("Problem.Contest")
				.Add(Restrictions.IdEq(ContestId));
			return crit;
		}

		public int ContestId { get; set; }
	}
}