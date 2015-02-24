using System;
using System.Collections.Generic;
using System.Text;

using Ne.Database.Classes;
using Ne.Interfaces;

namespace Ne.ContestTypeHandlers
{
	public interface IStatusBuilder
	{
		LightweightRow GetHeaders();
		LightweightRow GetInformation(int submissionID);
	}
}
