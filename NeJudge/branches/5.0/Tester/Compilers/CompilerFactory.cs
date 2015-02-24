using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Model.Factories;

namespace Tester.Compilers
{
	public class CompilerFactory : Factory<ICompiler>
	{
		public CompilerFactory(IWindsorContainer outerContainer) : base(outerContainer)
		{
		}

		protected override void RegisterInTemporaryContainer(IWindsorContainer tempContainer)
		{
			tempContainer.Register(
					Component.For<ICompiler, Msvc>().ImplementedBy<LatestMsvc>(),
					Component.For<ICompiler>().ImplementedBy<Java>(),
					Component.For<ICompiler>().ImplementedBy<MsvcTestlib>()
					);
		}
	}
}