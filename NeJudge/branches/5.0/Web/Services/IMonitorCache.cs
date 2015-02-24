using Web.Services;

namespace Web.Controllers
{
	public interface IMonitorCache : ICache<int, object>
	{ }

	class MonitorCache : Cache<int, object>, IMonitorCache
	{ }
}