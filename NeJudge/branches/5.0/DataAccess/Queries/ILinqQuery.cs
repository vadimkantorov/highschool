using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace DataAccess.Queries
{
	public interface ILinqQuery<T>
	{
		IQueryable<T> Load(ISession session);
	}

	public abstract class LinqQuery<T> : ILinqQuery<T>, ICollectionQuery<T>
	{
		public IList<T> List(ISession session)
		{
			return Load(session).ToList();
		}

		public abstract IQueryable<T> Load(ISession session);
	}
}