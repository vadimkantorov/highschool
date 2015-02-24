using Model;

namespace DataAccess.Mappings
{
	public class TestMapping : EntityMapping<Test>
	{
		public TestMapping()
		{
			References(x => x.Problem);
			Map(x => x.Description);
			Map(x => x.Points);
			Map(x => x.IsValid);
			References(x => x.Input).Cascade.SaveUpdate();
			References(x => x.Output).Cascade.SaveUpdate();
		}
	}
}