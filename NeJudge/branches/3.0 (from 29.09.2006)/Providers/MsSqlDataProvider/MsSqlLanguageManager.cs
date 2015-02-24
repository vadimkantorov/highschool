using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlLanguageManager : LanguageManager
	{
		public MsSqlLanguageManager(string s) : base(s)
		{}

		private static Language FromReader(SqlDataReader sr)
		{
			Language l = new Language((string) sr["ID"],
			                          (string) sr["Name"],
			                          (string) sr["Extension"],
			                          (string) sr["RunCommand"]);
			return l;
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			comm.Parameters.AddWithValue("@id", customParam);
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Language c = (Language) customParam;
			comm.Parameters.AddWithValue("@id", c.ID);
			comm.Parameters.AddWithValue("@na", c.Name);
			comm.Parameters.AddWithValue("@rc", c.RunCommand);

			if ( c.CompileScript != null )
				comm.Parameters.AddWithValue("@scr", c.CompileScript);
		}

		public override void AddLanguage(Language l)
		{
			string command = String.Format("INSERT INTO Languages (ID,Name,RunCommand{0}) VALUES(@id,@na,@rc{1})",
				l.CompileScript == null ? "" : ",Script", l.CompileScript == null ? "" : ",@scr");
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, l);
		}

		public override Language GetLanguage(string languageID)
		{
			string command = "SELECT * FROM Languages WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, languageID) )
			{
				rdr.Read();
				return FromReader(rdr);
			}
		}

		public override Language[] GetLanguages()
		{
			string command = "SELECT * FROM Languages";
			List<Language> tmp = new List<Language>();
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader sr = q.ExecuteReader(command, null, null) )
			{
				while ( sr.Read() )
					tmp.Add(FromReader(sr));
			}
			return tmp.ToArray();
		}

		public override void RemoveLanguage(string languageID)
		{
			string command = "DELETE FROM Languages WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillID, languageID);
		}

		public override void UpdateLanguage(Language l)
		{
			string command = String.Format("UPDATE Languages SET ID=@id,Name=@na,RunCommand=@rc{0} WHERE ID=@id",
				l.CompileScript == null ? "" : ",Script=@sc");
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, l);
		}

		public override bool ValidateID(string languageID)
		{
			string command = "SELECT ID FROM Languages WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, languageID) != null);
		}

		public override string GetScript(string languageID)
		{
			string command = "SELECT Script FROM Languages WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (string)q.ExecuteScalar(command, FillID, languageID);
		}
	}
}