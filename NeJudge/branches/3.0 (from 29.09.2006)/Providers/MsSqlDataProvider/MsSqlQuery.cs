using System;
using System.Data.SqlClient;
using System.Xml;
using System.Configuration;
using Ne.Database.Classes;

namespace MsSqlDataProvider
{
	public delegate void ParameterAdder(SqlCommand comm, object customParam);

	public class MsSqlQuery : IDisposable
	{
		private string _connectionString;
		private SqlConnection _conn;

		public MsSqlQuery(string connectionString)
		{
			_connectionString = connectionString;
			_conn = new SqlConnection(_connectionString);
		}

		public void Dispose()
		{
			_conn.Close();
		}

		public SqlDataReader ExecuteReader(string command, ParameterAdder pa, object customParam)
		{
			SqlCommand cmd = new SqlCommand(command, _conn);

			if ( pa != null )
				pa(cmd, customParam);

			_conn.Open();
			return cmd.ExecuteReader();
		}

		public object ExecuteScalar(string command, ParameterAdder pa, object customParam)
		{
			SqlCommand cmd = new SqlCommand(command, _conn);

			if ( pa != null )
				pa(cmd, customParam);

			_conn.Open();
			return cmd.ExecuteScalar();
		}

		public int ExecuteNonQuery(string command, ParameterAdder pa, object customParam)
		{
			SqlCommand cmd = new SqlCommand(command, _conn);

			if ( pa != null )
				pa(cmd, customParam);

			_conn.Open();
			return cmd.ExecuteNonQuery();
		}

		public XmlDocument ExecuteXmlDocument(string command, ParameterAdder pa, object customParam)
		{
			SqlCommand cmd = new SqlCommand(command, _conn);

			if ( pa != null )
				pa(cmd, customParam);

			_conn.Open();
			XmlDocument doc = new XmlDocument();
			doc.Load(cmd.ExecuteXmlReader());
			return doc;
		}
	}
}