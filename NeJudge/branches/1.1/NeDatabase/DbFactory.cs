using System;

namespace Ne.Database
{
	/// <summary>
	/// Summary description for dbfactory.
	/// </summary>
	public class DbFactory
	{
		public static BaseDb ConstructDatabase()
		{
			switch (SqlConfig.DbType)
			{
					//case DatabaseType.MySql:
					//	return new mysqldb(SqlConfig.SqlConectionString);
				case "MsSql":
					return new MsSqlDb(SqlConfig.SqlConnectionString);
				case "PostgreSql":
					throw new NotImplementedException("Не реализовано");
				default:
					throw new NotImplementedException("Не реализовано");
			}
		}

		private DbFactory()
		{
		}
	}
}