using System;
using System.Collections.Generic;
using Model;

namespace Broker.Tests
{
	public class FastClock : IClock
	{
		public DateTime CurrentTime
		{
			get
			{
				timeEnumerator.MoveNext();
				return timeEnumerator.Current;
			}
		}

		public FastClock()
		{
			timeEnumerator = FastTime(new DateTime(1990, 7, 7, 0, 0, 0));
		}

		static IEnumerator<DateTime> FastTime(DateTime b)
		{
			for (int secs = 0; ; secs += 2)
				yield return b.AddSeconds(secs);
		}

		readonly IEnumerator<DateTime> timeEnumerator;
	}
}