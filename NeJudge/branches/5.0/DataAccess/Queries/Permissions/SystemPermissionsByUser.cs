using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Permissions
{
	/*public class SystemPermissionsByUser<TOperation> : ICriteriaQuery<UserPermission<TOperation>>
	{
		public User User { get; set; }

		public ICriteria Load(ISession session)
		{
			return session
				.CreateCriteria<UserPermission<TOperation>>()
				.Add(Restrictions.Eq("User", User))
				.Add(Restrictions.Eq("ObjectName", "System"));
		}
	}*/
}