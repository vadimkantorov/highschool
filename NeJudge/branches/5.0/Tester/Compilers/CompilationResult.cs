namespace Tester.Compilers
{
	public class CompilationResult
	{
		public CompilationResult(bool success, string outputFileName, string compilerOutput)
		{
			Success = success;
			CompilerOutput = compilerOutput;
			OutputFileName = outputFileName;
		}

		public readonly bool Success;
		public readonly string CompilerOutput;
		public readonly string OutputFileName;
	}
}