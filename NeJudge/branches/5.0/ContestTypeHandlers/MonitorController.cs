using System.Web.Mvc;
using Model;

namespace ContestTypeHandlers
{
	public abstract class MonitorController : Controller
	{
		public abstract ActionResult Monitor(IMonitor monitor);
	}

	public abstract class MonitorController<T> : MonitorController
	{
		public abstract ActionResult Monitor(T monitor);

		public override ActionResult Monitor(IMonitor monitor)
		{
			return Monitor((T) monitor);
		}
	}
}