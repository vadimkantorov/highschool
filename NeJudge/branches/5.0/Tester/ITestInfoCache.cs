using System;
using Model.Testing;

namespace Tester
{
	public interface ITestInfoCache
	{
		TestInfo TryGet(Guid id);
		void Put(TestInfo testInfo);
	}
}