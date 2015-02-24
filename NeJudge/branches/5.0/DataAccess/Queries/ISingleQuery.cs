using NHibernate;

namespace DataAccess.Queries
{
	public interface ISingleQuery<T>
	{
		T Load(ISession session);
	}
}