using System;
using System.Configuration;
using System.Reflection;

using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class DataProvider
	{
		protected string connectionString;

		public static void Initialize()
		{
			object section = ConfigurationManager.GetSection("neDatabase");

			if ( section == null || !(section is NeDatabaseConfigurationSection) )
				throw new ConfigurationErrorsException("Configuration file doesn't contain valid <neDatabase>" +
					" configuration section");

			NeDatabaseConfigurationSection handler = (NeDatabaseConfigurationSection) section;
			Assembly provider = null;
			Type providerType = null;
			string msg = null;

			try
			{
				provider = Assembly.LoadFrom(handler.ProviderPath);

				foreach ( Type t in provider.GetTypes() )
				{
					if ( t.BaseType == typeof(DataProvider) )
					{
						providerType = t;
						break;
					}
				}
			}
			catch ( Exception ex )
			{
				msg = string.Format("Cannot load data provider class from assembly {0}", handler.ProviderPath);
				throw new ConfigurationErrorsException(msg, ex);
			}

			if ( providerType == null )
			{
				msg = string.Format("Assembly {0} doesn't contain valid data provider", handler.ProviderPath);
				throw new ConfigurationErrorsException(msg);
			}

			try
			{
				Provider = (DataProvider) Activator.CreateInstance(providerType, handler.ConnectionString);
			}
			catch ( Exception ex )
			{
				throw new ConfigurationErrorsException("Cannot create data provider instance", ex);
			}
		}

		protected DataProvider(string connectionString)
		{
			this.connectionString = connectionString;
		}

		internal static DataProvider Provider;

		protected ProblemManager problemManager;
		protected SubmissionManager submissionManager;
		protected ContestManager contestManager;
		protected UserManager userManager;
		protected MessageManager messageManager;
		protected LanguageManager languageManager;
		protected TestManager testManager;

		internal ProblemManager ProblemManager
		{
			get { return problemManager; }
		}

		internal ContestManager ContestManager
		{
			get { return contestManager; }
		}

		internal UserManager UserManager
		{
			get { return userManager; }
		}

		internal SubmissionManager SubmissionManager
		{
			get { return submissionManager; }
		}

		internal MessageManager MessageManager
		{
			get { return messageManager; }
		}

		internal LanguageManager LanguageManager
		{
			get { return languageManager; }
		}

		internal TestManager TestManager
		{
			get { return testManager; }
		}
	}
}