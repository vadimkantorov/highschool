using System;
using System.Collections.Generic;
using Model;

namespace Web.ViewModels
{
	public class EditParticipationApplicationsForm
	{
		public int ContestId { get; set; }

		public string ContestName { get; set; }

		public IList<EditParticipationApplicationForm> Applications { get; set; }
	}

	public class EditParticipationApplicationForm
	{
		public int Id { get; set; }

		public bool IsApproved { get; set; }

		public string UserDisplayName { get; set; }

		public DateTime SubmittedAt { get; set; }
	}
}