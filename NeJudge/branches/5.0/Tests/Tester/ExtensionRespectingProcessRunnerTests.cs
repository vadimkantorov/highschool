using System;
using System.CodeDom.Compiler;
using System.IO;
using Model;
using Model.Testing;
using Tester.Runner;
using Xunit;

namespace Tester.Tests
{
	public class ExtensionRespectingProcessRunnerTests
	{
		public ExtensionRespectingProcessRunnerTests()
		{
			TestUtils.ClearExeDirectory();
		}

		[Fact]
		public void can_run_gcd_program()
		{
			TestUtils.WriteToExeDirectory("input.txt", "13448 48952");
			var runInfo = CompileAndRun("Gcd", "", runner => {});
			Assert.Equal(RunStatus.Ok, runInfo.Status);
			Assert.Equal("8", TestUtils.ReadFromExeDirectory("output.txt"));
		}

		[Fact]
		public void terminates_program_with_time_limit()
		{
			TestUtils.WriteToExeDirectory("input.txt", "45");
			RunResult runInfo = CompileAndRun("SlowFib", "", runner => runner.ResourceLimits = new ResourceUsage {TimeInMilliseconds = 2000});
			Assert.Equal(RunStatus.TimeLimitExceeded, runInfo.Status);
		}

		[Fact]
		public void terminates_program_with_memory_limit()
		{
			TestUtils.WriteToExeDirectory("input.txt", "10000000");
			RunResult runInfo = CompileAndRun(
				"EatMemory",
				"",
				runner => runner.ResourceLimits = new ResourceUsage {MemoryInBytes = 64*1024*1024});
			Assert.Equal(RunStatus.MemoryLimitExceeded, runInfo.Status);
		}

		[Fact]
		public void runs_process_with_specified_arguments()
		{
			RunResult runInfo = CompileAndRun("SaveArgs", "one two three", runner => {});
			Assert.Equal(RunStatus.Ok, runInfo.Status);
			Assert.Equal("one two three", TestUtils.ReadFromExeDirectory("output.txt"));
		}

		[Fact]
		public void terminates_idle_program()
		{
			RunResult runInfo = CompileAndRun("Idle", "", runner =>
				{
					runner.IdlenessLimit = 2000;
					runner.ResourceLimits = new ResourceUsage {TimeInMilliseconds = 2000};
				});
			Assert.Equal(RunStatus.IdlenessLimitExceeded, runInfo.Status);
			Assert.True(runInfo.ResourceUsage.TimeInMilliseconds < 2000);
		}

		[Fact]
		public void can_run_bat_files()
		{
			RunResult runInfo = Run("Batch.bat", "", runner => {});
			Assert.Equal(RunStatus.Ok, runInfo.Status);
			Assert.Equal(42U, runInfo.ExitCode);
			Assert.Equal("I'm .Bat-Man!", TestUtils.ReadFromExeDirectory("output.txt"));
		}

		[Fact]
		public void can_run_jar_files()
		{
			TestUtils.WriteToExeDirectory("input.txt", "39");
			RunResult runInfo = Run(@"Java\Main.jar", "", runner => {});
			Assert.Equal(RunStatus.Ok, runInfo.Status);
			Assert.Equal("42", TestUtils.ReadFromExeDirectory("output.txt"));
		}

		[Fact]
		public void can_run_class_files()
		{
			TestUtils.WriteToExeDirectory("input.txt", "39");
			RunResult runInfo = Run(@"Java\Main.jar", "", runner => {});
			Assert.Equal(RunStatus.Ok, runInfo.Status);
			Assert.Equal("42", TestUtils.ReadFromExeDirectory("output.txt"));
		}

		[Fact]
		public void can_disallow_child_processes()
		{
			RunResult runInfo = Run("Spawn.exe", "", runner => runner.DisallowChildProcesses = true);
			Assert.Equal(RunStatus.SecurityViolation, runInfo.Status);
		}

		[Fact]
		public void can_run_program_allocating_1gb()
		{
			Run("AllocGB.exe", "", runner => {});
		}

		private static RunResult Run(string executableName, string arguments, Action<ExtensionRespectingProcessRunner> runnerPreparer)
		{
			var runner = new ExtensionRespectingProcessRunner(
				Path.Combine(TestUtils.TestProgramsDirectory, executableName),
				arguments,
				TestUtils.ExeDirectory);
			runnerPreparer(runner);
			RunResult runInfo = runner.Run();
			Console.WriteLine(runInfo);
			return runInfo;
		}

		private static RunResult CompileAndRun(string sourceName, string arguments, Action<ExtensionRespectingProcessRunner> runnerPreparer)
		{
			return Run(Compile(sourceName), arguments, runnerPreparer);
		}

		private static string Compile(string sourceName)
		{
			CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
			string exePath = Path.Combine(TestUtils.ExeDirectory, sourceName + ".exe");
			var compileParameters = new CompilerParameters
				{
					GenerateExecutable = true,
					OutputAssembly = exePath,
				};
			CompilerResults compilationResult = provider.CompileAssemblyFromFile(
				compileParameters,
				Path.Combine(TestUtils.TestProgramsDirectory, sourceName + ".cs"));
			if(compilationResult.Errors.Count > 0)
			{
				foreach(var error in compilationResult.Errors)
					Console.Error.WriteLine(error);
				throw new Exception(string.Format("Не получилось скомпилировать {0}", sourceName));
			}
			return exePath;
		}
	}
}