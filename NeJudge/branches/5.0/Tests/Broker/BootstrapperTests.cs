using System;
using System.Linq;
using Broker.ContestTypeHandlers;
using Castle.MicroKernel.Handlers;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using DataAccess;
using Model;
using Model.Factories;
using NHibernate;
using NHibernate.Context;
using Rhino.Mocks;
using Rhino.ServiceBus;
using Tests;
using Xunit;

namespace Broker.Tests
{
	public class BootstrapperTests
	{
		public BootstrapperTests()
		{
			container = new WindsorContainer();
			container.Register(
				Component.For<IDatabaseConfiguration>().ImplementedBy<TestDatabaseConfiguration>(),
				Component.For<IServiceBus>().Instance(MockRepository.GenerateStub<IServiceBus>())
			);
			bootstrapper = new BrokerBootstrapper();
			bootstrapper.InitializeContainer(container);
			bootstrapper.BeforeStart();
		}

		[Fact]
		public void can_resolve_components()
		{
			CurrentSessionContext.Bind(container.Resolve<ISessionFactory>().OpenSession());
			ContainerTester.Test(container);
		}

		[Fact]
		public void can_enumerate_standard_handlers()
		{
			var handlerFactory = container.Resolve<IFactory<IContestTypeHandler>>();
			Assert.NotNull(handlerFactory.Find("Icpc"));
			Assert.NotNull(handlerFactory.Find("Ioi"));
			Assert.NotNull(handlerFactory.Find("Kirov"));
		}

		readonly IWindsorContainer container;
		readonly BrokerBootstrapper bootstrapper;
	}
}