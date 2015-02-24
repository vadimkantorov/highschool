namespace Ne.Tester.Common
{
	public struct RunResult
	{
		public int TimeWorked;
		public int MemoryUsed;
		public int OutputSize;

		public RunStatus Status;
		public uint ExitCode;

		public RunResult(int timeWorked, int memoryUsed, int outputSize,
		                 RunStatus status, uint exitCode)
		{
			TimeWorked = timeWorked;
			MemoryUsed = memoryUsed;
			OutputSize = outputSize;
			Status = status;
			ExitCode = exitCode;
		}
	}
}