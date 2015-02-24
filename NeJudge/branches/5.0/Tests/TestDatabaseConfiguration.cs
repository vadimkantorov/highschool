using DataAccess;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Tests
{
	public class TestDatabaseConfiguration : DefaultDatabaseConfiguration
	{
		public TestDatabaseConfiguration()
		{
			Database = "nejudge_test";
		}

		protected override void ProcessConfiguration(Configuration cfg)
		{
			cfg.SetProperty(Environment.GenerateStatistics, true.ToString());
			new SchemaExport(cfg).Execute(false, true, false);
		}
	}
}