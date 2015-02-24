namespace Ne.Database.Classes
{
	public struct ContestRegistration
	{
		public ContestRights Rights;
		public bool IsInvisible;

		public ContestRegistration(ContestRights rights, bool isInvisible)
		{
			Rights = rights;
			IsInvisible = isInvisible;
		}
	}
}