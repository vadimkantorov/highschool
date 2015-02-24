using Broker.Common.Messages.Broker;
using Broker.Consumers;
using Rhino.Mocks;
using Xunit;

namespace Broker.Tests.ConsumerTests
{
	public class PrintConsumerTests
	{
		[Fact]
		public void calls_print_service_correctly()
		{
			var printService = MockRepository.GenerateMock<IPrinter>();
			var consumer = new PrintConsumer(printService);
			var msg = new Print {SyntaxHighlightingKey = "cppKey", Text = "Hi there!", Watermark = "Team.GOV!"};
			consumer.Consume(msg);
			printService.AssertWasCalled(x => x.PrintText(msg.Text, msg.Watermark));
		}
	}
}