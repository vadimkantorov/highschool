namespace Ne.Database.Classes
{
	public enum CheckStatus
	{
		Ok,
		WrongAnswer,
		PresentationError,
		NotChecked
	}

	public enum RunStatus
	{
		Ok = 0,
		TimeLimit = 1,
		MemoryLimit = 2,
		OutputLimit = 3,
		SecurityViolation = 4,
		RuntimeError = 5,
		Failure = 6
	}
}