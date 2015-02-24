using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Model.Factories;
using Model.Testing;

namespace Broker.ContestTypeHandlers
{
	public abstract class ContestTypeHandlerBase : NamedBase, IContestTypeHandler
	{
		public SubmissionResult BuildSubmissionResult(IList<Test> tests, TestLog log)
		{
			if (log.CheckResults == null)
				return Build(GenericVerdict.CompilationError, null);

			var points = HasPoints ? 0 : (int?) null;
			var verdictor = new TestRunVerdictor();
			for (int i = 0; i < log.CheckResults.Count; i++)
			{
				var verdict = verdictor.Judge(log.CheckResults[i]);
				if (IsFailedTest(verdict, tests[i]))
					return Build(verdict, 1 + i);
				points += tests[i].Points;
			}

			return Build(GenericVerdict.Accepted, points);
		}

		protected virtual bool IsFailedTest(GenericVerdict verdict, Test test)
		{
			return verdict != GenericVerdict.Ok;
		}

		protected virtual bool HasPoints { get { return true; } }

		static SubmissionResult Build(GenericVerdict verdict, int? points)
		{
			return new SubmissionResult { Verdict = verdict.ToString(), Value = points };
		}

		public abstract IMonitorBuilder CreateMonitorBuilder(Contest contest);
		public abstract string RenderMontior(object monitor);
	}
}