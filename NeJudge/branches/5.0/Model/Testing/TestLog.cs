using System.Collections.Generic;
using System.Text;

namespace Model.Testing
{
	public class TestLog
	{
		public IList<TestRunInfo> CheckResults { get; set; }
		public string CompilationReport { get; set; }

		public override string ToString()
		{
			var result = new StringBuilder("CompilationReport:\n\n" + CompilationReport);
			if (CheckResults != null)
			{
				for (int i = 0; i < CheckResults.Count; i++)
				{
					result.AppendLine("Test #" + (i + 1));
					result.AppendLine(CheckResults[i].ToString());
				}
			}
			return result.ToString();
		}
	}
}