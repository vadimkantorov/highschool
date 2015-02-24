using System;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Broker.Consumers;
using DataAccess;
using Model;
using Model.Testing;
using NHibernate;
using Rhino.Mocks;
using Rhino.ServiceBus;
using Tests;
using Xunit;

namespace Broker.Tests.ConsumerTests
{
	public class TestInfoRequestConsumerTests
	{
		public TestInfoRequestConsumerTests()
		{
			var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory();

			originalTestInfo = new TestInfo
				{
					Checker = new ProgramSource {LanguageId = "C++", Code = "abc"},
					TestVerifier = new ProgramSource {LanguageId = "Java", Code = "cba"},
					CheckerArguments = "test arguments",
					Tests = new[]
						{
							new Test { Description = "Descr #1"}
						}
				};
			
			var problem = new Problem { TestInfo = originalTestInfo };
			
			WithTransaction(factory, session => session.Save(problem));

			bus = MockRepository.GenerateMock<IServiceBus>();
			WithTransaction(factory, session =>
			{
				var consumer = new TestInfoRequestConsumer(bus, session);
				consumer.Consume(new TestInfoRequest { TestInfoId = problem.TestInfo.Id });
			});
		}

		[Fact]
		public void replies_with_corresponding_TestInfo()
		{
			var args = bus.GetArgumentsForCallsMadeOn(x => x.Reply());
			var actualTestInfo = ((TestInfoEnvelope) ((object[])args[0][0])[0]).TestInfo;

			Assert.Equal(originalTestInfo.Id, actualTestInfo.Id);
			Assert.Equal(originalTestInfo.CheckerArguments, actualTestInfo.CheckerArguments);

			Assert.Equal(originalTestInfo.TestVerifier.Id, actualTestInfo.TestVerifier.Id);
			Assert.Equal(originalTestInfo.TestVerifier.LanguageId, actualTestInfo.TestVerifier.LanguageId);
			Assert.Equal(originalTestInfo.TestVerifier.Code, actualTestInfo.TestVerifier.Code);

			Assert.Equal(originalTestInfo.Checker.Id, actualTestInfo.Checker.Id);
			Assert.Equal(originalTestInfo.Checker.LanguageId, actualTestInfo.Checker.LanguageId);
			Assert.Equal(originalTestInfo.Checker.Code, actualTestInfo.Checker.Code);

			Assert.Equal(originalTestInfo.Tests.Count, actualTestInfo.Tests.Count);
			Assert.Equal(originalTestInfo.Tests[0].Description, actualTestInfo.Tests[0].Description);
		}
		
		[Fact]
		public void replies_with_single_TestInfoEnvelope_message()
		{
			bus.AssertWasCalled(x=>x.Reply(Arg<object[]>.Matches(s=>s.Length == 1 && s[0] is TestInfoEnvelope)));
		}

		static void WithTransaction(ISessionFactory factory, Action<ISession> action)
		{
			using (var session = factory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				action(session);
				tx.Commit();
			}
		}

		readonly IServiceBus bus;
		readonly TestInfo originalTestInfo;
	}
}