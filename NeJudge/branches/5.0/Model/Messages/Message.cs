using System;

namespace Model
{
	public class Message : Entity
	{
		public DateTime SentAt { get; set; }

		public string Text { get; set; }

		public string Subject { get; set; }

		public User Author { get; set; }

		public Message Next { get; set; }

		public Contest Contest { get; set; }
	}
}