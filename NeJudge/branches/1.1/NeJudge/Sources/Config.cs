using System.Configuration;
using System.IO;

namespace Ne.Judge
{
	/// <summary>
	/// Paths for all
	/// </summary>
	public class Config
	{
		public static readonly string Root = ConfigurationSettings.AppSettings["Root"];
		public static readonly string SubmissionsDirectory = Path.Combine(Root, ConfigurationSettings.AppSettings["SubmissionsDirectory"]);
		public static readonly string ProblemsDirectory = Path.Combine(Root, ConfigurationSettings.AppSettings["ProblemsDirectory"]);
		public static readonly string DfTest = Path.Combine(Root, ConfigurationSettings.AppSettings["DfTest"]);
		public static readonly string DfCompile = Path.Combine(Root, ConfigurationSettings.AppSettings["DfCompile"]);
		public static readonly string Report = ConfigurationSettings.AppSettings["Report"];
		
		private Config()
		{
		}
	}
}