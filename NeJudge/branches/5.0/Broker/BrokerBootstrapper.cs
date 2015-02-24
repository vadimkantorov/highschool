using System.Collections.Generic;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Consumers;
using Broker.Impl.Printing;
using Broker.Interfaces;
using Broker.Jobs.SubmissionTesting;
using Broker.Scheduling;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataAccess;
using log4net.Config;
using Model;
using Model.Factories;
using NHibernate;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;

namespace Broker
{
	public class BrokerBootstrapper : NeBootstrapper, IBootstrapper
	{
		public IWindsorContainer Container
		{
			get { return container; }
		}

		void IBootstrapper.ConfigureContainer()
		{
			ConfigureContainer();
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();

			container.Kernel.Resolver.AddSubResolver(new PrioritySequenceResolver(container.Kernel));
			container.Register(
				Component.For<IEnumerable<Priority>>().ImplementedBy<BiasedPrioritySequence>(),
				Component.For<IJobQueue>().ImplementedBy<JobQueue>(),
				Component.For<IClock>().ImplementedBy<SystemClock>(),
				Component.For<ISubmissionQueueBuilder>().ImplementedBy<SubmissionQueueBuilder>(),
				Component.For<IPrinter>().ImplementedBy<Printer>(),
				Component.For<IJobMessageBuilder<SubmissionInfo>>().ImplementedBy<TestSubmissionMessageBuilder>(),
				Component.For<ITestsZipper>().ImplementedBy<TestsZipper>(),
				Component.For<IDatabaseConfiguration>().ImplementedBy<BrokerDatabaseConfiguration>(),
				Component.For<IWindsorContainer>().Instance(container),
				Component.For<IArchivedTestInfoRepository>().ImplementedBy<FileSystemArchivedTestInfoRepository>(),
				Component.For<IFactory<IContestTypeHandler>>().ImplementedBy<Factory<IContestTypeHandler>>(),
				Component.For<IMonitorService>().ImplementedBy<MonitorService>().LifeStyle.Transient
			);
		}

		public void ConfigureSystem()
		{
			container.Kernel.RegisterCustomDependencies(typeof(JobRequestConsumer), new { delaySeconds = 30 });
			container.Kernel.RegisterCustomDependencies(typeof(ITestsZipper), new { inputExt = "in", outputExt = "out" });
			container.Kernel.RegisterCustomDependencies(typeof(IArchivedTestInfoRepository), new { baseDirectory = @"..\..\..\Web\ArchivedTestInfos", webDirectory = "http://localhost:31452/" });
			container.Resolve<IFactory<IContestTypeHandler>>().RegisterAll();
		}

		public void StartServices()
		{
			container.AddFacility<NhibernateFacility>();

			using(new SessionScope(container.Resolve<ISessionFactory>()))
			{
				var queue = container.Resolve<IJobQueue>();
				var builder = container.Resolve<ISubmissionQueueBuilder>();
				builder.FillQueue(queue);
			}
		}

		public override void ConfigureBusFacility(RhinoServiceBusFacility f)
		{
			base.ConfigureBusFacility(f);
			f.AddMessageModule<NhMessageModule>();
		}

		public override void BeforeStart()
		{
			XmlConfigurator.Configure();

			ConfigureSystem();
            StartServices();
		}

		public override void AfterStart()
		{
			var bus = container.Resolve<IServiceBus>();
			bus.Send(new RefreshMonitors());
		}
	}
}
