using System;
using System.Collections.Generic;
using System.Text;

using Ne.Database.Classes;

namespace Ne.ContestTypeHandlers
{
	public class ICPCSubmissionInfo : ISubmissionInfo
	{
		bool isAccepted;
		int stopTest;
		int maxTime;
		int maxMemory;
		bool wasJudged;

		public bool IsAccepted
		{
			get { return isAccepted; }
		}

		public int StopTest
		{
			get { return stopTest; }
		}

		public int MaxTime
		{
			get { return maxTime; }
		}

		public int MaxMemory
		{
			get { return maxMemory; }
		}

		public bool WasJudged
		{
			get { return wasJudged; }
		}

		public void FillFromSubmission(Submission submit)
		{
			submit.LoadLog();

			isAccepted = submit.Outcome == ICPCOutcomeMapper.Accepted;
			wasJudged = !OutcomeMapper.IsNotJudgeableOutcome(submit.Outcome) &&
									!OutcomeMapper.IsNotJudgedYetOutcome(submit.Outcome);

			if ( !OutcomeMapper.IsNotJudgeableOutcome(submit.Outcome) )
			{
				List<TestRunInfo> tests = submit.Log.TestCollection;

				for ( int i = 1; i <= tests.Count; ++i )
				{
					TestRunInfo t = tests[i - 1];

					maxMemory = Math.Max(maxMemory, t.RunResult.MemoryUsed);
					maxTime = Math.Max(MaxTime, t.RunResult.TimeWorked);
					if ( t.CheckStatus != CheckStatus.Ok )
					{
						stopTest = i;
						break;
					}
				}
			}
		}
	}
}
