using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using Model;
using Model.Testing;

namespace Tester.Runner
{
	public class ProcessRunner : IProcessRunner
	{
		public ProcessRunner(string executablePath, string arguments, string workingDirectory)
		{
			this.executablePath = executablePath;
			this.arguments = arguments;
			this.workingDirectory = workingDirectory;
			
			ResourceLimits = new ResourceUsage();

			hJob = Api.CreateJobObject(IntPtr.Zero, IntPtr.Zero);
			hJob.EnsureIsValid("создать JobObject");
			SetJobInfo(
				JobObjectInfoClass.ExtendedLimitInformation,
				new JobObjectExtendedLimitInformation
					{
						BasicLimitInformation = new JobObjectBasicLimitInformation
							{
								LimitFlags = JobObjectLimitFlags.DieOnUnhandledException,
							},
					},
				"запретить показ стандартного диалога об ошибках");
			SetJobInfo(
				JobObjectInfoClass.BasicUIRestrictions,
				JobObjectBasicUiRestrictions.All(),
				"задать ограничения на UI");
			hCompletionPort = Api.CreateIoCompletionPort(SafeHandleExtensions.InvalidHandleValue, IntPtr.Zero, IntPtr.Zero, 0);
			hCompletionPort.EnsureIsValid("создать порт завершения ввода/вывода");
			SetJobInfo(
				JobObjectInfoClass.AssociateCompletionPortInformation,
				new JobObjectAssociateCompletionPort
					{
						CompletionPort = hCompletionPort.DangerousGetHandle(),
					},
				"ассоциировать порт завершения ввода/вывода с JobObject'ом");
		}

		public void Dispose()
		{
			hJob.Dispose();
			hCompletionPort.Dispose();
		}

		public RunResult Run()
		{
			SetMemoryLimit();
			SetActiveProcessLimit();
			using(var hProcess = StartProcess())
			{
				RunStatus exitStatus = GetExitStatus();
				Api.TerminateJobObject(hJob, 0);
				Api.WaitForSingleObject(hProcess, uint.MaxValue);
				uint exitCode;
				Api.GetExitCodeProcess(hProcess, out exitCode);
				var cpuTimeConsumedInMilliseconds = (long)(CpuTimeConsumedInTicks/TicksInMillisecond);
				return new RunResult
					{
						Status = exitStatus,
						ExitCode = exitCode,
						ResourceUsage = new ResourceUsage
							{
								TimeInMilliseconds = cpuTimeConsumedInMilliseconds,
								MemoryInBytes = (long)PeakMemoryUsed,
							}
					};
			}
		}

		private RunStatus GetExitStatus()
		{
			var stopwatch = Stopwatch.StartNew();
			long timeLimitInTicks = ResourceLimits.TimeInMilliseconds*TicksInMillisecond;
			ulong idleness = 0UL;
			ulong realTime = 0L;
			ulong cpuTimeConsumed = 0UL;
			while(true)
			{
				JobObjectMessage eventType;
				if(WaitForEvent(out eventType))
				{
					switch(eventType)
					{
						case JobObjectMessage.ActiveProcessLimit:
							return RunStatus.SecurityViolation;
						case JobObjectMessage.ActiveProcessZero:
							return RunStatus.Ok;
						case JobObjectMessage.AbnormalExitProcess:
							return RunStatus.RuntimeError;
						case JobObjectMessage.JobMemoryLimit:
							return RunStatus.MemoryLimitExceeded;
					}
				}
				else
				{
					ulong newCpuTimeConsumed = CpuTimeConsumedInTicks;
					var newRealTime = (ulong) stopwatch.ElapsedMilliseconds;
					if(newCpuTimeConsumed == cpuTimeConsumed)
						idleness += newRealTime - realTime;
					if((IdlenessLimit > 0) && (idleness > IdlenessLimit))
						return RunStatus.IdlenessLimitExceeded;
					if((timeLimitInTicks > 0) && (newCpuTimeConsumed > (ulong)timeLimitInTicks))
						return RunStatus.TimeLimitExceeded;
					realTime = newRealTime;
					cpuTimeConsumed = newCpuTimeConsumed;
				}
			}
		}

		public ResourceUsage ResourceLimits { get; set; }
		public ulong IdlenessLimit { get; set; }
		public bool DisallowChildProcesses { get; set; }

		private const long TicksInMillisecond = 10000;

		private bool WaitForEvent(out JobObjectMessage eventType)
		{
			IntPtr completionKey;
			IntPtr overlapped;
			return Api.GetQueuedCompletionStatus(hCompletionPort, out eventType, out completionKey, out overlapped, timeAtom);
		}

		private SafeFileHandle StartProcess()
		{
			ProcessInformation processInfo;
			var startupInfo = new StartupInfo
				{
					dwFlags = StartupInfo.UseShowWindow,
					wShowWindow = StartupInfo.SwHide,
				};
			if (!Api.CreateProcess(
				executablePath,
				"dummy " + arguments,
				IntPtr.Zero,
				IntPtr.Zero,
				false,
				ProcessCreationFlags.CreateNoWindow | ProcessCreationFlags.CreateSuspended | ProcessCreationFlags.CreateBreakawayFromJob,
				IntPtr.Zero,
				workingDirectory,
				ref startupInfo,
				out processInfo))
			{
				throw new Win32ExceptionWithContext("создать процесс");
			}
			var hProcess = new SafeFileHandle(processInfo.hProcess, true);
			var hThread = new SafeFileHandle(processInfo.hThread, true);
			if(!Api.AssignProcessToJobObject(hJob, hProcess))
				throw new Win32ExceptionWithContext("ассоциировать процесс с JobObject'ом");
			if(!Api.ResumeThread(hThread))
				throw new Win32ExceptionWithContext("запустить поток выполнения процесса");
			hThread.Dispose();
			return hProcess;
		}

		private void SetMemoryLimit()
		{
			if(ResourceLimits.MemoryInBytes == 0)
				return;
			SetJobInfo(
				JobObjectInfoClass.ExtendedLimitInformation,
				new JobObjectExtendedLimitInformation
					{
						BasicLimitInformation = new JobObjectBasicLimitInformation
							{
								LimitFlags = JobObjectLimitFlags.LimitJobMemory,
							},
						JobMemoryLimit = new UIntPtr((ulong)ResourceLimits.MemoryInBytes),
					},
				"задать ограничение на память");
		}

		private void SetActiveProcessLimit()
		{
			if(!DisallowChildProcesses)
				return;
			SetJobInfo(
				JobObjectInfoClass.BasicLimitInformation,
				new JobObjectBasicLimitInformation
					{
						LimitFlags = JobObjectLimitFlags.LimitActiveProcess,
						ActiveProcessLimit = 1,
					},
				"запретить создание дочерних процессов");
		}

		private void SetJobInfo(JobObjectInfoClass infoClass, object info, string errorMessage)
		{
			using(var pinnedInfo = new PinnedObject(info))
			{
				if(!Api.SetInformationJobObject(hJob, infoClass, pinnedInfo.Address, Marshal.SizeOf(info)))
					throw new Win32ExceptionWithContext(errorMessage);
			}
		}

		private void QueryJobInfo(JobObjectInfoClass infoClass, object info)
		{
			using(var pinnedInfo = new PinnedObject(info))
			{
				Api.QueryInformationJobObject(
					hJob,
					infoClass,
					pinnedInfo.Address,
					Marshal.SizeOf(info),
					IntPtr.Zero);
			}
		}

		private ulong CpuTimeConsumedInTicks
		{
			get
			{
				var stats = new JobObjectBasicAccountingInformation();
				QueryJobInfo(JobObjectInfoClass.BasicAccountingInformation, stats);
				return stats.TotalUserTime + stats.TotalKernelTime;
			}
		}

		private ulong PeakMemoryUsed
		{
			get
			{
				var stats = new JobObjectExtendedLimitInformation();
				QueryJobInfo(JobObjectInfoClass.ExtendedLimitInformation, stats);
				return stats.PeakJobMemoryUsed.ToUInt64();
			}
		}

		private const int timeAtom = 100;

		private readonly string executablePath;
		private readonly string arguments;
		private readonly string workingDirectory;

		private readonly SafeFileHandle hJob;
		private readonly SafeFileHandle hCompletionPort;
	}
}