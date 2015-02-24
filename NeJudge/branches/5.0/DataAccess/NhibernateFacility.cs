using System;
using System.Linq;
using Castle.Core;
using Castle.MicroKernel;
using Castle.MicroKernel.ComponentActivator;
using Castle.MicroKernel.Facilities;
using Castle.MicroKernel.LifecycleConcerns;
using Castle.MicroKernel.Proxy;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Context;

namespace DataAccess
{
	public class NhibernateFacility : AbstractFacility
	{
		public class DropDisposalConcernForISession : FactoryMethodActivator<ISession>
		{
			public DropDisposalConcernForISession(ComponentModel model, IKernel kernel, ComponentInstanceDelegate onCreation, ComponentInstanceDelegate onDestruction) : base(model, kernel, onCreation, onDestruction)
			{
			}

			protected override void ApplyDecommissionConcerns(object instance)
			{
				//we don't want to let windsor dispose the session and interfere with SessionScope owning the Session
				//the other way would be using PerWebRequest
				instance = ProxyUtil.GetUnproxiedInstance(instance);
				ApplyConcerns(Model.Lifecycle.DecommissionConcerns.Where(x => !(x is DisposalConcern)), instance);
			}
		}

		protected override void Init()
		{
			var cfg = Kernel.Resolve<IDatabaseConfiguration>().DatabaseConfiguration;
			Kernel.Register(
				Component.For<ISession>().UsingFactoryMethod(ResolveSession).LifeStyle.Transient.Activator<DropDisposalConcernForISession>().OverWrite(),
				Component.For<Configuration>().Instance(cfg),
				Component.For<ISessionFactory>().Instance(cfg.BuildSessionFactory())
				);
		}

		static ISession ResolveSession(IKernel kernel)
		{
			var factory = kernel.Resolve<ISessionFactory>();
			
			if (!CurrentSessionContext.HasBind(factory))
				throw new DependencyResolverException("Невозможно разрешить сессию, так как или невозможно разрешить ISessionFactory, или нет сессии, привязанной к CurrensSessionContext");
			return factory.GetCurrentSession();
		}
	}
}