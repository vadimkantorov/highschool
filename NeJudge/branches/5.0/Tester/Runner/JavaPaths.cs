using System.IO;
using Microsoft.Win32;

namespace Tester.Runner
{
	public static class JavaPaths
	{
		public static string Java
		{
			get
			{
				return GetJavaExecutablePath("Java Runtime Environment", "java.exe");
			}
		}

		public static string Javac
		{
			get
			{
				return GetJavaExecutablePath("Java Development Kit", "javac.exe");
			}
		}

		private static string GetJavaExecutablePath(string component, string fileName)
		{
			string componentKey = @"SOFTWARE\JavaSoft\" + component;
			var currentVersion = Registry.LocalMachine.ThrowingGetValue<string>(
				componentKey,
				"CurrentVersion",
				"опеределить установленную версию " + component);
			var componentRootPath = Registry.LocalMachine.ThrowingGetValue<string>(
				componentKey + @"\" + currentVersion,
				"JavaHome",
				"получить путь к установленной версии" + component);
			return Path.Combine(componentRootPath, @"bin\" + fileName);
		}
	}
}