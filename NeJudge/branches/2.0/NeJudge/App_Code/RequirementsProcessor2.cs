using System;
using System.Collections.Generic;
using System.Web;

using Ne.Database.Classes;
using Ne.Judge;
using System.Collections.Specialized;

namespace Ne.Helpers
{
	public class RequirementsProcessor2
	{
		Type type;
		Dictionary<string, QueryStringFieldValidator> dict = new Dictionary<string, QueryStringFieldValidator>();

		public RequirementsProcessor2(Type type)
		{
			this.type = type;
		}

		public void ProcessRequirements()
		{
			foreach( QueryStringFieldValidator v in type.GetCustomAttributes(typeof(QueryStringFieldValidator), true) )
			{
				v.Validate(HttpContext.Current.Request.QueryString);
				dict.Add(v.Name, v);
			}
		}
	}

	public abstract class QueryStringFieldValidator : Attribute
	{
		protected string field;
		protected bool acceptParameterLack;
		protected bool parameterDefined;
		string name;
		
		public string Field
		{
			get { return field; }
			set { field = value; }
		}

		public string Name
		{
			get { return name; }
			set { name = value; }
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

		public abstract void Validate(NameValueCollection queryString);

		public QueryStringFieldValidator(string field, bool acceptParameterLack)
		{
			this.field = field;
			this.acceptParameterLack = acceptParameterLack;
		}
	}

	public class Int32Validator : QueryStringFieldValidator
	{
		Predicate<int> match;
		int validatedValue;

		public Int32Validator(string field, bool acceptParameterLack, Predicate<int> match)
			: base(field, acceptParameterLack)
		{
			this.match = match;
		}

		public override void Validate(NameValueCollection queryString)
		{
			if( HttpContext.Current.Request.QueryString[field] != null )
			{
				int t;
				if( int.TryParse(queryString[field], out t) )
				{
					if(match != null)
						if(match(t))
							validatedValue = t;
						else
							throw new NeJudgeInvalidParametersException(field);
				}
				else
					throw new NeJudgeInvalidParametersException(field);
			}
			else
			{
				if( acceptParameterLack )
					parameterDefined = false;
				else
					throw new NeJudgeInvalidParametersException(field);
			}
		}
	}
}