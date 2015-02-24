using System;
using System.IO;

using Ne.Tester.Common;

namespace Ne.Tester
{
	public class Compiler
	{
		string source;
		string exe;
		string workDir;
		string lang;

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
			lang = langID;
		}

		public bool Compile()
		{
			string reportFile = Path.Combine(workDir, NeTesterConfiguration.ReportFile);
			string script = Path.Combine(workDir, NeTesterConfiguration.CompileScript);


			using( StreamWriter sw = new StreamWriter(script) )
			{
				throw new NotImplementedException();
				//sw.Write(lang.CompileScript);
			}

			RunResult rr = new RunResult();
			using( DfyzProc prc = new DfyzProc(NeTesterConfiguration.ShellCommand, workDir, null) )
			{
				prc.AddArgument(NeTesterConfiguration.ShellScriptParam);
				prc.AddArgument(script + " " + source + " " + exe);

				prc.SetCommonParams();

				prc.StdinRedirection = DfyzProc.NULL_DEVICE;
				prc.StdoutRedirection = reportFile;
				prc.DuplicateStdoutToStderr = true;

				rr = prc.Run();
			}
			if ( rr.Status != RunStatus.Ok )
				throw new NeTesterException("Compilation failed");

			using ( StreamReader sr = new StreamReader(reportFile) )
				compReport = sr.ReadToEnd();

			return rr.ExitCode == 0;
		}
	}
}