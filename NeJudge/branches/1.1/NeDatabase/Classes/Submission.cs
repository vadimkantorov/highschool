using System;

namespace Ne.Database.Classes
{
	// Permorfs conversions from one outcome representation to another
	public class OutcomeConverter
	{
		private static string[] _dbConvTable 
			= new string[] { "AC", "WA", "PE", "TLE", "MLE", "OLE", "RE", "SV", "CE", "FA", "WAI", "CO", "RU" };
		private static string[] _rfConvTable
			= new string[] { "Accepted", "Wrong answer", "Presentation error", "Time limit exceeded",
							   "Memory limit exceeded", "Output limit exceeded", "Runtime error",
							   "Security violation", "Compilation error", "Testing failure", "Waiting", 
							   "Compiling", "Runnning" };
		// Get the short string to store in DB
		public static string ConvertToDBString(Outcome enumValue)
		{
			return _dbConvTable[(int)enumValue];
		}

		// Get enum value from DB string
		public static Outcome ConvertToEnumValue(string stringValue)
		{
			for ( int i = 0; i < _dbConvTable.Length; i++ )
				if ( _dbConvTable[i] == stringValue )
					return (Outcome)i;
			// FIXME: throw NeDatabaseException
			throw new Exception();
		}

		// Get the long string to show to user
		public static string ToReadableForm(Outcome enumValue)
		{
			return _rfConvTable[(int)enumValue];
		}
	}

	public enum Outcome
	{
		// The only good status ;)
		Accepted = 0,
		// Statuses connected with incorrect solution output
		WrongAnswer = 1,
		PresentationError = 2,
		// Statuses connected with incorrect solution runtime behaviour
		TimeLimitExceeded = 3,
		MemoryLimitExceeded = 4,
		OutputLimitExceeded = 5,
		RuntimeError = 6,
		SecurityViolation = 7,
		// Solution wasn't tested at all due to errors
		CompilationError = 8,
		TestingFailure = 9,
		// Still testing
		Waiting = 10,
		Compiling = 11,
		Runnning = 12
	}

	public class Submission
	{
		internal int _id = -1;
		private int _contestID;
		private int _problemID;
		private string _userID;
		private string _languageID;
		private DateTime SubmissionTime;
		
		public Submission()
		{}
	}
}
