using System;
using System.Web;

using Ne.Database.Classes;

namespace Ne.Helpers
{
	public class ContestIDValidator : BaseQueryStringFieldValidator<int>
	{
		public ContestIDValidator(string field)
			: base(field)
		{ }

		public ContestIDValidator()
			: base("contestID")
		{ }

		protected override void Validate(string fieldData)
		{
			if ( !int.TryParse(fieldData, out validatedValue) || !Contest.ValidateID(validatedValue) )
				throw new InvalidQueryStringParameterException(field);
		}
	}
}