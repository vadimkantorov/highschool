using System.Data;

namespace Ne.Database.New
{
	public interface ISqlQuery
	{
		object ExecuteScalar(string sqlCommand);
		IDataReader ExecuteReader(string sqlCommand, params SqlQueryParameter[] parameters);
	}
}
