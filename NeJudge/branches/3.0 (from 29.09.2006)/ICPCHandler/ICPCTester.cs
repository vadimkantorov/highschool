using System;
using System.Collections.Generic;
using System.Threading;
using System.Reflection;
using System.IO;

using log4net;

using Ne.Tester;
using Ne.Database.Classes;
using Ne.ContestTypeHandlers;

namespace ICPCHandler
{
	class Worker
	{
		Submission sm;
		TestingDirectoryInfo tdInfo;
		string slnExe;
		string runComment;
		Problem problem;
		
		static readonly ILog logger = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		RunResult DoRun()
		{
			DfyzProc prc = new DfyzProc(slnExe, tdInfo.SandboxDir, null);
			prc.AllowProcessCreation = false;
			prc.TimeLimit = problem.TimeLimit;
			prc.MemoryLimit = problem.MemoryLimit;
			prc.OutputLimit = problem.OutputLimit;
			prc.IdlenessLimit = NeTesterConfiguration.IdlenessLimit;

			RunAsParameters rParams = NeTesterConfiguration.RunAsParams;

			if ( rParams.Username != null && rParams.Username != "" && rParams.Password != null )
			{
				prc.Username = rParams.Username;
				prc.Password = rParams.Password;
			}

			prc.StdinRedirection = problem.InputFile == Problem.STDIN_NAME ? 
				Path.Combine(tdInfo.SandboxDir, Problem.STDIN_NAME) : DfyzProc.NULL_DEVICE;
			prc.StdoutRedirection = problem.OutputFile == Problem.STDOUT_NAME ? 
				Path.Combine(tdInfo.SandboxDir, Problem.STDOUT_NAME) : DfyzProc.NULL_DEVICE;
			prc.StderrRedirection = DfyzProc.NULL_DEVICE;

			RunResult ret = prc.Run();
			runComment = prc.Comment;
			return ret;
		}

		internal void DoWork(object state)
		{
			problem = Problem.GetProblem(sm.ProblemID);
			sm.Log = new TestLog();
			
			sm.Outcome = OutcomeManager.Compiling;
			sm.Store();

			string slnSrc = Tester.FetchSolution(sm, tdInfo);
			slnExe = Path.Combine(tdInfo.SandboxDir, NeTesterConfiguration.SOLUTION_EXE);
			Compiler comp = new Compiler(slnSrc, slnExe, tdInfo.RootDir, sm.LanguageID);

			logger.Info("Compiling submission");
			bool ret = comp.Compile();
			sm.Log.CompilationReport = comp.CompilationReport;

			if ( !ret )
			{
				logger.Warn("Compilation error");
				sm.Outcome = OutcomeManager.CompilationError;
				sm.Store();
				return;
			}

			sm.Outcome = OutcomeManager.Running;
			sm.Store();

			Checker check = new Checker(Tester.FetchChecker(problem, tdInfo), tdInfo.CheckerDir);

			Test[] tests = Test.GetTests(problem.ID);
			
			bool nextTest = true;
			CheckStatus cs = CheckStatus.NotChecked;
			string chComment = "";

			int testNum = 1;
			sm.Outcome = ICPCOutcomeManager.Accepted;

			foreach ( Test t in tests )
			{
				if ( !nextTest )
					break;
				logger.Info(String.Format("Running test #{0}", testNum));

				cs = CheckStatus.NotChecked;
				chComment = "";

				string[] files = new string[] { 
					Path.Combine(tdInfo.SandboxDir, problem.InputFile),
					Path.Combine(tdInfo.RootDir, NeTesterConfiguration.ANSWER_FILE)
				};

				string[] filesWithOutput = new string[] { 
					files[0], 
					Path.Combine(tdInfo.SandboxDir, problem.OutputFile), 
					files[1] 
				};

				Tester.FetchTestData(t, files);
				RunResult rr = DoRun();

				logger.Info(String.Format("Run status - {0}", rr.Status.ToString()));

				if ( rr.Status == RunStatus.Ok )
				{
					cs = check.Check(filesWithOutput);
					chComment = check.CheckerComment;
					logger.Info(String.Format("Check status - {0}", cs.ToString()));
					if ( cs != CheckStatus.Ok )
					{
						nextTest = false;
						switch ( cs )
						{
							case CheckStatus.WrongAnswer:
								sm.Outcome = ICPCOutcomeManager.WrongAnswer;
								break;
							case CheckStatus.PresentationError:
								sm.Outcome = ICPCOutcomeManager.PresentationError;
								break;
						}
					}
				}
				else
				{
					switch ( rr.Status )
					{
						case RunStatus.Ok:
							sm.Outcome = ICPCOutcomeManager.Accepted;
							break;
						case RunStatus.TimeLimit:
							sm.Outcome = ICPCOutcomeManager.TimeLimit;
							break;
						case RunStatus.MemoryLimit:
							sm.Outcome = ICPCOutcomeManager.MemoryLimit;
							break;
						case RunStatus.OutputLimit:
							sm.Outcome = ICPCOutcomeManager.OutputLimit;
							break;
						case RunStatus.RuntimeError:
							sm.Outcome = ICPCOutcomeManager.RuntimeError;
							break;
						case RunStatus.SecurityViolation:
							sm.Outcome = ICPCOutcomeManager.SecurityViolation;
							break;
						case RunStatus.Failure:
							logger.Error(String.Format("Running solution failed - {0}", runComment));
							sm.Outcome = OutcomeManager.TestingFailure;
							break;
					}
					nextTest = false;
				}

				sm.Log.TestCollection.Add(new TestRunInfo(rr, cs, chComment));
				
				string[] arcNames = new string[] {
					"Input",
					"Output",
					"Answer"
				};

				for ( int i = 0; i < 3; ++i )
					if ( File.Exists(filesWithOutput[i]) )
						File.Copy(filesWithOutput[i], Path.Combine(tdInfo.ArchiveDir,
							String.Format("{0}{1:D3}.txt", arcNames[i], testNum)));

				foreach ( string s in filesWithOutput )
					if ( File.Exists(s) )
						File.Delete(s);
				++testNum;
			}

			logger.Info(String.Format("Outcome - {0}", sm.Outcome));
			sm.Store();
		}

		internal Worker(Submission subm)
		{
			sm = subm;
			tdInfo = new TestingDirectoryInfo();
		}
	}

	public class ICPCTester : Ne.Tester.Tester
	{
		public override void EnqueueSubmission(Submission submission)
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(new Worker(submission).DoWork));
		}
	}
}
