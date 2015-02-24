using System;

namespace Model
{
	public class ParticipationApplication : Entity
	{
		public User User { get; set; }

		public Contest Contest { get; set; }

		public bool IsApproved { get; set; }

		public DateTime SubmittedAt { get; set; }
	}
}