using System;
using System.Globalization;
using System.IO;
using System.Linq;
using Microsoft.Win32;

namespace Tester.Compilers
{
	public class LatestMsvc : Msvc
	{
		public LatestMsvc() : base(GetLatestVersion())
		{
		}

		static string GetLatestVersion()
		{
			var vs = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\VisualStudio");

			var versions = vs.GetSubKeyNames().Select(TryParse).Where(x => x != null);
			var latestVersion = versions.Max().Value;

			return latestVersion.ToString("F1");
		}

		static double? TryParse(string keyName)
		{
			double d;
			if (double.TryParse(keyName, NumberStyles.Float, CultureInfo.InvariantCulture, out d))
				return d;

			return null;
		}
	}
	
	public class Msvc : CompilerBase
	{
		public Msvc(string version)
		{
			this.version = version;
			string vsToolsEnvVar = string.Format("VS{0}COMNTOOLS", version.Replace(".", ""));
			string vsPath = Environment.GetEnvironmentVariable(vsToolsEnvVar);
			if (vsPath == null)
			{
				throw new FailedActionException(
					"получить путь к установленной Visual Studio " + version,
					"Не найдена переменная окружения " + vsToolsEnvVar);
			}
			vcVarsPath = Path.Combine(vsPath, @"..\..\VC\vcvarsall.bat");
		}

		protected override string SourceFileName { get { return "sln.cpp"; } }

		protected override string OutputFileName { get { return "sln.exe"; } }

		protected override string CompilerExecutable { get { return "cl.exe"; } }

		protected override string CompilerArguments { get { return "/O2 /EHsc"; } }

		public override bool ShowToContestants
		{
			get { return true; }
		}

		protected override string[] BeforeCompilationCommands 
		{
			get { return new[] {string.Format("call \"{0}\"", vcVarsPath)}; }
		}

		public override string Name { get { return "Microsoft Visual C++ " + version; } }

		readonly string vcVarsPath;
		readonly string version;
	}
}