using System;
using Model.Testing;

namespace Model
{
	public class Submission : Entity
	{
		public Submission()
		{
			TestingStatus = SubmissionTestingStatus.Waiting;
			Type = SubmissionType.Contestant;
		}

		public User Author { get; set; }
		
		public Problem Problem { get; set; }
		
		public ProgramSource Source { get; set; }

		public DateTime SubmittedAt { get; set; }

		public TestLog TestLog { get; set; }

		public SubmissionResult Result { get; set; }

		public SubmissionType Type { get; set; }

		public SubmissionTestingStatus TestingStatus { get; set; }
	}
}