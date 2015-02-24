using System.Collections.Generic;
using Model;
using System.Linq;

namespace Broker.ContestTypeHandlers
{
	public class IcpcMonitor
	{
		public class IcpcMonitorUserLine
		{
			public List<int> Results { get; set; }

			public string UserDisplayName { get; set; }
			
			public int Time { get; set;}

			public int AcceptedProblems { get; set; }

			public IcpcMonitorUserLine()
			{
				Results = new List<int>();
			}
		}

		public List<string> ProblemShortNames { get; set; }

		public List<IcpcMonitorUserLine> Lines { get; set; }
	}
	
	public class IcpcMonitorBuilder : IMonitorBuilder
	{
		public object BuildMonitor(IEnumerable<Submission> submissions)
		{
			var lines = new Dictionary<string, IcpcMonitor.IcpcMonitorUserLine>();
			foreach (var submission in submissions)
			{
				var line = GetOrAddLine(lines, submission.Author.DisplayName);
				UpdateLine(line, submission);
			}

			var orderedLines = from line in lines.Values
			                   orderby line.AcceptedProblems descending,
									  	line.Time ascending
			                   select line;
			return new IcpcMonitor { ProblemShortNames = shortNames.ToList(), Lines = orderedLines.ToList()};
		}

		void UpdateLine(IcpcMonitor.IcpcMonitorUserLine line, Submission submission)
		{
			var shortName = submission.Problem.ShortName;
			var index = shortNames.FindIndex(x => x == shortName);
			if (line.Results[index] > 0)
				return;

			if (submission.Result.Verdict == GenericVerdict.Accepted.ToString())
			{
				int attempts = -line.Results[index];
				line.AcceptedProblems++;
				line.Time += (int)(submission.SubmittedAt - contest.Beginning).TotalMinutes;
				line.Time += 20 * attempts;
				line.Results[index] = attempts + 1;
			}
			else
				line.Results[index]--;
		}

		IcpcMonitor.IcpcMonitorUserLine GetOrAddLine(Dictionary<string, IcpcMonitor.IcpcMonitorUserLine> lines, string userDisplayName)
		{
			IcpcMonitor.IcpcMonitorUserLine line;
			if(!lines.TryGetValue(userDisplayName, out line))
			{
				lines[userDisplayName] = line = new IcpcMonitor.IcpcMonitorUserLine
				{
					UserDisplayName = userDisplayName,
					Results = Enumerable.Repeat(0, shortNames.Count).ToList()
				};
			}
			return line;
		}

		public IcpcMonitorBuilder(Contest contest)
		{
			this.contest = contest;
			shortNames = contest.Problems.Select(x => x.ShortName).ToList();
		}

		readonly Contest contest;
		readonly List<string> shortNames;
	}
}