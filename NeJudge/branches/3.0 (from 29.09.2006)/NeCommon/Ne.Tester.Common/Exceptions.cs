using System;

namespace Ne.Tester
{
	public class NeTesterException : ApplicationException
	{
		public NeTesterException(string message)
			: base(message)
		{}
	}

	public class NeDfyzProcException : NeTesterException
	{
		public NeDfyzProcException(string message)
			: base(message)
		{}
	}

	public class NeConfigurationException : NeTesterException
	{
		public NeConfigurationException(string message)
			: base(message)
		{}
	}
}