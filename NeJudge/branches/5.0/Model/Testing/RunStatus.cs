namespace Model.Testing
{
	public enum RunStatus
	{
		Ok,
		TimeLimitExceeded,
		MemoryLimitExceeded,
		IdlenessLimitExceeded,
		SecurityViolation,
		RuntimeError,
	}
}