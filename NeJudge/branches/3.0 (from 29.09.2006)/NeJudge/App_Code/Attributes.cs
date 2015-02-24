using System;
using System.Collections.Generic;

namespace Ne.Judge
{
	public abstract class NeJudgeAttribute : Attribute
	{}

	public abstract class RequestAttribute : NeJudgeAttribute
	{}

	public class RequireContestIDAttribute : RequestAttribute
	{}

	public class RequireProblemIDAttribute : RequestAttribute
	{}

	public class RequireEveryProblemIDAttribute : RequestAttribute
	{}

	/*public class RequireQuestionIdAttribute : RequestAttribute
	{

	}*/

	public class RequireUserIDAttribute : RequestAttribute
	{}

	public class RequireOutcomeAttribute : RequestAttribute
	{}

	public class RequireSubmissionIDAttribute : RequestAttribute
	{}

	[AttributeUsage(AttributeTargets.Class,AllowMultiple=true)]
	public class RequireModeAttribute : RequestAttribute
	{
		string mode;
		List<RequestAttribute> attr = new List<RequestAttribute>();

		public string Mode
		{
			get { return mode; }
		}

		public RequestAttribute[] Requirements
		{
			get { return attr.ToArray(); }
		}
		//TODO: otherz
		public RequireModeAttribute(string mode, params string[] args)
		{
			this.mode = mode;
			foreach( string s in args )
			{
				switch( s )
				{
					case "RequireContestId":
						attr.Add(new RequireContestIDAttribute());
						break;
					case "RequireProblemId":
						attr.Add(new RequireProblemIDAttribute());
						break;
				}
			}
		}
	}
}