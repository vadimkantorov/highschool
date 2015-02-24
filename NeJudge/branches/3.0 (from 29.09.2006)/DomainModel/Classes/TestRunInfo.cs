namespace Ne.Database.Classes
{
	public struct TestRunInfo
	{
		public RunResult RunResult;
		public CheckStatus CheckStatus;

		public string CheckerComment;

		public TestRunInfo(RunResult rResult, CheckStatus cStatus, string comment)
		{
			RunResult = rResult;
			CheckStatus = cStatus;
			CheckerComment = comment;
		}
	}
}