using Model;

namespace DataAccess.Mappings
{
	public class LanguageMapping : EntityMapping<Language>
	{
		protected LanguageMapping()
		{
			Map(x => x.Name).Unique();
			Map(x => x.ShowToContestants);
		}
	}
}