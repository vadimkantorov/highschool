using System.Collections.Generic;
using System.Linq;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using log4net.Config;
using Model.Factories;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Impl;
using Tester.Compilers;

namespace Tester
{
	public class TesterBootstrapper : NeBootstrapper
	{
		public override void ConfigureBusFacility(RhinoServiceBusFacility f)
		{
			base.ConfigureBusFacility(f);
			facility = f;
		}

		protected override void ConfigureContainer()
		{
			base.ConfigureContainer();
			container.Register(Component.For<ITestInfoCache>().ImplementedBy<TestInfoMemoryCache>(),
				Component.For<IFactory<ICompiler>>().ImplementedBy<CompilerFactory>(),
				Component.For<IWindsorContainer>().Instance(container));
		}

		public override void BeforeStart()
		{
			XmlConfigurator.Configure();
		}

		public override void AfterStart()
		{
			container.Resolve<IFactory<ICompiler>>().RegisterAll();
			var msgs = Enumerable.Repeat(new JobRequest(), facility.ThreadCount);

			var bus = container.Resolve<IServiceBus>();
			bus.Send(msgs.ToArray());
		}

		RhinoServiceBusFacility facility;
	}
}