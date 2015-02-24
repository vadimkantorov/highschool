using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.IO;

using Ne.Tester.Common;

namespace Ne.Tester
{
	[ServiceBehavior(InstanceContextMode=InstanceContextMode.Single)]
	public class Tester : ITester
	{
		List<int> problems;

		public Tester()
		{
			problems = new List<int>();
			
			string rootdir = NeTesterConfiguration.RootDir;
			string problemsFolder = NeTesterConfiguration.ProblemsFolder;

			foreach( string folder in Directory.GetDirectories(Path.Combine(rootdir, problemsFolder)) )
				problems.Add(Convert.ToInt32(folder));
		}
		
		public void EnqueueSubmission(	int submissionID, int problemID, 
										string languageID, string source)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public string[] GetAvailableLanguages()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int GetBusiness()
		{
			throw new Exception("The method or operation is not implemented.");
		}

		public int[] GetCachedProblems()
		{
			return problems.ToArray();
		}
	}
}

#if PHONY
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
