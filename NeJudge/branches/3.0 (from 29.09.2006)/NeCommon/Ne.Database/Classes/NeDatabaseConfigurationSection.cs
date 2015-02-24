using System;
using System.Configuration;

namespace Ne.Database.Classes
{
	public class NeDatabaseConfigurationSection : ConfigurationSection
	{
		const string ConnectionStringKey = "connectionString";
		const string ProviderPathKey = "providerPath";

		[ConfigurationProperty(ConnectionStringKey, IsRequired = true)]
		public string ConnectionString
		{
			get { return (string) this[ConnectionStringKey]; }
			set { this[ConnectionStringKey] = value; }
		}

		[ConfigurationProperty(ProviderPathKey, IsRequired = true)]
		public string ProviderPath
		{
			get { return (string) this[ProviderPathKey]; }
			set { this[ProviderPathKey] = value; }
		}
	}
}
