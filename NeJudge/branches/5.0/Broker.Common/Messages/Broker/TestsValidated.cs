using System;
using Model.Testing;

namespace Broker.Common.Messages.Broker
{
	public class TestsValidated
	{
		public Guid TestInfoId { get; set; }
		
		public TestLog TestLog { get; set; }
	}
}