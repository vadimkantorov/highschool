using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.ContestTypeHandlers
{
	public class ContestTypeAttribute : Attribute
	{
		public readonly string Type;

		public ContestTypeAttribute(string type)
		{
			Type = type;
		}
	}
}
