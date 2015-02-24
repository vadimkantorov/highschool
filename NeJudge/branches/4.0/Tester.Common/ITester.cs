using System;
using System.ServiceModel;

namespace Ne.Tester.Common
{
	[ServiceContract]
	public interface ITester
	{
		[OperationContract]
		void EnqueueSubmission(int submissionID, int problemID,
								string languageID, string source);
		
		[OperationContract]
		string[] GetAvailableLanguages();

		[OperationContract]
		int GetBusiness();

		[OperationContract]
		int[] GetCachedProblems();
	}
}