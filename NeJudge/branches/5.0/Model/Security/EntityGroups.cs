namespace Model
{
	public static class EntityGroups
	{
		public const string ActiveContests = "ActiveContests";

		public static string Submissions(User user)
		{
			return "Submissions/ByUser/" + user.Id;
		}

		public static string Submissions(Contest contest)
		{
			return "Submissions/ByContest/" + contest.Id;
		}

		public static string QuestionsAndAnswers(User user)
		{
			return "QuestionsAndAnswers/ByUser/" + user.Id;
		}

		public static string QuestionsAndAnswers(Contest contest)
		{
			return "QuestionsAndAnswers/ByContest/" + contest.Id;
		}
	}
}