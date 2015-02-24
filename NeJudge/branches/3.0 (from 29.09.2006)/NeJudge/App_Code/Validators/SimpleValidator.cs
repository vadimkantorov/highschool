using System;
using System.Web;

namespace Ne.Helpers
{
	public class SimpleValidator : BaseQueryStringFieldValidator<string>
	{
		public SimpleValidator(string field)
			: base(field)
		{ }

		protected override void Validate(string fieldData)
		{
			validatedValue = fieldData;
		}
	}
}