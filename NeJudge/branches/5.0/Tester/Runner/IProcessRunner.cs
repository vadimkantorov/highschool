using System;
using Model;
using Model.Testing;

namespace Tester.Runner
{
	public interface IProcessRunner : IDisposable
	{
		RunResult Run();
		ResourceUsage ResourceLimits { get; set; }
		ulong IdlenessLimit { get; set; }
		bool DisallowChildProcesses { get; set; }
	}
}