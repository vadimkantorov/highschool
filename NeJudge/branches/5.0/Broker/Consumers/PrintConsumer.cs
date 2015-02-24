using Broker.Common.Messages.Broker;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class PrintConsumer : ConsumerOf<Print>
	{
		public void Consume(Print msg)
		{
			printer.PrintText(msg.Text, msg.Watermark);
		}

		public PrintConsumer(IPrinter printer)
		{
			this.printer = printer;
		}

		readonly IPrinter printer;
	}
}