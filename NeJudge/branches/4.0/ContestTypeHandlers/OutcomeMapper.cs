using System;
using System.Globalization;
using System.Collections.Generic;
using System.Text;

using Ne.DomainModel;

namespace Ne.ContestTypeHandlers
{
	public abstract class OutcomeMapper
	{
		/*public const string TestingFailure = "Testing Failure";
		public const string CannotJudge = "Cannot Judge";
		public const string Waiting = "Waiting";
		public const string Compiling = "Compiling";
		public const string Running = "Running";
		public const string CompilationError = "Compilation Error";

		protected SortedDictionary<string, OutcomeInfo> outcomes = new SortedDictionary<string, OutcomeInfo>();
		protected static readonly string[] notJudgeableOutcomes = { 
			TestingFailure, CannotJudge, CompilationError 
		};
		protected static readonly string[] notJudgedOutcomes = { 
			Waiting, Compiling, Running 
		};*/

		public OutcomeInfo[] GetAllOutcomes(CultureInfo lang)
		{
			return null;
			/*OutcomeInfo[] ret = new OutcomeInfo[outcomes.Count];
			outcomes.Values.CopyTo(ret, 0);
			return ret;*/
		}

		public OutcomeInfo[] GetAllOutcomes()
		{
			return GetAllOutcomes(CultureInfo.CurrentCulture);
		}

		public OutcomeInfo GetOutcome(string outcomeKey, CultureInfo lang)
		{
			return null;
		}

		public OutcomeInfo GetOutcome(string outcomeKey)
		{
			return GetOutcome(outcomeKey, CultureInfo.CurrentCulture);
		}

		/*public OutcomeMapper()
		{
			outcomes[TestingFailure] = new OutcomeInfo("������ ������������", InformationKind.Negative);
			outcomes[CannotJudge] = new OutcomeInfo("������������ ����������", InformationKind.Negative);
			outcomes[Waiting] = new OutcomeInfo("��������", InformationKind.Neutral);
			outcomes[Compiling] = new OutcomeInfo("�������������", InformationKind.Neutral);
			outcomes[Running] = new OutcomeInfo("�����������", InformationKind.Neutral);
			outcomes[CompilationError] = new OutcomeInfo("������ ����������", InformationKind.Negative);
		}

		public static bool IsNotJudgeableOutcome(string outcome)
		{
			return Array.IndexOf(notJudgeableOutcomes, outcome) >= 0;
		}

		public static bool IsNotJudgedYetOutcome(string outcome)
		{
			return Array.IndexOf(notJudgedOutcomes, outcome) >= 0;
		}*/
	}
}
