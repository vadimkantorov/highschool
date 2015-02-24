using System;

namespace Broker.Common.Messages.Tester
{
	public class TestSubmission
	{
		public Guid JobId { get; set; }
		
		public SubmissionInfo SubmissionInfo { get; set; }
	}
}