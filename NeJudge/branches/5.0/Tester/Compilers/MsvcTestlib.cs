using System;
using System.IO;

namespace Tester.Compilers
{
	public class MsvcTestlib : ICompiler
	{
		public CompilationResult Compile(string sourcePath, string outputDirectory)
		{
			string testlibHeaderFile = Path.Combine(outputDirectory, TestlibHeader);
			File.WriteAllBytes(testlibHeaderFile, ReadTestlibHeader());
			return msvc.Compile(sourcePath, outputDirectory);
		}

		public MsvcTestlib(Msvc msvc)
		{
			this.msvc = msvc;
		}

		byte[] ReadTestlibHeader()
		{
			var resourceType = GetType();
			string resourceName = string.Format("{0}.{1}", resourceType.Namespace, TestlibHeader);
			using (var resourceStream = resourceType.Assembly.GetManifestResourceStream(resourceName))
			{
				var result = new byte[resourceStream.Length];
				resourceStream.Read(result, 0, (int) resourceStream.Length);
				return result;
			}
		}

		public string Name
		{
			get { return msvc + " (Testlib)"; }
		}

		public bool ShowToContestants
		{
			get { return false; }
		}

		readonly Msvc msvc;
		const string TestlibHeader = "testlib.h";
	}
}