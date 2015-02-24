namespace Model.Testing
{
	public class TestRunInfo
	{
		public RunResult RunResult { get; set; }
		public CheckResult CheckResult { get; set; }

		public override string ToString()
		{
			return string.Format("RunResult: {{{0}}}\nCheckResult = {{{1}}}\n", RunResult, CheckResult);
		}
	}
}