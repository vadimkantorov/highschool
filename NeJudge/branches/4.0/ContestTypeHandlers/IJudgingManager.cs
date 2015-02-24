using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.ContestTypeHandlers
{
	public interface IJudgingManager
	{
		void Initialize();
		void JudgeSubmission(int submissionID);
		void RejudgeSubmission(int submissionID);
	}
}
