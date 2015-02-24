using System;
using System.Collections.Generic;
using System.Text;
using Ne.Interfaces;

namespace Ne.ContestTypeHandlers
{
	public class ICPCOutcomeMapper : OutcomeMapper
	{
		public const string Accepted = "Accepted";
		public const string TimeLimit = "Time Limit Exceeded";
		public const string MemoryLimit = "Memory Limit Exceeded";
		public const string OutputLimit = "Output Limit Exceeded";
		public const string SecurityViolation = "Security Violation";
		public const string RuntimeError = "Runtime Error";

		public const string WrongAnswer = "Wrong Answer";
		public const string PresentationError = "Presentation Error";

		public ICPCOutcomeMapper()
		{
			outcomes[Accepted] = new OutcomeInfo("ОК", InformationKind.Positive);
			outcomes[TimeLimit] = new OutcomeInfo("Превышен лимит времени", InformationKind.Negative);
			outcomes[MemoryLimit] = new OutcomeInfo("Превышен лимит памяти", InformationKind.Negative);
			outcomes[OutputLimit] = new OutcomeInfo("Превышен лимит размера выходного файла", InformationKind.Negative);
			outcomes[SecurityViolation] = new OutcomeInfo("Нарушение безопасности", InformationKind.Negative);
			outcomes[RuntimeError] = new OutcomeInfo("Ошибка времени выполнения", InformationKind.Negative);

			outcomes[WrongAnswer] = new OutcomeInfo("Неверный ответ", InformationKind.Negative);
			outcomes[PresentationError] = new OutcomeInfo("Неверный формат ответа", InformationKind.Negative);
		}
	}
}
