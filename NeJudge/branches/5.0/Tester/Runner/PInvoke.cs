using System;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tester.Runner
{
	[Flags]
	enum JobObjectLimitFlags
	{
		LimitActiveProcess = 0x00000008,
		LimitJobMemory = 0x00000200,
		DieOnUnhandledException = 0x00000400,
	}

	struct JobObjectBasicLimitInformation
	{
		public long PerProcessUserTimeLimit;
		public long PerJobUserTimeLimit;
		public JobObjectLimitFlags LimitFlags;
		public UIntPtr MinimumWorkingSetSize;
		public UIntPtr MaximumWorkingSetSize;
		public int ActiveProcessLimit;
		public UIntPtr Affinity;
		public int PriorityClass;
		public int SchedulingClass;
	}

	[StructLayout(LayoutKind.Sequential)]
	class JobObjectExtendedLimitInformation
	{
		public JobObjectBasicLimitInformation BasicLimitInformation;
		public IoCounters IoInfo;
		public UIntPtr ProcessMemoryLimit;
		public UIntPtr JobMemoryLimit;
		public UIntPtr PeakProcessMemoryUsed;
		public UIntPtr PeakJobMemoryUsed;
	}

	struct JobObjectBasicUiRestrictions
	{
		public static JobObjectBasicUiRestrictions All()
		{
			return new JobObjectBasicUiRestrictions { UIRestrictionsClass = 0x000000FF };
		}

		public int UIRestrictionsClass;
	}

	struct JobObjectAssociateCompletionPort
	{
		public IntPtr CompletionKey;
		public IntPtr CompletionPort;
	}

	[StructLayout(LayoutKind.Sequential)]
	class JobObjectBasicAccountingInformation
	{
		public ulong TotalUserTime;
		public ulong TotalKernelTime;
		public ulong ThisPeriodTotalUserTime;
		public ulong ThisPeriodTotalKernelTime;
		public uint TotalPageFaultCount;
		public uint TotalProcesses;
		public uint ActiveProcesses;
		public uint TotalTerminatedProcesses;
	}

	enum JobObjectInfoClass
	{
		BasicAccountingInformation = 1,
		BasicLimitInformation = 2,
		BasicUIRestrictions = 4,
		AssociateCompletionPortInformation = 7,
		ExtendedLimitInformation = 9,
	}

	struct IoCounters
	{
		public ulong ReadOperationCount;
		public ulong WriteOperationCount;
		public ulong OtherOperationCount;
		public ulong ReadTransferCount;
		public ulong WriteTransferCount;
		public ulong OtherTransferCount;
	}

	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	struct StartupInfo
	{
		public int cb;
		public string lpReserved;
		public string lpDesktop;
		public string lpTitle;
		public int dwX;
		public int dwY;
		public int dwXSize;
		public int dwYSize;
		public int dwXCountChars;
		public int dwYCountChars;
		public int dwFillAttribute;
		public int dwFlags;
		public short wShowWindow;
		public short cbReserved2;
		public IntPtr lpReserved2;
		public IntPtr hStdInput;
		public IntPtr hStdOutput;
		public IntPtr hStdError;

		public const int UseShowWindow = 0x00000001;
		public const int SwHide = 0;
	}

	struct ProcessInformation
	{
		public IntPtr hProcess;
		public IntPtr hThread;
		public int dwProcessId;
		public int dwThreadId;
	}

	struct SecurityAttributes
	{
		public int nLength;
		public IntPtr lpSecurityDescriptor;
		public int bInheritHandle;
	}

	[Flags]
	enum ProcessCreationFlags
	{
		CreateSuspended = 0x00000004,
		CreateBreakawayFromJob = 0x01000000,
		CreateNoWindow = 0x08000000,
	}

	static class ErrorCodes
	{
		public const int WaitTimeout = 258;
	}

	enum JobObjectMessage
	{
		ActiveProcessLimit = 3,
		ActiveProcessZero = 4,
		AbnormalExitProcess = 8,
		JobMemoryLimit = 10,
	}

	static class Api
	{
		[DllImport(kernelLib, CharSet = CharSet.Auto, SetLastError = true)]
		public static extern SafeFileHandle CreateJobObject(IntPtr lpJobAttributes, IntPtr lpName);

		[DllImport(kernelLib, SetLastError = true)]
		public static extern bool SetInformationJobObject(
			SafeFileHandle hJob,
			JobObjectInfoClass infoClass,
			IntPtr info,
			int infoLength);

		[DllImport(kernelLib)]
		public static extern void QueryInformationJobObject(
			SafeFileHandle hJob,
			JobObjectInfoClass infoClass,
			IntPtr info,
			int infoLength,
			IntPtr unused);

		[DllImport(kernelLib, SetLastError = true)]
		public static extern SafeFileHandle CreateIoCompletionPort(
			IntPtr fileHandle,
			IntPtr existingCompletionPort,
			IntPtr completionKey,
			int numberOfConcurrentThreads);

		[DllImport(kernelLib, SetLastError = true, CharSet = CharSet.Auto)]
		public static extern bool CreateProcess(
			string lpApplicationName,
			string lpCommandLine,
			IntPtr lpProcessAttributes,
			IntPtr lpThreadAttributes,
			bool bInheritHandles,
			ProcessCreationFlags dwCreationFlags,
			IntPtr lpEnvironment,
			string lpCurrentDirectory,
			[In] ref StartupInfo lpStartupInfo,
			out ProcessInformation lpProcessInformation);

		[DllImport(kernelLib, SetLastError = true)]
		public static extern bool AssignProcessToJobObject(SafeFileHandle hJob, SafeFileHandle hProcess);

		[DllImport(kernelLib)]
		public static extern bool ResumeThread(SafeFileHandle hThread);

		[DllImport(kernelLib)]
		public static extern bool TerminateJobObject(SafeFileHandle hProcess, uint exitCode);

		[DllImport(kernelLib)]
		public static extern void GetExitCodeProcess(SafeFileHandle hProcess, out uint exitCode);

		[DllImport(kernelLib)]
		public static extern bool GetQueuedCompletionStatus(
			SafeFileHandle CompletionPort,
			out JobObjectMessage lpNumberOfBytes,
			out IntPtr lpCompletionKey,
			out IntPtr lpOverlapped,
			uint dwMilliseconds);

		[DllImport(kernelLib)]
		public static extern bool WaitForSingleObject(SafeFileHandle hProcess, uint timeout);

		private const string kernelLib = "kernel32.dll";
	}
}