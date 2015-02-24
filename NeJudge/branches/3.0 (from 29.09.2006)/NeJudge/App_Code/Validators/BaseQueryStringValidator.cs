using System;
using System.Web;

namespace Ne.Helpers
{
	public abstract class BaseQueryStringFieldValidator<T>
	{
		protected string field;
		protected bool acceptParameterLack = true;
		protected bool parameterDefined;
		protected T validatedValue;

		public string Field
		{
			get { return field; }
		}

		public bool AcceptParameterLack
		{
			get { return acceptParameterLack; }
			set { acceptParameterLack = value; }
		}

		public bool ParameterDefined
		{
			get { return parameterDefined; }
		}

		public T ValidatedValue
		{
			get { return validatedValue; }
		}

		protected abstract void Validate(string fieldData);

		public BaseQueryStringFieldValidator(string field)
		{
			this.field = field;
		}

		public void Process()
		{
			if ( HttpContext.Current.Request.QueryString[field] != null )
			{
				parameterDefined = true;
				Validate(HttpContext.Current.Request.QueryString[field]);
			}
			else if ( !acceptParameterLack )
				throw new InvalidQueryStringParameterException(field);
		}
	}
}