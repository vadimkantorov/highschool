using System;

namespace Broker.Common.Messages.Tester
{
	public class VerifyTestInfo
	{
		public Guid JobId { get; set; }

		public Guid TestInfoId { get; set; }
	}
}