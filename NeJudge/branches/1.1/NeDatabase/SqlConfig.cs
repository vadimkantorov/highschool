using System.Configuration;

namespace Ne.Database
{
	public class SqlConfig
	{
		public static readonly string SqlConnectionString = ConfigurationSettings.AppSettings["SqlConnectionString"];
		public static string DbType = ConfigurationSettings.AppSettings["DatabaseType"];
		
		private SqlConfig()
		{
		}
	}
}
