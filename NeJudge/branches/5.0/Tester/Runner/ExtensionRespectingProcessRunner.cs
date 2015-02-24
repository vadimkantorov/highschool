using System;
using System.IO;

namespace Tester.Runner
{
	public class ExtensionRespectingProcessRunner : ProcessRunner
	{
		public ExtensionRespectingProcessRunner(string executablePath, string arguments, string workingDirectory)
			: base(RewriteExecutablePath(executablePath), RewriteArguments(executablePath, arguments), workingDirectory)
		{
		}

		private static string RewriteExecutablePath(string executablePath)
		{
			if (IsBatFile(executablePath))
				return Path.Combine(Environment.SystemDirectory, "cmd.exe");
			if (IsJarFile(executablePath) || IsClassFile(executablePath))
				return JavaPaths.Java;
			return executablePath;
		}

		private static string RewriteArguments(string executablePath, string arguments)
		{
			if (IsBatFile(executablePath))
				return string.Format("/c {0} {1}", executablePath, arguments);
			if (IsJarFile(executablePath))
				return string.Format("-jar {0} {1}", executablePath, arguments);
			if (IsClassFile(executablePath))
				return string.Format("-cp {0} {1}", Path.GetDirectoryName(executablePath), Path.GetFileNameWithoutExtension(executablePath));
			return arguments;
		}

		private static bool IsBatFile(string executablePath)
		{
			return Path.GetExtension(executablePath) == ".bat";
		}

		private static bool IsJarFile(string executablePath)
		{
			return Path.GetExtension(executablePath) == ".jar";
		}

		private static bool IsClassFile(string executablePath)
		{
			return Path.GetExtension(executablePath) == ".class";
		}
	}
}