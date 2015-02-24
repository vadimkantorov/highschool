using NHibernate;
using NHibernate.Cfg;

namespace DataAccess
{
	public interface IDatabaseConfiguration
	{
		Configuration DatabaseConfiguration { get; }
	}
}