using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

using Ne.Database.Classes;

namespace Ne.Tester
{
	public class DfyzProc : IDisposable
	{
		#region Unmanaged code wrappers

		private struct DfLimits
		{
			public int time;
			public int memory;
			public int output;
			public int idleness;
		}

		private struct DfRedirections
		{
			public string std_in;
			public string std_out;
			public string std_err;
			public int dup_out_to_err;
		}

		private struct DfRunAsParams
		{
			public string username;
			public string password;
		}

		private struct DfProcess
		{
			public string exe_name;
			public string work_dir;
			public IntPtr args;

			public DfLimits limits;
			public DfRedirections redirs;
			public DfRunAsParams run_as_params;
			public int allow_process_creation;
		}

		private const string DLL_NAME = @"D:\Projects\C#\notFinished\NeJudge\branches\2.0\debug\DfyzProc.dll";

		[DllImport(DLL_NAME)]
		private static extern IntPtr df_new(string exe_name, string work_dir);

		[DllImport(DLL_NAME)]
		private static extern void df_add_arg(ref DfProcess prc, string arg);

		[DllImport(DLL_NAME)]
		private static extern void df_free(ref DfProcess prc);

		[DllImport(DLL_NAME)]
		private static extern void df_run(ref DfProcess prc, ref RunResult res, StringBuilder comment);

		#endregion

		#region Fields

		private const int COMMENT_LEN = 512;
		private string _comment;

		private DfProcess _prc;

		#endregion

		#region Properties

		public const string NULL_DEVICE = @"NUL";

		public string ExeName
		{
			get { return _prc.exe_name; }
			set { _prc.exe_name = value; }
		}

		public string WorkDir
		{
			get { return _prc.work_dir; }
			set { _prc.work_dir = value; }
		}

		public int TimeLimit
		{
			get { return _prc.limits.time; }
			set
			{
				if ( value < 0 )
					throw new ArgumentException("Time limit must be non-negative");
				_prc.limits.time = value;
			}
		}

		public int MemoryLimit
		{
			get { return _prc.limits.memory; }
			set
			{
				if ( value < 0 )
					throw new ArgumentException("Memory limit must be non-negative");
				_prc.limits.memory = value;
			}
		}

		public int OutputLimit
		{
			get { return _prc.limits.output; }
			set
			{
				if ( value < 0 )
					throw new ArgumentException("Output limit must be non-negative");
				_prc.limits.output = value;
			}
		}

		public int IdlenessLimit
		{
			get { return _prc.limits.idleness; }
			set
			{
				if ( value < 0 )
					throw new ArgumentException("Idleness limit must be non-negative");
				_prc.limits.idleness = value;
			}
		}

		public bool DuplicateStdoutToStderr
		{
			get { return _prc.redirs.dup_out_to_err != 0; }
			set { _prc.redirs.dup_out_to_err = value ? 1 : 0; }
		}

		public string StdinRedirection
		{
			get { return _prc.redirs.std_in; }
			set { _prc.redirs.std_in = value; }
		}

		public string StdoutRedirection
		{
			get { return _prc.redirs.std_out; }
			set { _prc.redirs.std_out = value; }
		}

		public string StderrRedirection
		{
			get { return _prc.redirs.std_err; }
			set { _prc.redirs.std_err = value; }
		}

		public string Username
		{
			get { return _prc.run_as_params.username; }
			set { _prc.run_as_params.username = value; }
		}

		public string Password
		{
			get { return _prc.run_as_params.password; }
			set { _prc.run_as_params.password = value; }
		}

		public string Comment
		{
			get { return _comment; }
		}

		public bool AllowProcessCreation
		{
			get { return _prc.allow_process_creation != 0; }
			set { _prc.allow_process_creation = value ? 1 : 0; }
		}

		#endregion

		public void SetCommonParams()
		{
			TimeLimit = IdlenessLimit = 10000;
			MemoryLimit = 64*1024*1024;
			AllowProcessCreation = true;
		}

		public RunResult Run()
		{
			RunResult rr = new RunResult();
			StringBuilder comment = new StringBuilder(COMMENT_LEN);
			df_run(ref _prc, ref rr, comment);
			_comment = comment.ToString();
			return rr;
		}

		public DfyzProc(string exeName, string workDir,
		                List<string> args)
		{
			IntPtr ptr = df_new(exeName, workDir);
			_prc = (DfProcess) Marshal.PtrToStructure(ptr, typeof (DfProcess));
			if ( args != null )
				foreach ( string s in args )
					df_add_arg(ref _prc, s);
		}

		public void AddArgument(string arg)
		{
			df_add_arg(ref _prc, arg);
		}

		public void Dispose()
		{
			df_free(ref _prc);
		}
	}
}