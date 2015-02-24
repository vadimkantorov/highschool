using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlProvider : DataProvider
	{
		public MsSqlProvider(string s)
			: base(s)
		{
			_pm = new MsSqlProblemManager(s);
			_sm = new MsSqlSubmissionManager(s);
			_cm = new MsSqlContestManager(s);
			_mm = new MsSqlMessageManager(s);
			_um = new MsSqlUserManager(s);
			_lm = new MsSqlLanguageManager(s);
			_tm = new MsSqlTestManager(s);
		}
	}
}