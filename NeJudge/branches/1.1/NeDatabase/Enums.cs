using System;

namespace Ne.Database
{
	public enum TableType
	{
		foredit,
		forset
	}

	public enum Result
	{
		AC,
		WA,
		CE,
		TLE,
		MLE,
		OLE,
		PE,
		CRASH,
		WAIT,
		FA,
		SV,
		RU
	}

	[Flags]
	public enum Role
	{
		/*//ADMIN
			CREATE_AND_EDIT_PROBLEMS,
			CREATE_AND_EDIT_CONTESTS,
			PROVIDE_USERS_WITH_CAPABILITIES,
			//JUDGE
			VIEW_STATUS_OF_ALL_SUBMISSIONS,
			ANSWER_AND_VIEW_REPORTS_OR_SOLUTIONS,
			//USER
			SUBMIT_AND_ASK*/
		Developer = 1,
		Administrator = 2,
		Judge = 4,
		User = 8,
		Anonymous = 16
	}

	public enum Language
	{
		cpp,
		pas,
		c
	}
}