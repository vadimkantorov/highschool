using System.Collections.Generic;
using Model;
using Model.Testing;

namespace Broker.ContestTypeHandlers
{
	public class TestRunVerdictor
	{
		readonly Dictionary<CheckStatus, GenericVerdict> checkStatusMap = new Dictionary<CheckStatus, GenericVerdict>
		{
				{CheckStatus.WrongAnswer, GenericVerdict.WrongAnswer},
				{CheckStatus.PresentationError, GenericVerdict.PresentationError}
		};

		readonly Dictionary<RunStatus, GenericVerdict> runStatusMap = new Dictionary<RunStatus, GenericVerdict>
		{
			{RunStatus.TimeLimitExceeded, GenericVerdict.TimeLimitExceeded},
			{RunStatus.IdlenessLimitExceeded, GenericVerdict.TimeLimitExceeded},
			{RunStatus.MemoryLimitExceeded, GenericVerdict.MemoryLimitExceeded},
			{RunStatus.SecurityViolation, GenericVerdict.SecurityViolation},
			{RunStatus.RuntimeError, GenericVerdict.RuntimeError},
		};
		
		public GenericVerdict Judge(TestRunInfo checkResult)
		{
			if (checkStatusMap.ContainsKey(checkResult.CheckResult.CheckStatus))
				return checkStatusMap[checkResult.CheckResult.CheckStatus];

			if (runStatusMap.ContainsKey(checkResult.RunResult.Status))
				return runStatusMap[checkResult.RunResult.Status];
			
			return GenericVerdict.Ok;
		}
	}
}