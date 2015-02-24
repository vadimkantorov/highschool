namespace Model
{
	public class MessageReading : Entity
	{
		public Message Message { get; set; }

		public User User { get; set; }

		public bool IsRead { get; set; }
	}
}