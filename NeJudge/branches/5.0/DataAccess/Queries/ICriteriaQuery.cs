using System.Collections.Generic;
using NHibernate;

namespace DataAccess.Queries
{
	public interface ICriteriaQuery<T>
	{
		ICriteria Load(ISession session);
	}

	public abstract class CriteriaQuery<T> : ICriteriaQuery<T>, ICollectionQuery<T>
	{
		public IList<T> List(ISession session)
		{
			return Load(session).List<T>();
		}

		public abstract ICriteria Load(ISession session);
	}
}