using System;
using System.Collections.Generic;
using System.Text;

using Ne.Interfaces;

namespace Ne.ContestTypeHandlers
{
	public interface IMonitorBuilder
	{
		void Initialize();
		LightweightTable Build();
		void OnSubmissionJudged(int submissionID);
	}
}
