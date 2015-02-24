using Broker.RSB;
using Rhino.ServiceBus.Hosting;
using Rhino.ServiceBus.Impl;

namespace Broker.Common
{
	public class NeBootstrapper : AbstractBootStrapper
	{
		public override void ConfigureBusFacility(RhinoServiceBusFacility f)
		{
			f.UseMessageSerializer<CompressingXmlMessageSerializer>();
		}
	}
}