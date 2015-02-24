using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel.Security
{
	[Flags]
	public enum ProblemRights
	{
		ViewingClosed					= 1,
		Editing							= 1 << 1,
		ViewingAndAnsweringQuestions	= 1 << 2,
		ViewingSubmissions				= 1 << 3,
		ChangingRights					= 1 << 4,
	}

	[Flags]
	public enum ContestRights
	{
		ViewingClosed					= 1,
		Editing							= 1 << 1,
		ViewingAndAnsweringQuestions	= 1 << 2,
		ViewingSubmissions				= 1 << 3,
		ChangingRights					= 1 << 4,

		AddingProblems					= 1 << 5,
		RegisteringUsers				= 1 << 6
	}

	[Flags]
	public enum SystemRights
	{
		ViewingClosedContests			= 1,
		EditingContests					= 1 << 1,
		ViewingAndAnsweringQuestions	= 1 << 2,
		ViewingSubmissions				= 1 << 3,
		ChangingRightsOnContests		= 1 << 4,
		AddingProblems					= 1 << 5,
		RegisteringUsers				= 1 << 6,

		EditingUsers					= 1 << 7,
		ChangingRightsOnUsers			= 1 << 8
	}
	
	/*[Flags]
	public enum ProblemRights
	{
		InheritViewingClosed,
		ViewingClosed
		
		
		AllowViewingClosed					= 1,
		AllowEditing						= 1 << 1,
		AllowViewingAndAnsweringQuestions	= 1 << 2,
		AllowViewingSubmissions				= 1 << 3,
		AllowChangingRights					= 1 << 4,

		DenyViewingClosed					= 1 << 5,
		DenyEditing							= 1 << 6,
		DenyViewingAndAnsweringQuestions	= 1 << 7,
		DenyViewingSubmissions				= 1 << 8,
		DenyChangingRights					= 1 << 9
	}*/
}
