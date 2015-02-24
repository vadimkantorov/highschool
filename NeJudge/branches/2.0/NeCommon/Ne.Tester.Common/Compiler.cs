using System.IO;

using Ne.Database.Classes;

namespace Ne.Tester
{
	public class Compiler
	{
		string source;
		string exe;
		string workDir;
		Language lang;

		string compReport = "";

		public string CompilationReport
		{
			get { return compReport; }
			set { compReport = value; }
		}

		public Compiler(string source, string exe, string workDir, string langID)
		{
			this.source = source;
			this.exe = exe;
			this.workDir = workDir;
			lang = Language.GetLanguage(langID);
		}

		public bool Compile()
		{
			string reportFile = Path.Combine(workDir, NeTesterConfiguration.REPORT_FILE);
			string script = Path.Combine(workDir, NeTesterConfiguration.COMPILE_SCRIPT);

			using ( StreamWriter sw = new StreamWriter(script) )
				sw.Write(lang.CompileScript);

			DfyzProc prc = new DfyzProc(NeTesterConfiguration.SHELL_COMMAND,
			                            workDir, null);

			prc.AddArgument(NeTesterConfiguration.SHELL_SCRIPT_PARAM);
			prc.AddArgument(script + " " + source + " " + exe);

			prc.SetCommonParams();
			
			prc.StdinRedirection = DfyzProc.NULL_DEVICE;
			prc.StdoutRedirection = reportFile;
			prc.DuplicateStdoutToStderr = true;

			RunResult rr = prc.Run();
			if ( rr.Status != RunStatus.Ok )
				throw new NeTesterException("Compilation failed");

			using ( StreamReader sr = new StreamReader(reportFile) )
				compReport = sr.ReadToEnd();

			return rr.ExitCode == 0;
		}
	}
}