using System;
using DataAccess.Queries;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.ByteCode.Castle;
using NHibernate.Cfg;
using NHibernate.Context;
using NHibernate.Transaction;
using Environment = NHibernate.Cfg.Environment;

namespace DataAccess
{
	public class DefaultDatabaseConfiguration : IDatabaseConfiguration
	{
		readonly Lazy<Configuration> lazyConfiguration;
		
		public Configuration DatabaseConfiguration { get { return lazyConfiguration.Value; } }

		protected virtual void ProcessConfiguration(Configuration cfg)
		{}

		public DefaultDatabaseConfiguration()
		{
			Server = @"(local)";
			Database = "nejudge_test";

			lazyConfiguration = new Lazy<Configuration>(() =>
			                                            	{
			                                            		var cfg = BuildConfiguration();
			                                            		ProcessConfiguration(cfg);
			                                            		return cfg;
			                                            	});
		}

		private Configuration BuildConfiguration()
		{
			return Fluently
				.Configure()
				.Database(MsSqlConfiguration
				          	.MsSql2008
				          	.ConnectionString(c =>
				          	                  	{
				          	                  		c.Server(Server).Database(Database);

				          	                  		if (Username == null && Password == null)
				          	                  			c.TrustedConnection();
				          	                  		else
				          	                  			c.Username(Username).Password(Password);
				          	                  	}
				          	)
				//.ShowSql()
				)
				.Mappings(m => m.FluentMappings.AddFromAssemblyOf<DefaultDatabaseConfiguration>())
				.BuildConfiguration()
				.SetProperty(Environment.ProxyFactoryFactoryClass, typeof (ProxyFactoryFactory).AssemblyQualifiedName)
				.SetProperty(Environment.CurrentSessionContextClass, "thread_static");
			//.SetProperty(Environment.TransactionStrategy, typeof(AdoNetTransactionFactory).FullName);
		}

		public string Password { get; set; }

		public string Username { get; set; }

		public string Database { get; set; }

		public string Server { get; set; }
	}
}