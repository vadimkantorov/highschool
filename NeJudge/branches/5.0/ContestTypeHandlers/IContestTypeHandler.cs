using System.Collections.Generic;
using ContestTypeHandlers;
using Model;
using Model.Factories;
using Model.Testing;

namespace Broker
{
	public interface IContestTypeHandler : INamed
	{
		SubmissionResult BuildSubmissionResult(IList<Test> tests, TestLog log);
		IMonitorBuilder CreateMonitorBuilder(Contest contest);
		string RenderMontior(object monitor);
	}

	public interface IMonitorBuilder
	{
		object BuildMonitor(IEnumerable<Submission> submissions);
	}
}