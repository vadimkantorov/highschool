using System;

namespace ICPCHandler
{
	class ICPCHandler : Ne.ContestTypeHandlers.ContestTypeHandler
	{
		public ICPCHandler()
		{
			sm = new ICPCStatusManager();
			mm = new ICPCMonitorManager();
			om = new ICPCOutcomeManager();
			tm = new ICPCTesterManager();
			itm = new ICPCTestLogManager();
		}
	}
}
