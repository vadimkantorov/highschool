using DataAccess;
using NHibernate.Cfg;
using Web.Extensions;
using Environment = NHibernate.Cfg.Environment;

namespace Web
{
	public class WebDatabaseConfiguration : DefaultDatabaseConfiguration
	{
		protected override void ProcessConfiguration(Configuration cfg)
		{
			cfg.SetProperty(Environment.CurrentSessionContextClass, "web");
		}
	}
}