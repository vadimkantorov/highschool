using Ne.Database.Classes;

namespace Ne.Database.Interfaces
{
	public abstract class MessageManager
	{
		protected string _connectionString;

		public MessageManager(string connectionString)
		{
			_connectionString = connectionString;
		}

		public abstract void AddMessage(Message message);
		public abstract void UpdateMessage(Message message);
		public abstract Message[] GetMessages(MessagesFilter filter);
		public abstract Message GetMessage(int messageID);
	}
}