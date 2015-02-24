using System;
using System.IO;
using Model;
using Model.Testing;
using Tester.Checker;
using Xunit;

namespace Tester.Tests
{
	public class CheckerTests
	{
		public CheckerTests()
		{
			TestUtils.ClearExeDirectory();
		}

		[Fact]
		public void check_ok()
		{
			CheckResult checkResult = RunHCmp("123", "123");
			Assert.Equal(CheckStatus.Ok, checkResult.CheckStatus);
			Assert.Equal("answer is 123", checkResult.CheckerComment);
		}

		[Fact]
		public void check_wa()
		{
			CheckResult checkResult = RunHCmp("123", "-123");
			Assert.Equal(CheckStatus.WrongAnswer, checkResult.CheckStatus);
			Assert.Equal("expected -123, found 123", checkResult.CheckerComment);
		}

		[Fact]
		public void check_pe()
		{
			CheckResult checkResult = RunHCmp("abc", "123");
			Assert.Equal(CheckStatus.PresentationError, checkResult.CheckStatus);
			Assert.Equal("abc is not valid integer", checkResult.CheckerComment);
		}

		[Fact]
		public void check_fail()
		{
			var e = Assert.Throws<FailedActionException>(() => RunHCmp("123", "abc"));
			Assert.Contains("Код возврата: 3", e.Message);
			Console.WriteLine("Выкинутое исключение: " + e.Message);
		}

		private static CheckResult RunHCmp(string output, string answer)
		{
			TestUtils.WriteToExeDirectory("input.txt", "");
			TestUtils.WriteToExeDirectory("output.txt", output);
			TestUtils.WriteToExeDirectory("answer.txt", answer);
			var checker = new TestlibChecker(hCmp);
			string inputFile = Path.Combine(TestUtils.ExeDirectory, "input.txt");
			string outputFile = Path.Combine(TestUtils.ExeDirectory, "output.txt");
			string answerFile = Path.Combine(TestUtils.ExeDirectory, "answer.txt");
			return checker.Check(inputFile, outputFile, answerFile);
		}

		private static readonly string hCmp = Path.Combine(TestUtils.CheckersDirectory, "hcmp.exe");
	}
}