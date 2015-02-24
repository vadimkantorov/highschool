using System.IO;

namespace Tester.Tests
{
	public class TestUtils
	{
		public static string TestProgramsDirectory
		{
			get
			{
				string projectDirectory = Directory.GetCurrentDirectory();
				while(Directory.GetFiles(projectDirectory, "*.csproj").Length == 0)
					projectDirectory = Directory.GetParent(projectDirectory).FullName;
				return Path.Combine(projectDirectory, "Tester", "TestPrograms");
			}
		}

		public static string ExeDirectory { get { return Path.Combine(TestProgramsDirectory, "Executables"); } }
		public static string CheckersDirectory { get { return Path.Combine(TestProgramsDirectory, "Checkers"); } }

		public static void WriteToExeDirectory(string fileName, string input)
		{
			File.WriteAllText(Path.Combine(ExeDirectory, fileName), input);
		}

		public static string ReadFromExeDirectory(string fileName)
		{
			return File.ReadAllText(Path.Combine(ExeDirectory, fileName)).Trim();
		}

		public static void ClearExeDirectory()
		{
			if(Directory.Exists(ExeDirectory))
				Directory.Delete(ExeDirectory, true);
			Directory.CreateDirectory(ExeDirectory);
		}
	}
}