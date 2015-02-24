using Broker;
using Castle.Windsor;
using NHibernate;
using Tests;
using Xunit;

namespace DataAccess.Tests
{
	public class SessionResolverTests
	{
		[Fact]
		public void can_resolve_session()
		{
			var container = new WindsorContainer();
			container.AddComponent<IDatabaseConfiguration,TestDatabaseConfiguration>();
			container.AddFacility<NhibernateFacility>();

			container.AddComponent<NeedsSession>();
			using(new SessionScope(container.Resolve<ISessionFactory>()))
				Assert.NotNull(container.Resolve<NeedsSession>());
		}

		public class NeedsSession
		{
			public NeedsSession(ISession session)
			{ }
		}
	}
}