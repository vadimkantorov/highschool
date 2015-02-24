using System;
using System.IO;
using System.Threading;
using Broker;
using Broker.Common;
using Broker.Common.Messages.Broker;
using Broker.Common.Messages.Tester;
using Model;
using Model.Testing;
using Rhino.ServiceBus;
using Xunit;

namespace Tester.Tests
{
	public class TesterRealProblemTests :
		BaseTesterTests,
		OccasionalConsumerOf<JobRequest>,
		OccasionalConsumerOf<TestInfoRequest>,
		OccasionalConsumerOf<SubmissionTested>,
		OccasionalConsumerOf<FailedToTestSubmission>
	{
		[Fact]
		public void tests_valid_cpp_solution()
		{
			PrepareSubmission("tree_ai.cpp", "MSVC90");
			TestSubmission();
			Assert.True(Success);
		}

		[Fact]
		public void tests_valid_java_submission()
		{
			PrepareSubmission("tree_am.java", "Java");
			TestSubmission();
			Assert.True(Success);
		}

		[Fact]
		public void tests_ml_submission()
		{
			PrepareSubmission("ml.cpp", "MSVC90");
			TestSubmission();
			Assert.True(Success);
		}

		[Fact]
		public void tests_slow1_submission()
		{
			PrepareSubmission("slow1.java", "Java");
			TestSubmission();
			Assert.True(Success);
		}

		[Fact]
		public void tests_slow2_submission()
		{
			PrepareSubmission("slow2.java", "Java");
			TestSubmission();
			Assert.True(Success);
		}

		private void TestSubmission()
		{
			using (bus.AddInstanceSubscription(this))
			{
				messageConsumed.WaitOne();
				receivedSubmissionResult.WaitOne();
			}
		}

		public void Consume(JobRequest message)
		{
			lock (submissionInfo)
			{
				if (!submissionSent)
				{
					bus.Send(new TestSubmission {SubmissionInfo = submissionInfo});
					messageConsumed.Set();
				}
				submissionSent = true;
			}
		}

		public void Consume(TestInfoRequest message)
		{
			bus.Send(new TestInfoEnvelope { TestInfo = testInfo });
		}

		public void Consume(SubmissionTested message)
		{
			Success = true;
			Console.WriteLine("SubmissionId: " + message.SubmissionId);
			Console.WriteLine(message.TestLog);
			receivedSubmissionResult.Set();
		}

		public void Consume(FailedToTestSubmission message)
		{
			Success = false;
			Console.WriteLine(message.Error);
			receivedSubmissionResult.Set();
		}

		public void PrepareSubmission(string solutionName, string languageId)
		{
			submissionInfo = new SubmissionInfo
				{
					RunAllTests = false,
					Source = new ProgramSource
						{
							Code = File.ReadAllText(Path.Combine(problemsDirectory, solutionName)),
							LanguageId = languageId,
						},
					SubmissionId = 42,
					TestInfoId = testInfoId,
					SubmissionLimits = new ResourceUsage
						{
							TimeInMilliseconds = 1000,
							MemoryInBytes = 256*1024*1024,
						},
					InputFileName = "input.txt",
					OutputFileName = "output.txt",
				};
			testInfo = new TestInfo
				{
					Id = testInfoId,
					Checker = new ProgramSource
						{
							LanguageId = "MSVC90Testlib",
							Code = File.ReadAllText(Path.Combine(problemsDirectory, "check.cpp")),
						},
					Tests = new TestsZipper("", "ans").UnzipTests(File.ReadAllBytes(Path.Combine(problemsDirectory, "tests.zip"))),
				};
		}

		public bool Success { get; private set; }

		private static readonly Guid testInfoId = Guid.NewGuid();
		private static readonly string problemsDirectory = Path.Combine(TestUtils.TestProgramsDirectory, "RealProblem");

		private readonly AutoResetEvent receivedSubmissionResult = new AutoResetEvent(false);
		private readonly AutoResetEvent messageConsumed = new AutoResetEvent(false);
		private SubmissionInfo submissionInfo;
		private TestInfo testInfo;
		private bool submissionSent;
	}
}