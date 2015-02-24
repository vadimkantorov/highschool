using System;
using System.Collections;

namespace Ne.Judge
{
	public abstract class NeJudgeAttribute : Attribute
	{
	}
	
	public abstract class NeJudgeRequestAttribute : NeJudgeAttribute
	{
	}

	public class RequireContestIdAttribute : NeJudgeRequestAttribute
	{

	}

	public class RequireProblemIdAttribute : NeJudgeRequestAttribute
	{

	}

	/*public class RequireQuestionIdAttribute : NeJudgeRequestAttribute
	{

	}*/

	public class RequireUserIdAttribute : NeJudgeRequestAttribute
	{

	}

	public class RequireModeAttribute : NeJudgeRequestAttribute
	{
		Hashtable mod = new Hashtable();
		
		public Hashtable Modes
		{
			get
			{
				return mod;
			}
		}

		public RequireModeAttribute(string modes, params NeJudgeRequestAttribute[][] args)
		{
			string[] _modes = modes.Split(',');
			if(args.GetLength(1) != _modes.Length)
				throw new ArgumentException(" оличество опций не соответствует количеству переданных аргументов");
			for(int i = 0; i < _modes.Length; i++)
			{
				mod.Add(_modes[i], args[i]);
			}
		}
	}
}
