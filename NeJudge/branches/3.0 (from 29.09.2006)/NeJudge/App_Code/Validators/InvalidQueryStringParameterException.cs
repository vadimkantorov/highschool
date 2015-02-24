using System;

namespace Ne.Helpers
{
	public class InvalidQueryStringParameterException : Exception
	{
		string parameter;

		public string Parameter
		{
			get { return parameter; }
			set { parameter = value; }
		}

		public InvalidQueryStringParameterException() { }

		public InvalidQueryStringParameterException(string parameter)
			: base(string.Concat("The field ", parameter, " is invalid"))
		{
			this.parameter = parameter;
		}

		public InvalidQueryStringParameterException(string parameter, string message)
			: base(message)
		{
			this.parameter = parameter;
		}
	}
}
