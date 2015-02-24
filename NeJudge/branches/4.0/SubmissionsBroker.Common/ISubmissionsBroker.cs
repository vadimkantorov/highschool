using System;
using System.Collections.Generic;
using System.Text;

using Ne.DomainModel;

namespace SubmissionsBroker.Common
{
	public interface ISubmissionsBroker
	{
		void Submit(int submissionID);
		LightweightTable GetMonitor(int contestID);
		void ReportSolutionChecked(int submissionID, TestLog testLog);

		byte[][] FetchTests(int problemID);
		byte[] FetchChecker(int problemID);
	}
}
