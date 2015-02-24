using System;

namespace Ne.Judge
{
	/// <summary>
	/// Summary description for NeJudgeException.
	/// </summary>
	public class NeJudgeException : ApplicationException
	{
		public NeJudgeException(string message) : base(message)
		{}
	}

	public class NeJudgeSecurityException : NeJudgeException
	{
		public NeJudgeSecurityException(string message) : base(message)
		{}
	}

	public class NeJudgeInvalidParametersException : NeJudgeException
	{
		public NeJudgeInvalidParametersException(string parameter)
			: base(string.Format("� �������� ��������� \"{0}\" ���� �������� ������������ ��������.", parameter))
		{}

		public NeJudgeInvalidParametersException(string parameter, string message)
			: base(
				string.Format("� �������� ��������� \"{0}\" ���� �������� ������������ ��������.\r\n���������: \"{1}\"", parameter,
				              message))
		{}
	}
}