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
	public static class CssHelper
	{
		public const string GenericNegativeCssClass = "negative";
		public const string GenericPositiveCssClass = "positive";

		public const string MonitorNegativeCssClass = "monitorNegative";
		public const string MonitorPositiveCssClass = "monitorPositive";

		public const string TimeSpanCssClass = "monitorTime";

		public const string GridHeaderCssClass = "gridHeader";
		public const string GridRowCssClass = "gridRow";
		public const string GridAlternateRowCssClass = "gridAlternateRow";
	}
}
