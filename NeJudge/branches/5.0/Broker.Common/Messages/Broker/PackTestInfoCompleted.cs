using System;

namespace Broker.Common.Messages.Broker
{
	public class PackTestInfoCompleted
	{
		public string ArchiveUrl { get; set; }

		public Guid TestInfoId { get; set; }
	}
}