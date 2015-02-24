using Model;
using Tests;
using Xunit;

namespace DataAccess.Tests
{
	public class UserTests
	{
		[Fact]
		public void can_generate_user_id_without_going_to_the_database()
		{
			var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();
			using (var scope = new SessionScope(factory))
			{
				var user = new User
					{
                        DisplayName = "Test user"
					};
				scope.Session.Save(user);

				Assert.True(factory.Statistics.EntityInsertCount == 0);
				Assert.True(user.Id != 0);
			}

			Assert.True(factory.Statistics.EntityInsertCount > 0);
		}
	}
}