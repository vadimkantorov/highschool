using System;
using Model;
using NHibernate;
using NHibernate.Criterion;
using Rhino.Security.Interfaces;

namespace Web.Extensions
{
	public class EntityInformationExtractor<T> : IEntityInformationExtractor<T>
		where T : Entity
	{
		public Guid GetSecurityKeyFor(T entity)
		{
			return entity.SecurityKey;
		}

		public string SecurityKeyPropertyName
		{
			get { return "SecurityKey"; }
		}

		public string GetDescription(Guid securityKey)
		{
			var id = session.CreateCriteria<Entity>().Add(Restrictions.Eq(SecurityKeyPropertyName, securityKey)).UniqueResult<Entity>().Id;
			return typeof (T).Name + " (" + id + ")";
		}

		public EntityInformationExtractor(ISession session)
		{
			this.session = session;
		}

		readonly ISession session;
	}
}