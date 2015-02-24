using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Submissions
{
	public class FinishedSubmissionsSortedByTimeAsc : ICriteriaQuery<Submission>
	{
		public ICriteria Load(ISession session)
		{
			return session
				.CreateCriteria<Submission>()
				.AddOrder(Order.Asc("SubmittedAt"))
				.SetFetchMode("Problem", FetchMode.Join)
				.SetFetchMode("Problem.Contest", FetchMode.Join)
				.Add(Restrictions.Eq("TestingStatus", SubmissionTestingStatus.Finished));
		}
	}
}