using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Ne.Helpers
{
	public class HtmlFunctions
	{
		public static void BeautifyGridView(GridView dt)
		{
			if ( dt.Rows.Count != 0 )
				dt.Rows[0].CssClass = "gridLight";
			for ( int i = 1; i < dt.Rows.Count; i++ )
				dt.Rows[i].CssClass = ( dt.Rows[i - 1].CssClass == "gridLight" ) ? "gridDark" : "gridLight";
		}

		public static void BeautifyDataGrid(DataGrid dt)
		{
			if ( dt.Items.Count != 0 )
				dt.Items[0].CssClass = "gridLight";
			for ( int i = 1; i < dt.Items.Count; i++ )
				dt.Items[i].CssClass = ( dt.Items[i - 1].CssClass == "gridLight" ) ? "gridDark" : "gridLight";
		}
	}

	public class TimeUtils
	{
		public static DateTime ZeroDateTime(DateTime t)
		{
			return new DateTime(t.Year , t.Month , t.Day , t.Hour , t.Minute , 0 , 0);
		}

		public static TimeSpan ZeroTimeSpan(TimeSpan t)
		{
			return new TimeSpan(t.Days , t.Hours , t.Minutes , 0 , 0);
		}

		public static string BeautifyTimeSpan(TimeSpan t, bool short_days)
		{
			string str = "";
			if ( t > new TimeSpan(0, 0, 1, 0, 0) )
			{
				if ( ( int ) t.TotalDays > 0 )
				{
					str += (( int ) t.TotalDays).ToString();
					str += (short_days) ? " д " : " дней ";
				}
				str += (( int ) t.Hours).ToString("D2");
				str += ":";
				str += (( int ) t.Minutes).ToString("D2");
			}
			else
			{
				str = String.Format("{0} сек", t.Seconds);
			}
			return str;
		}
	}
}
