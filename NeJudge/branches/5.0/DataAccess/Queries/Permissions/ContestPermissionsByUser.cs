using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Permissions
{
	/*public class ContestPermissionsByUser<TOperation> : ICriteriaQuery<UserPermission<TOperation>>
	{
		public User User { get; set; }

		public Contest Contest { get; set; }
		
		public ICriteria Load(ISession session)
		{
			return session
				.CreateCriteria<UserPermission<TOperation>>()
				.Add(Restrictions.Eq("User", User))
				.Add(Restrictions.Eq("ObjectName", "Contest"))
				.Add(Restrictions.Eq("ObjectId", Contest.Id));
		}
	}*/
}