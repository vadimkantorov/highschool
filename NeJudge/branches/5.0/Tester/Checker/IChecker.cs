using Model.Testing;

namespace Tester.Checker
{
	public interface IChecker
	{
		CheckResult Check(string inputFile, string outputFile, string answerFile);
	}
}