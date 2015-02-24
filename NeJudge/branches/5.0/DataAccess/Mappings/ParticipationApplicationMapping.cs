using Model;

namespace DataAccess.Mappings
{
	public class ParticipationApplicationMapping : EntityMapping<ParticipationApplication>
	{
		public ParticipationApplicationMapping()
		{
			References(x => x.User);
			References(x => x.Contest);
			Map(x => x.IsApproved);
			Map(x => x.SubmittedAt);
		}
	}
}