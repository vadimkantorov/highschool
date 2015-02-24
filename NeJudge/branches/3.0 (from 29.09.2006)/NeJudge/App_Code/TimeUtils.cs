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
	public static class TimeUtils
	{
		public static DateTime ZeroDateTime(DateTime time)
		{
			return new DateTime(time.Year, time.Month, time.Day, time.Hour, time.Minute, 0, 0);
		}

		public static TimeSpan ZeroTimeSpan(TimeSpan time)
		{
			return new TimeSpan(time.Days, time.Hours, time.Minutes, 0, 0);
		}

		public static string BeautifyTimeSpan(TimeSpan time, bool useShortDays)
		{
			time = ZeroTimeSpan(time);

			string ret = string.Empty;

			if ( time > TimeSpan.FromMinutes(1) )
			{
				if ( time.TotalDays > 0.0 )
					ret = string.Format("{0} {1}", time.Days, useShortDays ? "д" : "дней");
				ret += string.Format(" {0}:{1}", time.Hours, time.Minutes);
			}
			else
				ret = String.Format("{0} сек", time.Seconds);

			return ret;
		}
	}
}