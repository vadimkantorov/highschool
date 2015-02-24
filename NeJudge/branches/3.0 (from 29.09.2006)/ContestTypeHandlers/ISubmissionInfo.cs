using System;
using System.Collections.Generic;
using System.Text;

using Ne.Database.Classes;

namespace Ne.ContestTypeHandlers
{
	public interface ISubmissionInfo
	{
		void FillFromSubmission(Submission submit);
	}
}
