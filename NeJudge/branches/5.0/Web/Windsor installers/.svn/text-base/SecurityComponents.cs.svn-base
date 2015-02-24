using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Rhino.Security.Interfaces;
using Web.Extensions;
using Web.Security;
using Web.Services;

namespace Web.WindsorInstallers
{
	public class SecurityComponents : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container
				.AddFacility<RhinoSecurityFacility>()
				.Register(Component.For<AuthorizationHelper>().LifeStyle.Transient,
				          Component.For<IUserSession>().ImplementedBy<UserSession>().LifeStyle.Transient,
				          Component.For<IAuthenticationService>().ImplementedBy<AuthenticationService>().LifeStyle.Transient,
				          Component.For(typeof (IEntityInformationExtractor<>)).ImplementedBy(typeof (EntityInformationExtractor<>)).LifeStyle.Transient,
				          Component.For<NhContestSecurityInterceptor>().LifeStyle.Transient,
				          Component.For<NhUserSecurityInterceptor>().LifeStyle.Transient,
				          Component.For<NhParticipationApplicationSecurityInterceptor>().LifeStyle.Transient,
				          Component.For<NhSubmissionSecurityInterceptor>().LifeStyle.Transient);
		}
	}
}