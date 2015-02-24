#if PHONY
using System;
using System.Collections.Generic;

using Ne.Database.Classes;
using Ne.ContestTypeHandlers;

namespace Ne.Tester
{
	public class NeTesterImplementation : MarshalByRefObject, INeTester
	{
		Dictionary<int, Tester> testers = new Dictionary<int,Tester>();

		public void EnqueueSubmission(int ID)
		{
			Submission sm = Submission.GetSubmission(ID);
			Contest c = Contest.GetContest(Problem.GetProblem(sm.ProblemID).ContestID);

			if ( !testers.ContainsKey(c.ID) )
				testers[c.ID] = Factory.GetHandlerInstance(c.Type).TesterManager.CreateTester();

			testers[c.ID].EnqueueSubmission(sm);
		}
	}
}
#endif
