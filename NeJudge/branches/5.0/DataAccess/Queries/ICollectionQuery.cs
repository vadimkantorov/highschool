using System.Collections.Generic;
using NHibernate;

namespace DataAccess.Queries
{
	public interface ICollectionQuery<T>
	{
		IList<T> List(ISession session);
	}
}