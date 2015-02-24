using Tests;
using Xunit;

namespace DataAccess.Tests
{
	public class DefaultDatabaseConfigurationTests
	{
		[Fact]
		public void can_build_default_session_factory()
		{
			Assert.NotNull(new DefaultDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory());
		}
	}
}