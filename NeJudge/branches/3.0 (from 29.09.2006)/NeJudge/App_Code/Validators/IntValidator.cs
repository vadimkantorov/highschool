using System;
using System.Web;

namespace Ne.Helpers
{
	public class IntValidator : BaseQueryStringFieldValidator<int>
	{
		public IntValidator(string field)
			: base(field)
		{ }

		protected override void Validate(string fieldData)
		{
			if ( !int.TryParse(fieldData, out validatedValue) )
				throw new InvalidQueryStringParameterException(field);
		}
	}
}