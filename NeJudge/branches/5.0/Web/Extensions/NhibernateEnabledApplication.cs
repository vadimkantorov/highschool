using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Broker.Common;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.Windsor.Installer;
using DataAccess;
using NHibernate;
using Web.Extensions;

namespace Web
{
	public abstract class NhibernateEnabledApplication : HttpApplication, IBootstrapper
	{
		public IWindsorContainer Container
		{
			get { return container; }
		}

		protected void Application_Start()
		{
			container = new WindsorContainer(new XmlInterpreter());
			container.Register(Component.For<IWindsorContainer>().Instance(container));

			ConfigureContainer();
			ConfigureSystem();
			StartNHibernateAndOtherServices();

			RegisterRoutesWithStandardIgnores(RouteTable.Routes);
			var controllerFactory = container.ResolveOrDefault<IControllerFactory>();
			var binder = container.ResolveOrDefault<IModelBinder>();
			var filterProvider = container.ResolveOrDefault<IFilterProvider>();

			if(controllerFactory != null)
				ControllerBuilder.Current.SetControllerFactory(controllerFactory);
			if(binder != null)
				ModelBinders.Binders.DefaultBinder = binder;
			if (filterProvider != null)
			{
				var oldProvider = FilterProviders.Providers.Single(f => f is FilterAttributeFilterProvider); 
				FilterProviders.Providers.Remove(oldProvider); 
				FilterProviders.Providers.Add(filterProvider);
			}
		}

		public abstract void StartServices();

		public abstract void ConfigureSystem();

		public void ConfigureContainer()
		{
			Container.Install(FromAssembly.This());
		}

		protected abstract void RegisterRoutes(RouteCollection routes);

		void StartNHibernateAndOtherServices()
		{
			Container.AddFacility<NhibernateFacility>();
			StartServices();
		}

		void RegisterRoutesWithStandardIgnores(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("favicon.ico");

			RegisterRoutes(routes);
		}

		protected void Application_BeginRequest()
		{
			HttpContext.Current.Items[SessionScopeKey] = new SessionScope(container.Resolve<ISessionFactory>());
		}

		protected void Application_EndRequest()
		{
			((SessionScope)HttpContext.Current.Items[SessionScopeKey]).Dispose();
		}

		protected void Application_End()
		{
			container.Dispose();
		}

		static IWindsorContainer container;
		static readonly object SessionScopeKey = new object();
	}
}