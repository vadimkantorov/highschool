using Model;

namespace DataAccess.Mappings
{
	public class ContestMapping : EntityMapping<Contest>
	{
		public ContestMapping()
		{
			References(x => x.Owner);
			References(x => x.Judge);
			References(x => x.Announcement).Cascade.SaveUpdate();
			HasMany(x => x.Problems);
			HasMany(x => x.Participants);
			Map(x => x.Beginning);
			Map(x => x.Ending);
			Map(x => x.Type);
			Map(x => x.IsPublic);
		}
	}
}