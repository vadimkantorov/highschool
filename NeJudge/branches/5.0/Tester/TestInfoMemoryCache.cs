using System;
using System.Collections.Generic;
using Model.Testing;

namespace Tester
{
	public class TestInfoMemoryCache : ITestInfoCache
	{
		public TestInfo TryGet(Guid id)
		{
			lock (cache)
			{
				return cache.ContainsKey(id) ? cache[id] : null;
			}
		}

		public void Put(TestInfo testInfo)
		{
			lock (cache)
			{
				cache[testInfo.Id] = testInfo;
			}
		}

		private readonly IDictionary<Guid, TestInfo> cache = new Dictionary<Guid, TestInfo>();
	}
}