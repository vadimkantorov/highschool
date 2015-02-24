using System;
using Model.Testing;

namespace Broker.Common.Messages.Broker
{
	public class SubmissionTested
	{
		public int SubmissionId { get; set; }
		public Guid JobId { get; set; }
		public TestLog TestLog { get; set; }
	}
}