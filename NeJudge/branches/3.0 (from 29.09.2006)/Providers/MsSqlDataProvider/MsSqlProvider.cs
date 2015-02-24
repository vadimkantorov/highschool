using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlProvider : DataProvider
	{
		public MsSqlProvider(string s)
			: base(s)
		{
			problemManager = new MsSqlProblemManager(s);
			submissionManager = new MsSqlSubmissionManager(s);
			contestManager = new MsSqlContestManager(s);
			messageManager = new MsSqlMessageManager(s);
			userManager = new MsSqlUserManager(s);
			languageManager = new MsSqlLanguageManager(s);
			testManager = new MsSqlTestManager(s);
		}
	}
}