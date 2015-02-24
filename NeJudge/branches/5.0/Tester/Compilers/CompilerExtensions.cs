using System;
using System.IO;
using Model;
using Model.Factories;

namespace Tester.Compilers
{
	public static class CompilerExtensions
	{
		public static CompilationResult CompileToTempDirectory(ProgramSource source, IFactory<ICompiler> compilers)
		{
			var tempDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
			Directory.CreateDirectory(tempDirectory);
			
			var sourcePath = Path.GetTempFileName();
			File.WriteAllText(sourcePath, source.Code);

			return compilers.Find(source.LanguageId).Compile(sourcePath, tempDirectory);
		}
	}
}