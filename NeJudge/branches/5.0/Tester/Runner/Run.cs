using System;
using System.IO;
using Model;
using Model.Testing;

namespace Tester.Runner
{
	public static class Run
	{
		public static RunResult WithNoLimits(string executablePath, string arguments, string workingDirectory)
		{
			return RunWithFiller(executablePath, arguments, workingDirectory, runner => {});
		}
		
		public static RunResult WithDefaultLimits(string executablePath, string arguments, string workingDirectory)
		{
			return RunWithFiller(executablePath, arguments, workingDirectory, runner =>
				{
					runner.ResourceLimits = new ResourceUsage { TimeInMilliseconds = defaultTimeLimit, MemoryInBytes = 256*1024*1024 };
					runner.IdlenessLimit = defaultTimeLimit;
				});
		}

		public static RunResult Solution(string executablePath, ResourceUsage resourceLimits)
		{
			return RunWithFiller(executablePath, "", Path.GetDirectoryName(executablePath), runner =>
				{
					runner.DisallowChildProcesses = true;
					runner.ResourceLimits = resourceLimits;
					runner.IdlenessLimit = defaultTimeLimit;
				});
		}

		private static RunResult RunWithFiller(string executablePath, string arguments, string workingDirectory, Action<IProcessRunner> filler)
		{
			using(var runner = new ExtensionRespectingProcessRunner(executablePath, arguments, workingDirectory))
			{
				filler(runner);
				return runner.Run();
			}
		}

		private const int defaultTimeLimit = 10000;
	}
}