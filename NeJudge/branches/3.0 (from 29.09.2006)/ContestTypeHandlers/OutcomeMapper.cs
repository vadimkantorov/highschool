using System;
using System.Collections.Generic;
using System.Text;

using Ne.Interfaces;

namespace Ne.ContestTypeHandlers
{
	public class OutcomeMapper
	{
		public const string TestingFailure = "Testing Failure";
		public const string CannotJudge = "Cannot Judge";
		public const string Waiting = "Waiting";
		public const string Compiling = "Compiling";
		public const string Running = "Running";
		public const string CompilationError = "Compilation Error";

		protected Dictionary<string, OutcomeInfo> outcomes = new Dictionary<string, OutcomeInfo>();
		protected static readonly string[] notJudgeableOutcomes = { 
			TestingFailure, CannotJudge, CompilationError 
		};
		protected static readonly string[] notJudgedOutcomes = { 
			Waiting, Compiling, Running 
		};

		public OutcomeInfo[] GetAllOutcomes()
		{
			OutcomeInfo[] ret = new OutcomeInfo[outcomes.Count];
			outcomes.Values.CopyTo(ret, 0);
			Array.Sort(ret);
			return ret;
		}

		public OutcomeInfo GetOutcome(string outcomeKey)
		{
			if ( !outcomes.ContainsKey(outcomeKey) )
				return null;
			return outcomes[outcomeKey];
		}

		public OutcomeMapper()
		{
			outcomes[TestingFailure] = new OutcomeInfo("Ошибка тестирования", InformationKind.Negative);
			outcomes[CannotJudge] = new OutcomeInfo("Тестирование невозможно", InformationKind.Negative);
			outcomes[Waiting] = new OutcomeInfo("Ожидание", InformationKind.Neutral);
			outcomes[Compiling] = new OutcomeInfo("Компилируется", InformationKind.Neutral);
			outcomes[Running] = new OutcomeInfo("Тестируется", InformationKind.Neutral);
			outcomes[CompilationError] = new OutcomeInfo("Ошибка компиляции", InformationKind.Negative);
		}

		public static bool IsNotJudgeableOutcome(string outcome)
		{
			return Array.IndexOf(notJudgeableOutcomes, outcome) >= 0;
		}

		public static bool IsNotJudgedYetOutcome(string outcome)
		{
			return Array.IndexOf(notJudgedOutcomes, outcome) >= 0;
		}
	}
}
