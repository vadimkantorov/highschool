using System;
using System.Configuration;
using System.IO;

namespace Ne.Tester
{
	public abstract class Tester
	{
		public static string FetchSolution(Submission s, TestingDirectoryInfo tdInfo)
		{
			s.LoadSource();
			string slnFile = Path.Combine(tdInfo.RootDir,
				String.Format(NeTesterConfiguration.SOLUTION_FILE_PATTERN, Language.GetLanguage(s.LanguageID).Extension));
			using ( StreamWriter sw = new StreamWriter(slnFile) )
				sw.Write(s.Source);
			return slnFile;
		}

		public static bool CompileSolution(out string compReport, string slnExe, string slnSource,
			string languageID, TestingDirectoryInfo tdInfo)
		{
			Compiler comp = new Compiler(slnSource, slnExe, tdInfo.RootDir, languageID);
			bool ret = comp.Compile();
			compReport = comp.CompilationReport;
			return ret;
		}

		public static void FetchTestData(Test t, string[] filenames)
		{
			t.LoadInput();
			t.LoadOutput();
			using ( BinaryWriter sw = new BinaryWriter(
				new FileStream(filenames[0], FileMode.CreateNew)
			) )
				sw.Write(t.Input);
			using ( BinaryWriter sw = new BinaryWriter(
				new FileStream(filenames[1], FileMode.CreateNew)
			) )
				sw.Write(t.Output);
		}

		public static string FetchChecker(Problem prob, TestingDirectoryInfo tdInfo)
		{
			prob.LoadCheckerBytes();
			string ret = Path.Combine(tdInfo.CheckerDir, NeTesterConfiguration.CHECKER_FILE);
			using ( BinaryWriter sw = new BinaryWriter(
				new FileStream(ret, FileMode.CreateNew)
			) )
				sw.Write(prob.CheckerBytes);
			return ret;
		}

		public abstract void EnqueueSubmission(Submission submission);
	}
}