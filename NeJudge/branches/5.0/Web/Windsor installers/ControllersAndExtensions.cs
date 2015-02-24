using System;
using System.Reflection;
using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MvcContrib.Castle;
using Web.Extensions;

namespace Web.WindsorInstallers
{
	public class ControllersAndExtensions : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container
				.RegisterControllers(Assembly.GetExecutingAssembly())
				.Register(
				Component.For<IControllerFactory>().ImplementedBy<WindsorControllerFactory>(),
				Component.For<IModelBinder>().ImplementedBy<NeBinder>(),
				Component.For<IFilterProvider>().ImplementedBy<WindsorFilterAttributeFilterProvider>());
		}
	}
}