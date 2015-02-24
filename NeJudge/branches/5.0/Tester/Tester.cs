using System.Collections.Generic;
using System.IO;
using Broker.Common;
using Model;
using Model.Factories;
using Model.Testing;
using Tester.Checker;
using Tester.Compilers;
using Tester.Runner;

namespace Tester
{
	internal class Tester
	{
		public Tester(IFactory<ICompiler> compilers, SubmissionInfo submissionInfo, TestInfo testInfo)
		{
			this.submissionInfo = submissionInfo;
			this.testInfo = testInfo;

			CompilationResult checkerCompilationResult = CompilerExtensions.CompileToTempDirectory(testInfo.Checker, compilers);
			if (!checkerCompilationResult.Success)
			{
				throw new FailedActionException(
					"скомпилировать чекер",
					"Лог компиляции: " + checkerCompilationResult.CompilerOutput);
			}
			checkerPath = checkerCompilationResult.OutputFileName;
			checkerDirectory = Path.GetDirectoryName(checkerPath);
			CompilationResult sourceCompilationResult = CompilerExtensions.CompileToTempDirectory(submissionInfo.Source, compilers);
			result = new TestLog {CompilationReport = sourceCompilationResult.CompilerOutput};
			compilationSucceeded = sourceCompilationResult.Success;
			solutionPath = sourceCompilationResult.OutputFileName;
			solutionDirectory = Path.GetDirectoryName(solutionPath);
		}

		public TestLog Test()
		{
			if (!compilationSucceeded)
				return result;
			result.CheckResults = new List<TestRunInfo>();
			foreach (var test in testInfo.Tests)
			{
				TestRunInfo testRunInfo = RunOneTest(test);
				result.CheckResults.Add(testRunInfo);
				if (!submissionInfo.RunAllTests && ShouldBreak(testRunInfo))
					break;
			}
			return result;
		}

		private TestRunInfo RunOneTest(Test test)
		{
			string inputFile = Path.Combine(solutionDirectory, submissionInfo.InputFileName);
			File.WriteAllBytes(inputFile, test.Input.Bytes);
			RunResult runResult = Run.Solution(solutionPath, submissionInfo.SubmissionLimits);
			CheckResult checkResult = RunChecker(test, runResult);
			return new TestRunInfo { RunResult = runResult, CheckResult = checkResult };
		}

		private CheckResult RunChecker(Test test, RunResult runResult)
		{
			if (runResult.Status != RunStatus.Ok)
				return new CheckResult { CheckStatus = CheckStatus.NotChecked };
			string inputFile = Path.Combine(checkerDirectory, "input.txt");
			string outputFile = Path.Combine(solutionDirectory, submissionInfo.OutputFileName);
			string answerFile = Path.Combine(checkerDirectory, "answer.txt");
			File.WriteAllBytes(inputFile, test.Input.Bytes);
			File.WriteAllBytes(answerFile, test.Output.Bytes);
			return new TestlibChecker(checkerPath).Check(inputFile, outputFile, answerFile);
		}



		private static bool ShouldBreak(TestRunInfo testRunInfo)
		{
			return (testRunInfo.RunResult.Status != RunStatus.Ok) || (testRunInfo.CheckResult.CheckStatus != CheckStatus.Ok);
		}

		private readonly SubmissionInfo submissionInfo;
		private readonly TestInfo testInfo;
		private readonly TestLog result;
		private readonly string checkerPath;
		private readonly string checkerDirectory;
		private readonly string solutionPath;
		private readonly string solutionDirectory;
		private readonly bool compilationSucceeded;
	}
}