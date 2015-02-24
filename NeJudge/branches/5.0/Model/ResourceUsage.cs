namespace Model
{
	public class ResourceUsage
	{
		public long TimeInMilliseconds { get; set; }
		public long MemoryInBytes { get; set; }

		public double TimeInSeconds
		{
			get { return TimeInMilliseconds / (double)Second; }
			set { TimeInMilliseconds = (long) (value*Second); }
		}

		public int MemoryInMegabytes
		{
			get { return (int) (MemoryInBytes/Megabyte); }
			set { MemoryInBytes = value*Megabyte; }
		}
		
		public ResourceUsage()
		{
			TimeInSeconds = 2.0;
			MemoryInMegabytes = 65;
		}

		public override string ToString()
		{
			return string.Format("Time = {0}s, Memory = {1}Mb", TimeInSeconds, MemoryInMegabytes);
		}

		public const int Megabyte = 1 << 20;
		public const int Second = 1000;
	}
}