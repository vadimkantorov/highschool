using System;
using Model;

namespace Broker.Common
{
	public class SubmissionInfo
	{
		public Guid TestInfoId { get; set; }
		public ProgramSource Source { get; set; }
		public DateTime SubmittedAt { get; set; }
		public bool RunAllTests { get; set; }
		public ResourceUsage SubmissionLimits { get; set; }
		public int SubmissionId { get; set; }
		public SubmissionType Type { get; set; }
		public string ContestType { get; set; }
		public string InputFileName { get; set; }
		public string OutputFileName { get; set; }

		public SubmissionInfo()
		{
		}

		public SubmissionInfo(Submission s)
		{
			TestInfoId = s.Problem.TestInfo.Id;
			Source = s.Source;
			SubmittedAt = s.SubmittedAt;
			RunAllTests = false;
			SubmissionLimits = s.Problem.Limits;
			SubmissionId = s.Id;
			Type = s.Type;
			ContestType = s.Problem.Contest.Type;
			InputFileName = "input.txt";
			OutputFileName = "output.txt";
		}

		public override string ToString()
		{
			return string.Format("id = {0}", SubmissionId);
		}
	}
}