using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlContestManager : ContestManager
	{
		public MsSqlContestManager(string s) : base(s)
		{}

		private static Contest FromReader(SqlDataReader sr)
		{
			Contest c = new Contest((DateTime) sr["Beginning"],
			                        (DateTime) sr["Ending"],
			                        (string) sr["Name"],
									(string) sr["Type"]);

			c.ID = (int) sr["ID"];
			return c;
		}

		private static void FillID(SqlCommand comm, object customParam)
		{
			comm.Parameters.AddWithValue("@id", customParam);
		}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Contest c = (Contest) customParam;
			comm.Parameters.AddWithValue("@id", c.ID);
			comm.Parameters.AddWithValue("@bg", c.Beginning);
			comm.Parameters.AddWithValue("@en", c.Ending);
			comm.Parameters.AddWithValue("@na", c.Name);
			comm.Parameters.AddWithValue("@ty" , c.Type);
		}

		public override void AddContest(Contest c)
		{
			string command = "INSERT INTO Contests (Beginning,Ending,Name,Type) VALUES (@bg,@en,@na,@ty);" +
			                 "SELECT ID FROM Contests WHERE ID=@@IDENTITY";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				c.ID = (int) q.ExecuteScalar(command, FillParams, c);
		}

		public override Contest GetContest(int contestID)
		{
			string command = "SELECT * FROM Contests WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, FillID, contestID) )
			{
				rdr.Read();
				return FromReader(rdr);
			}
		}

		public override Contest[] GetContests(ContestTime type)
		{
			string[] conds = new string[]
				{
					"(Beginning <= GETDATE() AND Ending >= GETDATE())",
					"Ending < GETDATE()",
					"Beginning > GETDATE()"
				};

			string command = "SELECT * FROM Contests WHERE ";

			bool first = true;
			int[] inds = new int[] {1, 2, 4};
			for ( int i = 0; i < 3; i++ )
				if ( ((int) type & inds[i]) != 0 )
				{
					if ( !first )
						command += " OR ";
					first = false;
					command += conds[i];
				}

			List<Contest> ret = new List<Contest>();

			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, null, null) )
			{
				while ( rdr.Read() )
					ret.Add(FromReader(rdr));
			}

			ret.Sort(delegate(Contest a, Contest b)
			        {
						if ( a.Time > b.Time ) return 1;
						if ( a.Time < b.Time ) return -1;
			         	return 0;
			        });
			return ret.ToArray();
		}

		public override void RemoveContest(int contestID)
		{
			string command = "DELETE FROM Contests WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillID, contestID);
		}

		public override void UpdateContest(Contest c)
		{
			string command = "UPDATE Contests SET Beginning=@bg, Ending=@en, Name=@na, Type=@ty WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, c);
		}

		public override bool ValidateID(int contestID)
		{
			string command = "SELECT ID FROM Contests WHERE ID=@id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				return (q.ExecuteScalar(command, FillID, contestID) != null);
		}
	}
}