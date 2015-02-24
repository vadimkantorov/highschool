using System;
using System.Web;

using Ne.Database.Classes;

namespace Ne.Helpers
{
	public class ProblemIDValidator : BaseQueryStringFieldValidator<int>
	{
		public ProblemIDValidator(string field)
			: base(field)
		{ }

		public ProblemIDValidator()
			: base("problemID")
		{ }

		protected override void Validate(string fieldData)
		{
			if ( !int.TryParse(fieldData, out validatedValue) || !Problem.ValidateID(validatedValue) )
				throw new InvalidQueryStringParameterException(field);
		}
	}
}