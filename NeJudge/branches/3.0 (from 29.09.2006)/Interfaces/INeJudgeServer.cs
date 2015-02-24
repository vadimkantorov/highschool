using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.Interfaces
{
	public interface INeJudgeServer
	{
		// outcome methods
		OutcomeInfo GetOutcome(int contestID, string outcomeKey);
		OutcomeInfo[] GetOutcomes(int contestID);

		// status methods
		LightweightRow GetStatusHeaders(int contestID);
		LightweightRow GetStatusInformation(int submissionID);

		// monitor methods
		LightweightTable GetMonitor(int contestID);

		// judging methods
		void JudgeSubmission(int submissionID);
		void RejudgeProblem(int problemID, int page);
	}
}
