using Castle.Windsor;

namespace Broker.Common
{
	public interface IBootstrapper
	{
		void ConfigureContainer();
		void ConfigureSystem();
		void StartServices();

		IWindsorContainer Container { get; }
	}
}