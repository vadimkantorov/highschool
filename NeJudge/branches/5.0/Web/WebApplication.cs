using System.Web.Mvc;
using System.Web.Routing;
using Broker;
using Broker.ContestTypeHandlers;
using DataAccess;
using Microsoft.Practices.ServiceLocation;
using Model;
using Model.Factories;
using Rhino.Security;
using Rhino.ServiceBus.Hosting;
using Web.DocumentFormatters;
using Web.Extensions;

namespace Web
{
	public class WebApplication : NhibernateEnabledApplication
	{
		protected override void RegisterRoutes(RouteCollection routes)
		{
			routes.MapRoute(
				"Default",                                              // Route name
				"{controller}/{action}/{id}",							// URL with parameters
				new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
				);
		}

		public override void ConfigureSystem()
		{
			Container.Resolve<IFactory<IContestTypeHandler>>().RegisterAll();
			Container.Resolve<IFactory<IFormatter>>().RegisterAll();
			SetupRhinoSecurity();
		}

		void SetupRhinoSecurity()
		{
			Rhino.Security.Security.Configure<User>(Container.Resolve<IDatabaseConfiguration>().DatabaseConfiguration, SecurityTableStructure.Prefix, u => new SecurityInfo(u.DisplayName, u.Id));
			ServiceLocator.SetLocatorProvider(() => new WindsorServiceLocator(Container));
		}

		public override void StartServices()
		{
			var host = new DefaultHost();
			host.UseContainer(Container);
			host.Start<WebBootstrapper>();
		}
	}
}