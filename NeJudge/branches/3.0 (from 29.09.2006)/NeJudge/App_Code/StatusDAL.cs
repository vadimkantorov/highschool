using System;
using System.Data;
using System.Web;

using Ne.Database.Classes;
#if PHONY
namespace Ne.Helpers
{
	public class StatusDataProvider
	{
		public static DataTable GetRows(int maximumRows, int startRowIndex, object filter)
		{
			SubmissionsFilter filt = (SubmissionsFilter) filter;
			int cid = filt.ContestID;
			filt.From = startRowIndex + 1; //( page - 1 ) * statusGrid.PageSize + 1;
			filt.To = startRowIndex + maximumRows; //page * statusGrid.PageSize;

			DataTable dt = new DataTable();
			dt.Columns.Add("ID");
			dt.Columns.Add("Время");
			dt.Columns.Add("Команда");
			dt.Columns.Add("Задача");
			dt.Columns.Add("Язык");
			dt.Columns.Add("Статус");
			
			Contest con = Contest.GetContest(cid);
			ContestTypeHandler h = Factory.GetHandlerInstance(con.Type);
			string[] headers = h.StatusManager.GetHeaders();
			foreach ( string s in headers )
				dt.Columns.Add(s);
			foreach ( Submission s in Submission.GetSubmissions(filt) )
			{
				DataRow dr = dt.NewRow();
				dr["ID"] = s.ID;
				dr["Время"] = TimeUtils.BeautifyTimeSpan(s.Time - con.Beginning, true);
				dr["Команда"] = User.GetUser(s.UserID).Name;
				dr["Задача"] = String.Format("<a href='{0}'>{1}</a>", UrlRenderer.RenderProblemUrl(s.ProblemID),
				                             Problem.GetProblem(s.ProblemID).ShortName);
				dr["Язык"] = Language.GetLanguage(s.LanguageID).Name;
				dr["Статус"] = h.OutcomeManager.GetPrintableValue(s.Outcome);

				string[] ins = h.StatusManager.GetInfo(s);
				for ( int i = 0; i < headers.Length; i++ )
					dr[i+6] = ins[i];
				dt.Rows.Add(dr);
			}
			return dt;
		}

		public static int GetTotalNumberOfRows(object filter)
		{
			return Submission.GetSubmissionsCount((SubmissionsFilter) filter);
		}
	}
}
#endif