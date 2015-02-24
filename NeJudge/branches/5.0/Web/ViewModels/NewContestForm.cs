using System;
using System.Collections.Generic;
using Model;
using Web.Extensions;

namespace Web.ViewModels
{
	public class NewContestForm
	{
		public string Name { get; set; }

		public DateTime Beginning { get; set; }

		public DateTime Ending { get; set; }

		public Choose ContestTypes { get; set; }
	}

	public class EditContestForm : NewContestForm
	{
		public int ContestId { get; set; }

		public IEnumerable<Problem> Problems { get; set; }

		public bool IsPublic { get; set; }
	}

	public class ChangeContestAdministrationForm
	{
		public int ContestId { get; set; }
		
		public Choose Owners { get; set; }

		public Choose Judges { get; set; }	
	}
}