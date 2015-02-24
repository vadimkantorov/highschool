using System;
using System.Collections.Generic;
using System.Text;

using Ne.DomainModel;

namespace Ne.ContestTypeHandlers
{
	public class SubmissionInfoCache<T>
		where T : class, ISubmissionInfo, new()
	{
		Dictionary<int, T> infos = new Dictionary<int, T>();

		public void AddSubmissionInfo(int submissionID)
		{
			//AddSubmissionInfo(Submission.GetSubmission(submissionID));
		}

		public void AddSubmissionInfo(Submission submission)
		{
			T tmp = new T();
			tmp.FillFromSubmission(submission);
			infos[submission.ID] = tmp;
		}

		public T GetSubmissionInfo(int submissionID)
		{
			if ( !infos.ContainsKey(submissionID) )
				return null;
			return infos[submissionID];
		}
	}
}
