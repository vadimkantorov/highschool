using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Model;

namespace Timeline
{
	public class LogImporter
	{
		public IEnumerable<Submission> Import(string logPath)
		{
			var lines = File.ReadAllLines(logPath);
			var teams = new Dictionary<string, User>();
			var contest = new Contest();

			Func<Match, string, string> c = (x, y) => x.Groups[y].ToString();
			Func<int, int?> nullIfZero = x => x == 0 ? (int?)null : x;

			return from line in lines
			       select Regex.Match(line, @"(?<team>\w+)\s+(?<problem>\w)\s+(?<time>\d+)\s+(?<test>\d+)\s+(?<verdict>.{30})")
			       into m where m.Groups.Count >= 6
			       select new Submission
			              	{
			              		Author = GetOrAddTeam(teams, c(m, "team")),
			              		Problem = GetOrAddProblem(contest, c(m, "problem")),
			              		SubmittedAt = contest.Beginning.AddMinutes(Convert.ToInt32(c(m, "time"))),
			              		Result = new SubmissionResult
			              		         	{
			              		         		Verdict = c(m, "verdict").Trim(),
			              		         		Value = nullIfZero(Convert.ToInt32(c(m, "test")))
			              		         	}
			              	};
		}

		static User GetOrAddTeam(IDictionary<string, User> teams, string name)
		{
			if(!teams.ContainsKey(name))
				teams.Add(name, new User {DisplayName = name});
			return teams[name];
		}

		static Problem GetOrAddProblem(Contest contest, string shortName)
		{
			var res = contest.Problems.FirstOrDefault(x => x.ShortName == shortName);
			if (res == null)
			{
				res = new Problem {ShortName = shortName, Contest = contest, Statement = new FormattedDocument {Name = shortName}};
				contest.Problems.Add(res);
			}
			return res;
		}
	}
}