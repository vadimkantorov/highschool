using Model;

namespace DataAccess.Mappings
{
	public class ProblemMapping : EntityMapping<Problem>
	{
		public ProblemMapping()
		{
			Component(x => x.Limits, c =>
			{
				c.Map(x => x.MemoryInBytes);
				c.Map(x => x.TimeInMilliseconds);
			});
			Component(x => x.TestInfo, c =>
				{
					c.Map(x => x.Id).Column("TestInfo_Id");
					c.Map(x => x.CheckerArguments);
					c.References(x => x.Checker).Cascade.SaveUpdate();
					c.References(x => x.TestVerifier).Cascade.SaveUpdate();
					c.HasMany(x => x.Tests).AsList(x => x.Column("`Index`")).KeyColumn("Problem_Id").Cascade.AllDeleteOrphan();
					c.ParentReference(x => x.Problem);
				});
			Map(x => x.ShortName);
			References(x => x.Statement).Cascade.SaveUpdate();
			References(x => x.Contest).Cascade.SaveUpdate();
		}
	}
}