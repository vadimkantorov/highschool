using System;
using Model;

namespace Broker.ContestTypeHandlers
{
	public class SchoolHandler : ContestTypeHandlerBase
	{
		protected override bool IsFailedTest(GenericVerdict verdict, Test test)
		{
			return base.IsFailedTest(verdict, test) && IsSample(test);
		}

		public override IMonitorBuilder CreateMonitorBuilder(Contest contest)
		{
			throw new NotImplementedException();
		}

		public override string RenderMontior(object monitor)
		{
			throw new NotImplementedException();
		}

		static bool IsSample(Test test)
		{
			return test.Points == 0;
		}
	}
	
	/*[ContestTypeHandler]
	public sealed class KirovHandler : SchoolHandler
	{}

	[ContestTypeHandler]
	public class IoiHandler : SchoolHandler
	{}*/
}