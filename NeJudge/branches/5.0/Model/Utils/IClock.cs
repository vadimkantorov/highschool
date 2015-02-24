using System;

namespace Model
{
	public interface IClock
	{
		DateTime CurrentTime { get; }
	}

	public class SystemClock : IClock
	{
		public DateTime CurrentTime
		{
			get { return DateTime.Now; }
		}
	}
}