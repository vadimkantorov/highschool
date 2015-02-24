namespace Ne.Database.New
{
	public struct ContestRegistration
	{
		bool AllowParticipate;
		bool AllowJudge;
		bool IsBanned;
		bool IsInvisible;

		public ContestRegistration(bool allowParticipate, bool allowJudge, bool isBanned, bool isInvisible)
		{
			AllowParticipate = allowParticipate;
			AllowJudge = allowJudge;
			IsBanned = isBanned;
			IsInvisible = isInvisible;
		}
	}
}