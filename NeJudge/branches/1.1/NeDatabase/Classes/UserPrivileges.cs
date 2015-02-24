using System;

namespace Ne.Database.New
{
	[Flags]
	public enum UserPrivileges
	{
		BypassParticipatingCheck = 1,
		BypassJudgingCheck = 2,
		EditProblems = 4,
		GrantPrivileges = 8
	}
}