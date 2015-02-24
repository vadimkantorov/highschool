using System;
using System.Web;

namespace Ne.Helpers
{
	public class EnumValidator<U> : BaseQueryStringFieldValidator<U>
	{
		public EnumValidator(string field)
			: base(field)
		{
			if ( !typeof(U).IsSubclassOf(typeof(Enum)) )
				throw new InvalidOperationException(string.Format("Generic parameter 'U' must be enumeration (actually it was {0})",
					typeof(U).FullName));
		}

		protected override void Validate(string fieldData)
		{
			try
			{
				validatedValue = (U)Enum.Parse(typeof(U), fieldData, true);
			}
			catch ( ArgumentException )
			{
				throw new InvalidQueryStringParameterException(field);
			}
		}
	}
}