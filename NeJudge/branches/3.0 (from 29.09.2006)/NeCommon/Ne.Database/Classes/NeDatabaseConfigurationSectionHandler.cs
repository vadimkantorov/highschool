using System;
using System.Configuration;

namespace Ne.Database.Classes
{
	public class NeDatabaseConfigurationSectionHandler : ConfigurationSection
	{
		const string ConnectionStringKey = "ConnectionString";
		const string ProviderPathKey = "ProviderPath";

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
