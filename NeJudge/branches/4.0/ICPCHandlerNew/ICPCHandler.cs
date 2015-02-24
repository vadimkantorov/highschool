using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.ContestTypeHandlers
{
	[ContestType("ICPC")]
	public class ICPCHandler : ContestTypeHandler
	{
		SubmissionInfoCache<ICPCSubmissionInfo> cache = 
			new SubmissionInfoCache<ICPCSubmissionInfo>();

		public ICPCHandler(int contestID)
			: base(contestID)
		{
			statusBuilder = new ICPCStatusBuilder(cache);
			outcomeMapper = new ICPCOutcomeMapper();
		}
	}
}
