using System;
using System.IO;
using Model;
using Model.Testing;
using Tester.Runner;

namespace Tester.Checker
{
	public class TestlibChecker : IChecker
	{
		public TestlibChecker(string checkerFile)
		{
			this.checkerFile = checkerFile;
			resultFile = Path.Combine(Path.GetDirectoryName(checkerFile), Guid.NewGuid() + ".txt");
		}

		public CheckResult Check(string inputFile, string outputFile, string answerFile)
		{
			string checkerArguments = string.Format(
				"{0} {1} {2} {3}",
				inputFile.Quote(),
				outputFile.Quote(),
				answerFile.Quote(),
				resultFile.Quote());
			RunResult runInfo = Run.WithDefaultLimits(checkerFile, checkerArguments, Path.GetDirectoryName(checkerFile));
			if (runInfo.Status != RunStatus.Ok)
			{
				throw new FailedActionException(
					string.Format("получить результаты проверки от чекера {0}", checkerFile),
					string.Format("Запуск чекера завершился со статусом {0}, код возврата {1}", runInfo.Status, runInfo.ExitCode));
			}
			return new CheckResult
				{
					CheckerComment = File.ReadAllText(resultFile),
					CheckStatus = MapExitCodeToCheckStatus(runInfo.ExitCode),
				};
		}

		private CheckStatus MapExitCodeToCheckStatus(uint exitCode)
		{
			switch (exitCode)
			{
				case 0:
					return CheckStatus.Ok;
				case 1:
					return CheckStatus.WrongAnswer;
				case 2:
					return CheckStatus.PresentationError;
				default:
					throw new FailedActionException(
						string.Format("интерпретировать код возврата чекера {0} как результат проверки", checkerFile),
						string.Format("Код возврата: {0}", exitCode));
			}
		}

		private readonly string checkerFile;
		private readonly string resultFile;
	}
}