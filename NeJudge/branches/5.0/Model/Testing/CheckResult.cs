namespace Model.Testing
{
	public class CheckResult
	{
		public CheckStatus CheckStatus { get; set; }
		public string CheckerComment { get; set; }

		public override string ToString()
		{
			return string.Format("CheckStatus = {0}, CheckerComment = {1}", CheckStatus, CheckerComment);
		}
	}
}