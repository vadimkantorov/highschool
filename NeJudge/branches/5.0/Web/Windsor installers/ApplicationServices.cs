using Broker;
using Castle.Facilities.TypedFactory;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using DataAccess;
using Model;
using Model.Factories;
using Web.Controllers;
using Web.DocumentFormatters;
using Web.Services;

namespace Web.WindsorInstallers
{
	public class ApplicationServices : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.AddFacility<TypedFactoryFacility>();
			container.Register(Component.For<IDatabaseConfiguration>().ImplementedBy<WebDatabaseConfiguration>(),
			                   Component.For<IClock>().ImplementedBy<SystemClock>(),
			                   Component.For<IFactory<IContestTypeHandler>>().ImplementedBy<Factory<IContestTypeHandler>>(),
			                   Component.For<IMonitorCache>().ImplementedBy<MonitorCache>(),
			                   Component.For<ITestInfoArchiveLinkCache>().ImplementedBy<TestInfoArchiveLinkCache>(),
							   Component.For<IFactory<IFormatter>>().ImplementedBy<Factory<IFormatter>>());
		}
	}
}