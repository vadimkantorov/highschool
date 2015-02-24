using Xunit;

namespace Tests
{
	public class TestDatabaseConfigurationTests
	{
		[Fact]
		public void can_build_test_session_factory()
		{
			Assert.NotNull(new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory());			
		}
	}
}