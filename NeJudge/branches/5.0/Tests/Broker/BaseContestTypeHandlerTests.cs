using System;
using Broker.ContestTypeHandlers;
using Model;
using Xunit;

namespace Broker.Tests
{
	public class BaseContestTypeHandlerTests
	{
		[Fact]
		public void can_form_handler_names()
		{
			Assert.Equal("Вася Петя", new Вася_ПетяHandler().Name);
			Assert.Equal("Bar", new Foo().Name);
			Assert.Throws<InvalidOperationException>(() => new Noname().Name);
		}

		class Вася_ПетяHandler : ContestTypeHandlerBase
		{
			public override IMonitorBuilder CreateMonitorBuilder(Contest contest)
			{
				throw new NotImplementedException();
			}

			public override string RenderMontior(object monitor)
			{
				throw new NotImplementedException();
			}
		}

		class Foo : ContestTypeHandlerBase
		{
			public override IMonitorBuilder CreateMonitorBuilder(Contest contest)
			{
				throw new NotImplementedException();
			}

			public override string RenderMontior(object monitor)
			{
				throw new NotImplementedException();
			}
		}

		class Noname : ContestTypeHandlerBase
		{
			public override IMonitorBuilder CreateMonitorBuilder(Contest contest)
			{
				throw new NotImplementedException();
			}

			public override string RenderMontior(object monitor)
			{
				throw new NotImplementedException();
			}
		}
	}
}