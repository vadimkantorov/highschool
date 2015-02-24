using Model;

namespace Broker.Common
{
	public interface ILanguageDiscovery
	{
		Language[] DiscoverAvailableLanguages();
	}

	public class LanguageDiscovery : ILanguageDiscovery
	{
		public Language[] DiscoverAvailableLanguages()
		{
			return new[]
			       	{
			       		new Language {Name = "Microsoft Visual C++ 10.0", ShowToContestants = true},
			       		new Language {Name = "Microsoft Visual C++ 10.0 (Testlib)", ShowToContestants = false},
			       		//new Language {Name = "Sun Java", ShowToContestants = true}
			       	};
		}
	}
}