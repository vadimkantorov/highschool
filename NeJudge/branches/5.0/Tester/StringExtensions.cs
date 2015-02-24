namespace Tester
{
	public static class StringExtensions
	{
		public static string Quote(this string s)
		{
			return "\"" + s.Replace("\"", "\\\"") + "\"";
		}
	}
}