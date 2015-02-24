using System;
using Rhino.ServiceBus.DataStructures;

namespace Web.Services
{
	public interface ITestInfoArchiveLinkCache : ICache<Guid, string>
	{ }

	class TestInfoArchiveLinkCache : Cache<Guid, string>, ITestInfoArchiveLinkCache
	{ }
}