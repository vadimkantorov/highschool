using System;
using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.ParticipationApplication
{
	public class ApplicationsSortedByTimeDesc : ICriteriaQuery<Model.ParticipationApplication>
	{
		public ICriteria Load(ISession session)
		{
			return session
				.CreateCriteria<Model.ParticipationApplication>()
				.AddOrder(Order.Desc("SubmittedAt"))
				.CreateCriteria("Contest")
				.Add(Restrictions.IdEq(ContestId));
		}

		public int ContestId { get; set; }
	}
}