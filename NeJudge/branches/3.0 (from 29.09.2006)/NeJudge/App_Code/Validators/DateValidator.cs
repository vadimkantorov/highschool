using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Ne.Helpers
{
	public class DateValidator : BaseQueryStringFieldValidator<DateTime>
	{
		public DateValidator(string field) : base(field)
		{}

		protected override void Validate(string fieldData)
		{
			if ( !DateTime.TryParse(fieldData, out validatedValue) )
				throw new InvalidQueryStringParameterException(field);
		}
	}
}
