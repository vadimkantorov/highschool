using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;

using Ne.ContestTypeHandlers;
using Ne.Database.Classes;
using Ne.Database.Interfaces;
using Ne.Interfaces;

using log4net;

namespace Ne.Judge.Server
{
	class Server :  MarshalByRefObject, INeJudgeServer
	{
		static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
		Dictionary<int, ContestTypeHandler> handlers = new Dictionary<int, ContestTypeHandler>();

		ContestTypeHandler GetHandler(int contestID)
		{
			if ( !handlers.ContainsKey(contestID) )
			{
				string type = Contest.GetContest(contestID).Type;
				log.DebugFormat("Creating handler for contest {0} with type {1}", contestID, type);
				handlers[contestID] = Factory.CreateContestHandler(type, contestID);

				//handlers[contestID].MonitorBuilder.Initialize();
			}

			return handlers[contestID];
		}

		int GetContestIDBySubmissionID(int submissionID)
		{
			return Problem.GetProblem(Submission.GetSubmission(submissionID).ProblemID).ContestID;
		}

		public Server()
		{
			try
			{
				DataProvider.Initialize();
			}
			catch ( Exception ex )
			{
				log.Error("Failed to initialize NeDatabase", ex);
				Environment.Exit(1);
			}

			try
			{
				Factory.Initialize();
			}
			catch ( Exception ex )
			{
				log.Error("Failed to initialize contest type handlers factory", ex);
				Environment.Exit(1);
			}
			log.Info("Server was created");
		}

		~Server()
		{
			log.Info("Server was destroyed");
		}

		#region INeJudgeServer Members

		public OutcomeInfo GetOutcome(int contestID, string outcomeKey)
		{
			return GetHandler(contestID).OutcomeMapper.GetOutcome(outcomeKey);
		}

		public OutcomeInfo[] GetOutcomes(int contestID)
		{
			return GetHandler(contestID).OutcomeMapper.GetAllOutcomes();
		}

		public LightweightRow GetStatusHeaders(int contestID)
		{
			return GetHandler(contestID).StatusBuilder.GetHeaders();
		}

		public LightweightRow GetStatusInformation(int submissionID)
		{
			int contestID = GetContestIDBySubmissionID(submissionID);

			if ( log.IsDebugEnabled )
			{
				string type = Contest.GetContest(contestID).Type;
				log.DebugFormat("Creating status for submission {0} (contest type {1})", submissionID, type);
			}

			return GetHandler(contestID).StatusBuilder.GetInformation(submissionID);
		}

		public LightweightTable GetMonitor(int contestID)
		{
			return GetHandler(contestID).MonitorBuilder.Build();
		}

		public void JudgeSubmission(int submissionID)
		{
			int contestID = GetContestIDBySubmissionID(submissionID);

			GetHandler(contestID).JudgingManager.JudgeSubmission(submissionID);
		}

		public void RejudgeProblem(int problemID, int page)
		{
			
		}

		#endregion
	}
}
