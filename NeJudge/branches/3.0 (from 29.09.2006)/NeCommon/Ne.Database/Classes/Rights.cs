using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.Database.Classes
{
	[Flags]
	public enum SystemRights
	{
		None = 0,
		AllowViewingClosedContests = 1,
		DenyViewingClosedContests = 2,
		AllowEditingContests = 4,
		DenyEditingContests = 8,
		AllowViewingAndAnsweringQuestions = 16,
		DenyViewingAndAnsweringQuestions = 32,
		AllowViewingSources = 64,
		DenyViewingSources = 128,
		AllowGrantingOnContests = 256,
		DenyGrantingOnContests = 512,
		AllowSubmittingAndAskingQuestions = 1024,
		DenySubmittingAndAskingQuestions = 2048,

		AllowAddingProblem = 4096,
		DenyAddingProblem = 8192,
		AllowRegisteringUsers = 16384,
		DenyRegisteringUsers = 32768,
		
		AllowEditingUsers = 65536,
		DenyEditingUsers = 131072,
		AllowGrantingOnUsers = 262144,
		DenyGrantingOnUsers = 524288,
		AllowCreatingContests = 1048576,
		DenyCreatingContests = 2097152
		
	}

	[Flags]
	public enum ProblemRights
	{
		None = 0,
		AllowViewing = 1,
		DenyViewing = 2,
		AllowEditing = 4,
		DenyEditing = 8,
		AllowViewingAndAnsweringQuestions = 16,
		DenyViewingAndAnsweringQuestions = 32,
		AllowViewingSources = 64,
		DenyViewingSources = 128,
		AllowGranting = 256,
		DenyGranting = 512,
		AllowSubmittingAndAskingQuestions = 1024,
		DenySubmittingAndAskingQuestions = 2048,
	}

	[Flags]
	public enum ContestRights
	{
		None = 0,
		AllowViewing = 1,
		DenyViewing = 2,
		AllowEditing = 4,
		DenyEditing = 8,
		AllowViewingAndAnsweringQuestions = 16,
		DenyViewingAndAnsweringQuestions = 32,
		AllowViewingSources = 64,
		DenyViewingSources = 128,
		AllowGranting = 256,
		DenyGranting = 512,
		AllowSubmittingAndAskingQuestions = 1024,
		DenySubmittingAndAskingQuestions = 2048,
		
		AllowAddingProblem = 4096,
		DenyAddingProblem = 8192,
		AllowRegisteringUsers = 16384,
		DenyRegisteringUsers = 32768
		
	}

}