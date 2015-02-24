using System;
using Broker.Common;
using log4net.Config;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;
using Tests;

namespace Tester.Tests
{
	public class BaseTesterTests : RsbTestFixture, IDisposable
	{
		public BaseTesterTests()
		{
			XmlConfigurator.Configure();
			stubBrokerHost = new DefaultHost();
			stubBrokerHost.UseStandaloneCastleConfigurationFileName(@".\..\..\Tester\BrokerEndpoint.config");
			stubBrokerHost.Start<TestBootstrapper>();
			bus = stubBrokerHost.Container.Resolve<IServiceBus>();

			testerHost = new RemoteAppDomainHost(typeof(TesterBootstrapper));
			testerHost.Start();
		}

		public void Dispose()
		{
			stubBrokerHost.Dispose();
			testerHost.Close();
		}

		private class TestBootstrapper : NeBootstrapper
		{
			protected override bool IsTypeAcceptableForThisBootStrapper(Type t)
			{
				return t.Namespace.StartsWith(GetType().Namespace);
			}
		}

		protected readonly RemoteAppDomainHost testerHost;
		protected readonly DefaultHost stubBrokerHost;
		protected readonly IServiceBus bus;
	}
}