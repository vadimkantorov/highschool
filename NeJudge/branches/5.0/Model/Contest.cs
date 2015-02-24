using System;
using System.Collections.Generic;

namespace Model
{
	public class Contest : Entity
	{
		public DateTime Beginning { get; set; }

		public DateTime Ending { get; set; }

		public bool IsPublic { get; set; }
		
		//public int FreezeMinutes { get; set; }

		public FormattedDocument Announcement { get; set; }

		public User Owner { get; set; }

		public User Judge { get; set; }

		public IList<Problem> Problems { get; set; }

		public IList<ParticipationApplication> Participants { get; set; }

		public string Type { get; set; }

		public Contest()
		{
			Problems = new List<Problem>();
			Participants = new List<ParticipationApplication>();
		}
	}
}