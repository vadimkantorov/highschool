using System.Linq;
using Broker.Common.Messages.Broker;
using Broker.Interfaces;
using Model;
using NHibernate;
using Rhino.ServiceBus;

namespace Broker.Consumers
{
	public class UnpackTestInfoConsumer : ConsumerOf<UnpackTestInfo>
	{
		public void Consume(UnpackTestInfo msg)
		{
			var newTests = zipper.UnzipTests(msg.ZipArchive);
			var problem = session.Get<Problem>(msg.ProblemId);

			problem.TestInfo.InsertTestsAt(msg.InsertAt, newTests);
			session.Save(problem);
		}

		public UnpackTestInfoConsumer(ITestsZipper zipper, ISession session)
		{
			this.zipper = zipper;
			this.session = session;
		}

		readonly ITestsZipper zipper;
		readonly ISession session;
	}
}