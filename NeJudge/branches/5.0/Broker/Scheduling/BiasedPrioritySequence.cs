using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Broker.Scheduling
{
	public class BiasedPrioritySequence : IEnumerable<Priority>
	{
		public IEnumerator<Priority> GetEnumerator()
		{
			return Priorities.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		static readonly IEnumerable<Priority> Priorities =
			Enumerable.Repeat(Priority.Highest, 8)
				.Concat(Enumerable.Repeat(Priority.High, 6))
				.Concat(Enumerable.Repeat(Priority.Normal, 4))
				.Concat(Enumerable.Repeat(Priority.Low, 2))
				.Concat(Enumerable.Repeat(Priority.Lowest, 1))
				.Repeat();
	}
}