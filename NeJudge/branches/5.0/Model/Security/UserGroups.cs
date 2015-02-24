using System;

namespace Model
{
	public static class UserGroups
	{
		public const string Everyone = "Everyone";

		public const string SuperAdmins = "SuperAdmins";

		public static string ContestOwners(Contest contest)
		{
			return "ContestOwners/" + contest.Id;
		}

		public static string ContestJudges(Contest contest)
		{
			return "ContestJudges/" + contest.Id;
		}

		public static string ContestParticipants(Contest contest)
		{
			return "ApprovedParticipants/" + contest.Id;
		}
	}
}