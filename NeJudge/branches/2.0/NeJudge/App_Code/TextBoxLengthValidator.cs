using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.ComponentModel;

namespace Ne.Helpers
{
	[ToolboxData("<{0}:TextBoxLengthValidator runat=\"server\" ErrorMessage=\"TextBoxLengthValidator\"></{0}:TextBoxLengthValidator>")]
	public sealed class TextBoxLengthValidator : BaseValidator
	{
		#region Properties
		
		[Themeable(false), DefaultValue(0)]
		public int MinimalLength
		{
			get
			{
				if( ViewState["MinimalLength"] != null )
					return (int)ViewState["MinimalLength"];
				return 0;
			}
			set
			{
				if( value < 0 )
					throw new ArgumentException("MinimalLength must be non-negative integer", "value");
				ViewState["MinimalLength"] = value;
			}
		}

		[Themeable(false), DefaultValue(0)]
		public int MaximalLength
		{
			get
			{
				if( ViewState["MaximalLength"] != null )
					return (int)ViewState["MaximalLength"];
				return 0;
			}
			set
			{
				if( value < 0 )
					throw new ArgumentException("MaximalLength must be non-negative integer", "value");
				if( value < MinimalLength )
					throw new ArgumentException("MaximalLength must be greater than or equal to MinimalLength", "value");
				ViewState["MaximalLength"] = value;
			}
		}

		[Themeable(false), DefaultValue(true)]
		public bool TrimValueBeforeValidation
		{
			get
			{
				if( ViewState["TrimValueBeforeValidation"] != null )
					return (bool)ViewState["TrimValueBeforeValidation"];
				return false;
			}
			set	{ ViewState["TrimValueBeforeValidation"] = value; }
		}

		[Themeable(false), DefaultValue(true)]
		public string LengthLessThanMinimalLengthErrorMessage
		{
			get
			{
				if( ViewState["LengthLessThanMinimalLengthErrorMessage"] != null )
					return ViewState["LengthLessThanMinimalLengthErrorMessage"].ToString();
				return "Length must be greater than or equal to {0}";
			}
			set { ViewState["LengthLessThanMinimalLengthErrorMessagen"] = value; }
		}

		[Themeable(false), DefaultValue(true)]
		public string LengthGreaterThanMaximalLengthErrorMessage
		{
			get
			{
				if( ViewState["LengthGreaterThanMaximalLengthErrorMessage"] != null )
					return ViewState["LengthGreaterThanMaximalLengthErrorMessage"].ToString();
				return "Length must be less than or equal to {0}";
			}
			set { ViewState["LengthGreaterThanMaximalLengthErrorMessagen"] = value; }
		}

		private new string ErrorMessage
		{
			get { return base.ErrorMessage; }
			set { base.ErrorMessage = value; }
		}

		#endregion

		#region Methods

		protected override void AddAttributesToRender(HtmlTextWriter writer)
		{
			base.AddAttributesToRender(writer);
			if( RenderUplevel )
			{
				
				/*writer, ClientID, "evaluationfunction", "TextBoxLengthValidatorEvaluateIsValid", false);
				writer, ClientID, "maximumlength", MinimalLength.ToString());
				writer, ClientID, "minimumlength", MaximalLength.ToString());
				writer, ClientID, "lengthlessthanminimallengtherrormessage", LengthLessThanMinimalLengthErrorMessage);
				writer, ClientID, "lengthgreaterthanmaximallengtherrormessage", LengthGreaterThanMaximalLengthErrorMessage);
				writer, ClientID, "trimvaluebeforevalidation", TrimValueBeforeValidation.ToString().ToLowerInvariant(), false);*/
			}
		}

		protected override bool EvaluateIsValid()
		{
			string value = GetControlValidationValue(ControlToValidate);
			if( value == null )
				return true;
			if( TrimValueBeforeValidation )
				value = value.Trim();
			if( value.Length < MinimalLength )
			{
				ErrorMessage = string.Format(LengthLessThanMinimalLengthErrorMessage, MinimalLength);
				return false;
			}
			if( value.Length > MaximalLength )
			{
				ErrorMessage = string.Format(LengthGreaterThanMaximalLengthErrorMessage, MaximalLength);
				return false;
			}
			return true;
		}

		protected override bool ControlPropertiesValid()
		{
			string str = base.ControlToValidate;
			if( str.Length > 0 )
				CheckControlValidationProperty(str, "ControlToValidate");
			return true;
		}

		#endregion
	}
}