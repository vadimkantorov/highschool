using Ne.Tester.Common;

namespace Ne.DomainModel
{
	public struct TestRunInfo
	{
		public RunResult RunResult;
		public CheckStatus CheckStatus;

		public string CheckerComment;

		public TestRunInfo(RunResult runResult, CheckStatus checkStatus, string comment)
		{
			RunResult = runResult;
			CheckStatus = checkStatus;
			CheckerComment = comment;
		}
	}
}