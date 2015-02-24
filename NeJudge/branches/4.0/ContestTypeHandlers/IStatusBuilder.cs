using System;
using System.Collections.Generic;
using System.Text;

using Ne.DomainModel;

namespace Ne.ContestTypeHandlers
{
	public interface IStatusBuilder
	{
		LightweightRow GetHeaders();
		LightweightRow GetInformation(int submissionID);
	}
}
