using System;
using System.Reflection;

using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class DataProvider
	{
		protected string _connectionString;

		public static void Initialize(string assPath, string connStr)
		{
			Assembly ass = Assembly.LoadFrom(assPath);
			Type prov = null;
			foreach (Type t in ass.GetTypes())
			{
				if (t.BaseType == typeof(DataProvider))
				{
					prov = t;
					break;
				}
			}
			if (prov == null)
				throw new TypeLoadException("Cannot load provider class");
			Provider = Activator.CreateInstance(prov,connStr) as DataProvider;
		}

		protected DataProvider(string connectionString)
		{
			_connectionString = connectionString;
		}

		internal static DataProvider Provider;

		protected ProblemManager _pm;
		protected SubmissionManager _sm;
		protected ContestManager _cm;
		protected UserManager _um;
		protected MessageManager _mm;
		protected LanguageManager _lm;
		protected TestManager _tm;

		internal ProblemManager ProblemManager
		{
			get { return _pm; }
		}

		internal ContestManager ContestManager
		{
			get { return _cm; }
		}

		internal UserManager UserManager
		{
			get { return _um; }
		}

		internal SubmissionManager SubmissionManager
		{
			get { return _sm; }
		}

		internal MessageManager MessageManager
		{
			get { return _mm; }
		}

		internal LanguageManager LanguageManager
		{
			get { return _lm; }
		}

		internal TestManager TestManager
		{
			get { return _tm; }
		}
	}
}