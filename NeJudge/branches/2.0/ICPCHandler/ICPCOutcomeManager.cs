using System;
using System.Collections.Generic;

using Ne.Database.Classes;

namespace ICPCHandler
{
	class ICPCOutcomeManager : Ne.ContestTypeHandlers.OutcomeManager
	{
		public const string Accepted = "Accepted";
		public const string TimeLimit = "Time Limit Exceeded";
		public const string MemoryLimit = "Memory Limit Exceeded";
		public const string OutputLimit = "Output Limit Exceeded";
		public const string SecurityViolation = "Security Violation";
		public const string RuntimeError = "Runtime Error";

		public const string WrongAnswer = "Wrong Answer";
		public const string PresentationError = "Presentation Error";

		public ICPCOutcomeManager() : base()
		{
			outcomes[Accepted] = "�������";
			outcomes[TimeLimit] = "�������� ����� �������";
			outcomes[MemoryLimit] = "�������� ����� ������";
			outcomes[OutputLimit] = "�������� ����� ������� ��������� �����";
			outcomes[SecurityViolation] = "��������� ������������";
			outcomes[RuntimeError] = "������ ������� ����������";

			outcomes[WrongAnswer] = "�������� �����";
			outcomes[PresentationError] = "�������� ������ ������";
		}
	}
}
