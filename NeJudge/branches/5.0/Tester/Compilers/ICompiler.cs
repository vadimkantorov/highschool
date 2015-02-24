using Model.Factories;

namespace Tester.Compilers
{
	public interface ICompiler : INamed
	{
		CompilationResult Compile(string sourcePath, string outputDirectory);
		bool ShowToContestants { get; }
	}
}