using System;
using Ne.Database.Classes;

using Ne.ContestTypeHandlers;

namespace ICPCHandler
{
	class ICPCStatusManager : Ne.ContestTypeHandlers.IStatusManager
	{
		static readonly string[] headers = new string[] { "Тест №" , "Максимальное время" , "Максимальная память" };
		
		public string[] GetHeaders()
		{
			return headers;
		}

		public string[] GetInfo(Submission s)
		{
			string[] ans = new string[] {"","","" };

			if ( s.Outcome != OutcomeManager.CompilationError &&
				s.Outcome != OutcomeManager.Compiling &&
				s.Outcome != OutcomeManager.Waiting &&
				s.Outcome != OutcomeManager.Running &&
				s.Outcome != OutcomeManager.CannotJudge &&
				s.Outcome != OutcomeManager.TestingFailure )
			{
				double maxtime = -1, maxmem = -1;
				int stoptest = -1;
				s.LoadLog();
				for ( int i = 0; i < s.Log.TestCollection.Count; i++ )
				{
					TestRunInfo t = s.Log.TestCollection[i];
					if ( t.RunResult.MemoryUsed > maxmem )
						maxmem = t.RunResult.MemoryUsed;
					if ( t.RunResult.TimeWorked > maxtime )
						maxtime = t.RunResult.TimeWorked;
					if ( t.CheckStatus != CheckStatus.Ok )
					{
						stoptest = i + 1;
						break;
					}
				}

				if ( stoptest > 0 )
					ans[0] = stoptest.ToString();
				if ( maxtime > 0 )
					ans[1] = Math.Round(maxtime / 1000, 4) + " сек";
				if ( maxmem > 0 )
					ans[2] = maxmem / 1024 + " КБ";
			}
			return ans;
		}
	}
}