using System;
using System.IO;
using System.Threading;
using Broker;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using DataAccess;
using Model;
using Model.Testing;
using Rhino.ServiceBus;
using Rhino.ServiceBus.Hosting;
using Tester;
using Xunit;

namespace Tests.Integration
{
	public class UnzipTests_Submit_ReceiveResult : RsbTestFixture
	{
		class TestHost : DefaultHost
		{
			private TestHost(IWindsorContainer container)
			{
				UseContainer(container);
				Container.AddComponent<IDatabaseConfiguration, TestDatabaseConfiguration>();
			}

			public TestHost()
				: this(new WindsorContainer(new XmlInterpreter()))
			{
				
			}

			public TestHost(string standaloneCastleConfiguration)
				:this(new WindsorContainer(new XmlInterpreter(standaloneCastleConfiguration)))
			{
				
			}
		}

		class TestWebBootstrapper : NeBootstrapper
		{
			
		}

        [Fact]
		public void prepare_contest_submit_solution_save_result()
		{
			var realProblemDirectory = @".\..\..\Tester\TestPrograms\RealProblem";

			var zip = File.ReadAllBytes(Path.Combine(realProblemDirectory, "tests2.zip"));
			var user = new User {DisplayName = "User"};
			var contest = new Contest {Beginning = new DateTime(1990, 7, 7), Ending = new DateTime(1990, 7, 7), Type = "Icpc"};
			var problem = new Problem
				{
					Contest = contest,
					Limits = new ResourceUsage
						{
							MemoryInBytes = int.MaxValue,
							TimeInMilliseconds = 100500
						},
					TestInfo = new TestInfo
						{
							Checker = new ProgramSource
								{
									LanguageId = "MSVC90Testlib",
									Code = File.ReadAllText(Path.Combine(realProblemDirectory, "check.cpp"))
								}
						}

				};

			var submission = new Submission
				{
					Author = user,
					Problem = problem,
					Source = new ProgramSource
						{
							Code = File.ReadAllText(Path.Combine(realProblemDirectory, "tree_ai.cpp")),
							LanguageId = "MSVC90"
						},
					SubmittedAt = contest.Beginning
				};

			var brokerHost = new RemoteAppDomainHost(typeof(BrokerBootstrapper));
			brokerHost.SetHostType(typeof(TestHost));
			brokerHost.Start();

			var testerHost = new RemoteAppDomainHost(typeof (TesterBootstrapper));
			testerHost.SetHostType(typeof (TestHost));
			testerHost.Start();

			var webHost = new TestHost(@".\..\..\Integration\WebEndpoint.config");
			webHost.Start<TestWebBootstrapper>();

			var bus = webHost.Container.Resolve<IServiceBus>();
			
			using (var factory = new TestDatabaseConfiguration().DatabaseConfiguration.BuildSessionFactory())
			{
				using(var scope = new SessionScope(factory))
				{
					var session = scope.Session;
					session.Save(user);
					session.Save(contest);
					session.Save(problem);
					session.Save(submission);
                }

				bus.Send(new UnpackTestInfo {ProblemId = problem.Id, ZipArchive = zip});
                bus.Send(new JudgeSubmission{SubmissionId = submission.Id});

				Thread.Sleep(100000);
                
				using(var scope = new SessionScope(factory))
				{
					submission = scope.Session.Get<Submission>(submission.Id);
					Assert.Equal(SubmissionTestingStatus.Finished, submission.TestingStatus);
					Assert.NotNull(submission.Result);
					Assert.Contains("Accepted", submission.Result.Verdict);
				}
			}
		}
	}
}