using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.ContestTypeHandlers
{
	public abstract class ContestTypeHandler
	{
		protected int contestID;

		protected IMonitorBuilder monitorBuilder;
		protected IStatusBuilder statusBuilder;
		protected OutcomeMapper outcomeMapper;
		protected IJudgingManager judgingManager;

		public IMonitorBuilder MonitorBuilder
		{
			get { return monitorBuilder; }
		}

		public IStatusBuilder StatusBuilder
		{
			get { return statusBuilder; }
		}

		public OutcomeMapper OutcomeMapper
		{
			get { return outcomeMapper; }
		}

		public IJudgingManager JudgingManager
		{
			get { return judgingManager; }
		}

		protected ContestTypeHandler(int contestID)
		{
			this.contestID = contestID;
		}
	}
}
