using FluentNHibernate.Mapping;
using Model;

namespace DataAccess.Mappings
{
	public class EntityMapping<T> : ClassMap<T>
		where T : Entity
	{
		protected EntityMapping()
		{
			Id(x => x.Id).GeneratedBy.HiLo(short.MaxValue.ToString());
			Version(x => x.Version);
			Map(x => x.SecurityKey);

			Not.LazyLoad();
		}
	}
}