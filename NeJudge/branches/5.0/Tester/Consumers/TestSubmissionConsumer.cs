using System;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Model.Factories;
using Model.Testing;
using Rhino.ServiceBus;
using Tester.Compilers;

namespace Tester.Consumers
{
	public class TestSubmissionConsumer : ConsumerOf<TestSubmission>
	{
		public void Consume(TestSubmission message)
		{
			var submissionInfo = message.SubmissionInfo;
			
			Logger.Log.InfoFormat("Начинаем тестировать решение ({0})", submissionInfo);
			var testInfoId = submissionInfo.TestInfoId;
			var testInfo = testInfoCache.TryGet(testInfoId);

			if (testInfo == null)
			{
				Logger.Log.InfoFormat(
					"Не найден TestInfo {0} для решения {1}, посылаем запрос на тесты",
					testInfoId,
					submissionInfo);
				bus.Send(new TestInfoRequest { TestInfoId = testInfoId });
				bus.DelaySend(DateTime.Now.AddSeconds(30), message);
				return;
			}
			try
			{
				Logger.Log.InfoFormat("Начинаем тестировать решение {0}", submissionInfo);
				var testLog = new Tester(compilers, submissionInfo, testInfo).Test();
				Logger.Log.InfoFormat("Успешно протестировали решение {0}", submissionInfo);
				Logger.Log.DebugFormat("Лог тестирования решения {0}: {1}", submissionInfo, testLog);
				bus.Send(new SubmissionTested { SubmissionId = submissionInfo.SubmissionId, TestLog = testLog, JobId = message.JobId});
			}
			catch (Exception error)
			{
				Logger.Log.WarnFormat("Не удалось проверить решение {0}", submissionInfo);
				Logger.Log.DebugFormat("Ошибка при проверке решения {0}: {1}", submissionInfo, error);
				bus.Send(new FailedToTestSubmission { Error = error.Message });
			}
			finally
			{
				bus.Send(new JobRequest());
			}
		}

		public TestSubmissionConsumer(IServiceBus bus, ITestInfoCache testInfoCache, IFactory<ICompiler> compilers)
		{
			this.bus = bus;
			this.testInfoCache = testInfoCache;
			this.compilers = compilers;
		}

		private readonly IServiceBus bus;
		private readonly ITestInfoCache testInfoCache;
		readonly IFactory<ICompiler> compilers;
	}
}