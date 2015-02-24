using System;

using Ne.Database.Classes;

namespace Ne.Helpers
{
	public static class UrlRenderer
	{
		public static string RenderContestUrl(Contest con)
		{
			return String.Format("<a href='contest.aspx?contestID={0}'>{1}</a>", con.ID, con.Name);
		}

		public static string RenderMonitorUrl(Contest con)
		{
			return String.Format("<a href='monitor.aspx?contestID={0}'>{1}</a>", con.ID, "—сылка");
		}

		public static string RenderContestUrl(int contestID)
		{
			return "~/contest.aspx?contestID=" + contestID;
		}

		public static string RenderTestlogUrl(int submissionID)
		{
			return "~/testlog.aspx?submissionID=" + submissionID;
		}

		public static string RenderSubmitUrl(int problemID)
		{
			return "~/submit.aspx?problemID=" + problemID;
		}

		public static string RenderAskUrl(int problemID)
		{
			return "~/ask.aspx?problemID=" + problemID;
		}

		public static string RenderPrintContestUrl(int contestID)
		{
			return "~/printcontest.aspx?contestID=" + contestID;
		}

		public static string RenderPrintProblemUrl(int problemID)
		{
			return "~/printproblem.aspx?problemID=" + problemID;
		}

		public static string RenderQuestionsUrl(int contestID)
		{
			return "~/questions.aspx?contestID=" + contestID;
		}

		public static string RenderEditProblemUrl(int problemID, char ret)
		{
			string s = "~/editproblem.aspx?problemID=" + problemID;
			if ( ret != ' ' )
				s += "&ret=" + ret;
			return s;
		}

		public static string RenderStatusUrl(int contestID)
		{
			return "~/status.aspx?contestID=" + contestID;
		}

		public static string RenderMonitorUrl(int contestID)
		{
			return "~/monitor.aspx?contestID=" + contestID;
		}

		public static string RenderEditContestUrl(int contestID)
		{
			return "~/editcontest.aspx?contestID=" + contestID;
		}

		public static string RenderProblemUrl(int problemID)
		{
			return "~/problem.aspx?problemID=" + problemID;
		}
	}
}