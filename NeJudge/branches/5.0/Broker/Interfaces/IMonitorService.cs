using System.Linq;
using Broker.Common.Messages.Broker;
using Broker.ContestTypeHandlers;
using DataAccess.Queries.Submissions;
using Model;
using Model.Factories;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Interfaces
{
	public interface IMonitorService
	{
		void UpdateMonitors();
	}

	class MonitorService : IMonitorService
	{
		public void UpdateMonitors()
		{
			var submissions = new FinishedSubmissionsSortedByTimeAsc().Load(session).List<Submission>();

			var groupedByContests =	from s in submissions
									group s by s.Problem.Contest into g
									select new {Contest = g.Key, Submissions = g};

			foreach(var g in groupedByContests)
			{
				var handler = handlerFactory.Find(g.Contest.Type);
				var builder = handler.CreateMonitorBuilder(g.Contest);
				var monitor = builder.BuildMonitor(g.Submissions);
				bus.Notify(new MonitorChanged {ContestId = g.Contest.Id, Monitor = monitor});
			}
		}

		public MonitorService(ISession session, IServiceBus bus, IFactory<IContestTypeHandler> handlerFactory)
		{
			this.session = session;
			this.bus = bus;
			this.handlerFactory = handlerFactory;
		}

		readonly ISession session;
		readonly IServiceBus bus;
		readonly IFactory<IContestTypeHandler> handlerFactory;
	}
}