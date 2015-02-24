using System.Linq;
using Broker.Scheduling;
using Model.Utils;
using Xunit;

namespace Broker.Tests
{
	public class BiasedPrioritySequenceTests
	{
		[Fact]
		public void should_enumerate_all_priorities_within_100_iterations()
		{
			Priority[] first100 = new BiasedPrioritySequence().Take(100).ToArray();
			foreach (var type in EnumUtils.Values<Priority>())
				Assert.Contains(type, first100);
		}
	}
}