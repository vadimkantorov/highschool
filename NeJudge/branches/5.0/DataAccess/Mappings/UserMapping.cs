using Model;

namespace DataAccess.Mappings
{
	public class UserMapping : EntityMapping<User>
	{
		public UserMapping()
		{
			Map(x => x.DisplayName);
			Map(x => x.UserName).Unique();
			Component(x => x.Password, c =>
			{
				c.Map(x => x.Hash);
				c.Map(x => x.Salt);
			});
		}
	}
}