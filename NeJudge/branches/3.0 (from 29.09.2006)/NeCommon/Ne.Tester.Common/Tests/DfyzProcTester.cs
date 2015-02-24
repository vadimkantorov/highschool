using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;

using Ne.Database.Classes;

using NUnit.Framework;

namespace Ne.Tester
{
	[TestFixture]
	public class DfyzProcTester
	{
		private const string TESTS_DIR = @"D:\My Documents\NeJudge\branches\2.0\DfyzProc\Tests";

		[Test]
		public void TestOk()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "Ok.exe"), TESTS_DIR, null);
			RunResult res = dp.Run();
			Assert.AreEqual(RunStatus.Ok, res.Status);
			Assert.AreEqual(0, res.ExitCode);
		}

		[Test]
		public void TestArguments()
		{
			string argsFile = Path.Combine(TESTS_DIR, "args.txt");
			if ( File.Exists(argsFile) )
				File.Delete(argsFile);
			string[] trickyArgs = new string[] {"\"\"\"\"\"", "\\\"\"\\", "\\\\"};

			List<string> argList = new List<string>();
			for ( int i = 1; i <= 1000; i++ )
				argList.Add(i.ToString());
			foreach ( string s in trickyArgs )
				argList.Add(s);

			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "Args.exe"), TESTS_DIR, argList);
			RunResult res = dp.Run();
			Assert.AreEqual(RunStatus.Ok, res.Status);
			Assert.AreEqual(0, res.ExitCode);

			StreamReader sr = new StreamReader(argsFile);
			string nLine;

			for ( int i = 1; i <= 1000; i++ )
			{
				nLine = sr.ReadLine();
				Assert.AreNotEqual(null, nLine);
				Assert.AreEqual(i, int.Parse(nLine, NumberStyles.Integer));
			}

			for ( int i = 0; i < trickyArgs.Length; i++ )
			{
				nLine = sr.ReadLine();
				Assert.AreNotEqual(null, nLine);
				Assert.AreEqual(trickyArgs[i], nLine);
			}

			sr.Close();

			if ( File.Exists(argsFile) )
				File.Delete(argsFile);
		}

		[Test]
		public void TestTimeLimit()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "TimeLimit.exe"), TESTS_DIR, null);
			dp.TimeLimit = 1000;
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.TimeLimit, rr.Status);
		}

		[Test]
		public void TestMemoryLimit()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "MemoryLimit"), TESTS_DIR, null);
			dp.MemoryLimit = 6*1048576;
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.MemoryLimit, rr.Status);
		}

		[Test]
		public void TestMultithreading()
		{
			for ( int i = 0; i < 10; i++ )
			{
				Thread t = new Thread(TestOk);
				t.Start();
			}
		}

		[Test]
		public void TestCrashes()
		{
			DfyzProc avDp = new DfyzProc(Path.Combine(TESTS_DIR, "AV.exe"), TESTS_DIR, null);
			DfyzProc zdDp = new DfyzProc(Path.Combine(TESTS_DIR, "Zerodiv.exe"), TESTS_DIR, null);
			RunResult rr = avDp.Run();
			Assert.AreEqual(RunStatus.RuntimeError, rr.Status);
			rr = zdDp.Run();
			Assert.AreEqual(RunStatus.RuntimeError, rr.Status);
		}

		[Test]
		public void TestExitCode()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "ExitCode.exe"), TESTS_DIR, null);
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.Ok, rr.Status);
			Assert.AreEqual(31337, rr.ExitCode);
		}

		[Test]
		public void TestIdlenessLimit()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "IdlenessLimit.exe"), TESTS_DIR, null);
			dp.IdlenessLimit = 2500;
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.SecurityViolation, rr.Status);
		}

		[Test]
		public void TestRedirections()
		{
			string inputFile = Path.Combine(TESTS_DIR, "input.txt");
			string outputFile = Path.Combine(TESTS_DIR, "output.txt");
			string errorFile = Path.Combine(TESTS_DIR, "error.txt");

			if ( File.Exists(inputFile) )
				File.Delete(inputFile);
			if ( File.Exists(errorFile) )
				File.Delete(errorFile);
			if ( File.Exists(outputFile) )
				File.Delete(outputFile);

			List<int> randoms = new List<int>();
			StreamWriter sw = new StreamWriter(inputFile);
			Random r = new Random(Environment.TickCount);

			for ( int i = 0; i < 1000; i++ )
			{
				int nextR = r.Next();
				randoms.Add(nextR);
				sw.WriteLine(nextR.ToString());
			}

			sw.Close();

			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "StdStreams.exe"), TESTS_DIR, null);

			dp.StdinRedirection = inputFile;
			dp.StdoutRedirection = outputFile;
			dp.StderrRedirection = errorFile;

			RunResult rr = dp.Run();
			if ( rr.Status == RunStatus.Failure )
				Console.Error.WriteLine(dp.Comment);
			Assert.AreEqual(RunStatus.Ok, rr.Status);

			StreamReader rOut = new StreamReader(outputFile);
			StreamReader rErr = new StreamReader(errorFile);

			for ( int i = 0; i < randoms.Count; i++ )
			{
				Assert.AreEqual(randoms[i] + 1, int.Parse(rOut.ReadLine(), NumberStyles.Integer));
				Assert.AreEqual(randoms[i] + 2, int.Parse(rErr.ReadLine(), NumberStyles.Integer));
			}

			rOut.Close();
			rErr.Close();

			if ( File.Exists(inputFile) )
				File.Delete(inputFile);
			if ( File.Exists(errorFile) )
				File.Delete(errorFile);
			if ( File.Exists(outputFile) )
				File.Delete(outputFile);
		}

		[Test]
		public void TestOutputLimit()
		{
			string outputFile = Path.Combine(TESTS_DIR, "big-file.txt");
			if ( File.Exists(outputFile) )
				File.Delete(outputFile);
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "OutputLimit.exe"), TESTS_DIR, null);
			dp.OutputLimit = 10000;
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.OutputLimit, rr.Status);
			if ( File.Exists(outputFile) )
				File.Delete(outputFile);
		}

		[Test]
		public void TestSecurityViolation()
		{
			DfyzProc dp = new DfyzProc(Path.Combine(TESTS_DIR, "Spawn.exe"), TESTS_DIR, null);
			dp.AllowProcessCreation = false;
			RunResult rr = dp.Run();
			Assert.AreEqual(RunStatus.SecurityViolation, rr.Status);
		}
	}
}