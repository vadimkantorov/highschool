using System;
using System.Linq;
using Model;
using Model.Testing;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;

namespace DataAccess.Queries.TestInfos
{
	public class TestInfoById : ISingleQuery<TestInfo>
	{
		public Guid Id { get; set;}
		
		public TestInfo Load(ISession session)
		{
			return session
				.CreateCriteria<Problem>()
				.Add(Restrictions.Eq("TestInfo.Id", Id))
				.UniqueResult<Problem>().TestInfo;
		}
	}
}