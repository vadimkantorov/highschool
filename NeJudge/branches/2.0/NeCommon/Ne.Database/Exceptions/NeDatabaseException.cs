using System;

namespace Ne.Database.Classes
{
	public class NeDatabaseException : Exception
	{
		public NeDatabaseException(string message)
			: base(message)
		{}
	}
}