using Model;
using NHibernate;
using NHibernate.Criterion;

namespace DataAccess.Queries.Users
{
	public class UserByUserName : ISingleQuery<User>
	{
		public string UserName { get; set; }

		public User Load(ISession session)
		{
			return session
				.CreateCriteria<User>()
				.Add(Restrictions.Eq("UserName", UserName))
				.UniqueResult<User>();
		}
	}
}