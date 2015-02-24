using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Ne.Interfaces;

namespace Ne.Helpers
{
	public static class LightweightTableConverter
	{
		static string FormatText(string text, InformationKind textKind, string positiveCssClass, string negativeCssClass)
		{
			if ( textKind == InformationKind.Neutral )
				return text;
			else
				return string.Format("<span class='{0}'>{1}</span>",
						textKind == InformationKind.Positive ? positiveCssClass : negativeCssClass,
						HttpUtility.HtmlEncode(text));
		}

		public static string CellToHtml(LightweightCell cell)
		{
			if ( cell is PlaintextCell )
			{
				PlaintextCell cur = (PlaintextCell) cell;
				return FormatText(cur.Text, cur.Kind, CssHelper.GenericPositiveCssClass, CssHelper.GenericNegativeCssClass);
			}
			else if ( cell is HyperlinkCell )
			{
				HyperlinkCell cur = (HyperlinkCell) cell;
				int id = cur.LinkObjectID;
				string url = string.Empty;

				switch ( cur.LinkType )
				{
					case HyperlinkCell.LinkObjectType.Contest:
						url = UrlRenderer.RenderContestUrl(id);
						break;
					case HyperlinkCell.LinkObjectType.Monitor:
						url = UrlRenderer.RenderMonitorUrl(id);
						break;
					case HyperlinkCell.LinkObjectType.Problem:
						url = UrlRenderer.RenderProblemUrl(id);
						break;
					case HyperlinkCell.LinkObjectType.Submission:
						url = UrlRenderer.RenderTestlogUrl(id);
						break;
					default:
						throw new ArgumentException(string.Format("Invalid link object type {0}", cur.LinkType));
				}

				return string.Format("<a href='{0}'>{1}</a>", HttpContext.Current.Request.MapPath(url), cur.Text);
			}
			else if ( cell is OutcomeInfoCell )
			{
				OutcomeInfoCell cur = (OutcomeInfoCell) cell;

				return FormatText(cur.Info.PrintableValue, cur.Info.Kind, CssHelper.GenericPositiveCssClass, 
					CssHelper.GenericNegativeCssClass);
			}
			else if ( cell is MonitorCell )
			{
				MonitorCell cur = (MonitorCell) cell;

				string ret = FormatText(cur.Text, cur.Kind, CssHelper.MonitorPositiveCssClass, 
					CssHelper.MonitorNegativeCssClass);

				if ( cur.ShowTime )
					ret += string.Format("<span class='{0}'>{1}</span>", CssHelper.TimeSpanCssClass, 
						TimeUtils.BeautifyTimeSpan(cur.Time, true));

				return ret;
			}
			else
				throw new ArgumentException(string.Format("Invalid cell type {0}", cell.GetType()));
		}

		static HtmlTableRow CreateHtmlRow(LightweightRow row, string cssClass)
		{
			HtmlTableRow ret = new HtmlTableRow();
			ret.Style.Add("class", cssClass);

			foreach ( LightweightCell cell in row )
			{
				HtmlTableCell htmlCell = new HtmlTableCell();
				htmlCell.InnerHtml = CellToHtml(cell);
				htmlCell.ColSpan = cell.ColSpan;
				ret.Cells.Add(htmlCell);
			}

			return ret;
		}

		public static HtmlTable ConvertToTable(LightweightTable table)
		{
			HtmlTable ret = new HtmlTable();

			ret.Rows.Add(CreateHtmlRow(table.HeaderRow, CssHelper.GridHeaderCssClass));

			foreach ( LightweightRow row in table.Rows )
				ret.Rows.Add(CreateHtmlRow(row, 
					row.IsAlternateStyle ? CssHelper.GridAlternateRowCssClass : CssHelper.GridRowCssClass));

			return ret;
		}
	}
}
