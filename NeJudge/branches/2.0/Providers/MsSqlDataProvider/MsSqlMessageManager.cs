using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using Ne.Database.Classes;
using Ne.Database.Interfaces;

namespace MsSqlDataProvider
{
	public class MsSqlMessageManager : MessageManager
	{
		public MsSqlMessageManager(string s) : base(s)
		{}

		private static void FillParams(SqlCommand comm, object customParam)
		{
			Message m = (Message) customParam;
			comm.Parameters.AddWithValue("@id", m.ID);
			comm.Parameters.AddWithValue("@pid", m.ProblemID);
			comm.Parameters.AddWithValue("@uid", m.UserID);
			comm.Parameters.AddWithValue("@time", m.Time);
			comm.Parameters.AddWithValue("@type", m.Type.ToString());
			comm.Parameters.AddWithValue("@cm", m.ContestantMessage);
			comm.Parameters.AddWithValue("@jm", m.JuryMessage);
		}

		private static Message FromReader(SqlDataReader rdr)
		{
			Message m = new Message();
			m.ID = (int) rdr["ID"];
			m.ProblemID = (int) rdr["ProblemID"];
			m.UserID = (string) rdr["UserID"];
			m.Time = (DateTime) rdr["Time"];
			m.Type = (MessageType) Enum.Parse(typeof (MessageType), (string) rdr["Type"]);
			m.ContestantMessage = (string) rdr["ContestantMessage"];
			m.JuryMessage = (string) rdr["JuryMessage"];
			return m;
		}

		public override void AddMessage(Message message)
		{
			string command = "INSERT INTO Messages (ProblemID, UserID, Time, Type, ContestantMessage, JuryMessage) " +
			                 "VALUES (@pid, @uid, @time, @type, @cm, @jm); SELECT ID FROM Messages WHERE ID = @@IDENTITY";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				message.ID = (int) q.ExecuteScalar(command, FillParams, message);
		}

		public override Message GetMessage(int messageID)
		{
			string command = "SELECT * FROM Messages WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, delegate(SqlCommand comm, object customParam)
			                                                     {
			                                                     	comm.Parameters.AddWithValue("@id", messageID);
			                                                     }, null) )
			{
				rdr.Read();
				return FromReader(rdr);
			}
		}

		public override Message[] GetMessages(MessagesFilter filter)
		{
			List<Message> messages = new List<Message>();
			string command = null;
			if( filter.ReguiredProblemID )
				command = "SELECT * FROM Messages WHERE ProblemID = @pid AND Type = @type";
			else
				command = "SELECT Messages.ID,Messages.ProblemID,Messages.UserID,Messages.Time,Messages.Type," +
						  "Messages.ContestantMessage,Messages.JuryMessage FROM Messages,Contests,Problems " +
						  "WHERE Messages.Type = @type AND Messages.ProblemID = Problems.ID AND Problems.ContestID = Contests.ID AND Contests.ID = @cid";
			if( filter.RequiredEmptyJuryMessage )
				command += " AND Messages.JuryMessage = ''";
			else
				command += " AND Messages.JuryMessage != ''";
			if(filter.RequiredUserID)
				command += " AND Messages.UserID=@uid";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
			using ( SqlDataReader rdr = q.ExecuteReader(command, delegate(SqlCommand comm, object customParam)
				                                                     {
				                                                     	comm.Parameters.AddWithValue("@type", filter.Type.ToString());
				                                                     	comm.Parameters.AddWithValue("@pid", filter.ProblemID);
				                                                     	comm.Parameters.AddWithValue("@cid", filter.ContestID);
																		if(filter.RequiredUserID)
																			comm.Parameters.AddWithValue("@uid", filter.UserID);
				                                                     }, null) )
				while ( rdr.Read() )
					messages.Add(FromReader(rdr));
			return messages.ToArray();
		}

		public override void UpdateMessage(Message message)
		{
			string command = "UPDATE Messages SET ProblemID = @pid, UserID = @uid, Time = @time, Type = @type, " +
			                 "ContestantMessage = @cm, JuryMessage = @jm WHERE ID = @id";
			using ( MsSqlQuery q = new MsSqlQuery(_connectionString) )
				q.ExecuteNonQuery(command, FillParams, message);
		}
	}
}