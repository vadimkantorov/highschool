using Castle.MicroKernel.Facilities;
using Rhino.Security.Interfaces;
using Castle.MicroKernel.Registration;
using Rhino.Security.Services;

namespace Web.Extensions
{
	public class RhinoSecurityFacility : AbstractFacility
	{
		protected override void Init()
		{
			Kernel.Register(Component.For<IAuthorizationService>()
			                	.ImplementedBy<AuthorizationService>()
			                	.LifeStyle.Transient,
			                Component.For<IAuthorizationRepository>()
			                	.ImplementedBy<AuthorizationRepository>()
			                	.LifeStyle.Transient,
			                Component.For<IPermissionsBuilderService>()
			                	.ImplementedBy<PermissionsBuilderService>()
			                	.LifeStyle.Transient,
			                Component.For<IPermissionsService>()
			                	.ImplementedBy<PermissionsService>()
			                	.LifeStyle.Transient);
		}
	}
}