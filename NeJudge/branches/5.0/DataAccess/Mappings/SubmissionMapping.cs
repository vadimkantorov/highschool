using Model;

namespace DataAccess.Mappings
{
	public class SubmissionMapping : EntityMapping<Submission>
	{
		public SubmissionMapping()
		{
			References(x => x.Author).Cascade.SaveUpdate();
			References(x => x.Problem).Cascade.SaveUpdate();
			References(x => x.Source).Cascade.SaveUpdate();
			Map(x => x.TestingStatus);
			Map(x => x.Type);
			Map(x => x.SubmittedAt);
			Component(x => x.Result, c =>
			{
				c.Map(x => x.Verdict);
				c.Map(x => x.Value);
			});
		}
	}
}