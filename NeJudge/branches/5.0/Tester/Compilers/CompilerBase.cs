using System.IO;
using Model.Testing;
using Tester.Runner;

namespace Tester.Compilers
{
	public abstract class CompilerBase : ICompiler
	{
		public CompilationResult Compile(string sourcePath, string outputDirectory)
		{
			string renamedSourcePath = Path.Combine(outputDirectory, SourceFileName);
			File.Copy(sourcePath, renamedSourcePath);
			string tmpBatFile = Path.Combine(outputDirectory, "compiler.bat");
			string compilerOutputFile = Path.Combine(outputDirectory, "log.txt");
			File.WriteAllLines(tmpBatFile, BeforeCompilationCommands);
			File.AppendAllText(
				tmpBatFile,
				string.Format("{0} {1} {2} 1>{3} 2>&1",
				CompilerExecutable.Quote(),
				CompilerArguments,
				renamedSourcePath.Quote(),
				compilerOutputFile.Quote()));
			var compilerRunResult = Run.WithDefaultLimits(tmpBatFile, "", outputDirectory);
			if (compilerRunResult.Status != RunStatus.Ok)
			{
				throw new FailedActionException(
					string.Format("скомпилировать файл {0}", renamedSourcePath),
					string.Format("Запуск компилятора завершился со статусом {0}", compilerRunResult.Status));
			}
			return new CompilationResult(
				compilerRunResult.ExitCode == 0,
				Path.Combine(outputDirectory, outputDirectory, OutputFileName),
				File.ReadAllText(compilerOutputFile));
		}

		protected abstract string SourceFileName { get; }
		protected abstract string OutputFileName { get; }
		protected abstract string CompilerExecutable { get; }
		protected abstract string CompilerArguments { get; }
		public abstract string Name { get; }
		public abstract bool ShowToContestants { get; }

		protected virtual string[] BeforeCompilationCommands
		{
			get { return new string[0]; }
		}
	}
}