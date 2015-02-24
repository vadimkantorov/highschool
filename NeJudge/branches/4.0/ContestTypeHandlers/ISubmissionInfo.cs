using System;
using System.Collections.Generic;
using System.Text;

using Ne.DomainModel;

namespace Ne.ContestTypeHandlers
{
	public interface ISubmissionInfo
	{
		void FillFromSubmission(Submission submission);
	}
}
