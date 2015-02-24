using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

using Ne.Database.Classes;
using Ne.ContestTypeHandlers;
using Ne.Helpers;

namespace ICPCHandler
{
	public class ICPCMonitorManager : IMonitorManager
	{
		struct LastAcceptedSubmission
		{
			public DateTime Time;
			public string ShortName;
		}

		bool has_ac;
		LastAcceptedSubmission lastAc;
		Contest contest;

		public DataTable[] Build(int contestID)
		{
			contest = Contest.GetContest(contestID);
			data = new Dictionary<string, UserData>();
			SubmissionsFilter f = new SubmissionsFilter(contestID);
			foreach ( Submission s in Submission.GetSubmissions(f) )
				if ( Problem.GetProblem(s.ProblemID).ContestID == contestID )
					ProcessSubmission(s);
			DataTable[] dts = new DataTable[] {FormatMonitor(contest), FormatStatistics(contest)};
			dts[0].TableName = "Монитор";
			dts[1].TableName = "Статистика";
			return dts;
		}

		public void PaintDataGrid(DataGrid dg, int index, int solvedIndex)
		{
			if ( index == 0 )
			{
				if ( dg.Items.Count != 0 )
					dg.Items[0].CssClass = "gridLight";
				for ( int i = 1; i < dg.Items.Count; i++ )
				{
					if ( dg.Items[i].Cells[solvedIndex].Text != 
						dg.Items[i - 1].Cells[solvedIndex].Text )
					{
						if ( dg.Items[i - 1].CssClass == "gridLight" )
							dg.Items[i].CssClass = "gridDark";
						else
							dg.Items[i].CssClass = "gridLight";
					}
					else
						dg.Items[i].CssClass = dg.Items[i - 1].CssClass;
				}
			}
			else
				Ne.Helpers.HtmlFunctions.BeautifyDataGrid(dg);
		}


		class UserData : IComparable<UserData>
		{
			public class ProblemInfo
			{
				public int Result = 0;
				public DateTime AcTime;
			}

			public int Penalty;
			public int SolvedCount;
			public string DisplayName;
			public Dictionary<int, ProblemInfo> ProblemsInfo;

			public UserData(string userID, int contestID)
			{
				DisplayName = User.GetUser(userID).Name;
				ProblemsInfo = new Dictionary<int, ProblemInfo>();
				foreach ( Problem p in Problem.GetProblems(contestID) )
					ProblemsInfo.Add(p.ID, new ProblemInfo());
			}

			#region IComparable<UserData> Members

			public int CompareTo(UserData other)
			{
				if ( SolvedCount != other.SolvedCount )
					return (SolvedCount > other.SolvedCount) ? -1 : 1;
				if ( Penalty != other.Penalty )
					return (Penalty < other.Penalty) ? -1 : 1;
				if ( DisplayName != other.DisplayName )
					return DisplayName.CompareTo(other.DisplayName);
				return 0;
			}

			#endregion
		}

		Dictionary<string, UserData> data;

		void ProcessSubmission(Submission s)
		{
			User u = User.GetUser(s.UserID);
			Contest c = Contest.GetContest(Problem.GetProblem(s.ProblemID).ContestID);
			ProcessSubmission(s, u, new ContestRegistration() /*u.GetRegistration(c.ID)*/); //TODO:очевидно
		}

		void ProcessSubmission(Submission s, User u, ContestRegistration cr)
		{
			if ( !u.IsInvisible /* && !cr.IsInvisible*/ )
			{
				if ( data.ContainsKey(s.UserID) )
				{
					UserData ud = data[s.UserID];
					UserData.ProblemInfo pi = ud.ProblemsInfo[s.ProblemID];
					
					if ( s.Outcome == ICPCOutcomeManager.Accepted )
					{
						if ( pi.Result <= 0 )
						{
							// вычисление попыток
							pi.Result = -pi.Result + 1;
							ud.SolvedCount++;
							// начисление времени с учетом штрафа
							ud.Penalty = (pi.Result - 1)*20;
							ud.Penalty += (int) (s.Time - contest.Beginning).TotalMinutes;
							//FIXME: ?

							pi.AcTime = s.Time;
							pi.AcTime = TimeUtils.ZeroDateTime(pi.AcTime);

							//статистика
							has_ac = true;
							if ( lastAc.Time < pi.AcTime )
							{
								lastAc.Time = pi.AcTime;
								lastAc.ShortName = Problem.GetProblem(s.ProblemID).ShortName;
							}
						}
					}
					else if ( s.Outcome != OutcomeManager.Waiting && s.Outcome != OutcomeManager.Running )
					{
						if ( pi.Result <= 0 )
						{
							pi.Result--;
						}
					}
				}
				else
				{
					data.Add(s.UserID, new UserData(s.UserID, contest.ID));
					ProcessSubmission(s, u, cr);
				}
			}
		}

		DataTable FormatMonitor(Contest con)
		{
			List<int> pids = new List<int>();

			DataTable mon = new DataTable();
			mon.Columns.Add("Имя участника");
			foreach ( Problem p in Problem.GetProblems(con.ID) )
			{
				mon.Columns.Add(p.ShortName);
				pids.Add(p.ID);
			}
			mon.Columns.Add("Всего решено");
			mon.Columns.Add("Время");
			mon.Columns.Add("Место");

			List<UserData> uds = new List<UserData>(data.Values);
			uds.Sort();

			for ( int i = 0; i < uds.Count; i++ )
			{
				DataRow dr = mon.NewRow();
				dr["Имя участника"] = uds[i].DisplayName;
				for ( int j = 0; j < pids.Count; j++ )
				{
					if ( uds[i].ProblemsInfo[pids[j]].Result > 0 )
					{
						// Write a result to the problem's column
						dr[1 + j] = "<span style='color:blue;'>+";
						if ( uds[i].ProblemsInfo[pids[j]].Result != 1 )
						{
							dr[1 + j] += (uds[i].ProblemsInfo[pids[j]].Result - 1).ToString();
						}
						dr[1 + j] += "</span><span style='font-size:smaller;display:block;'>(";
						dr[1 + j] += TimeUtils.BeautifyTimeSpan(
							TimeUtils.ZeroTimeSpan(uds[i].ProblemsInfo[pids[j]].AcTime - con.Beginning), true);
						dr[1 + j] += ")</span>";
					}
					else if ( uds[i].ProblemsInfo[pids[j]].Result < 0 )
					{
						dr[1 + j] = "<span style='color:red;'>";
						dr[1 + j] += uds[i].ProblemsInfo[pids[j]].Result.ToString();
						dr[1 + j] += "</span>";
					}
					else
					{
						dr[1 + j] = "";
					}
				}

				dr["Всего решено"] = uds[i].SolvedCount;
				dr["Время"] = uds[i].Penalty;
				dr["Место"] = i + 1;
				mon.Rows.Add(dr);
			}
			return mon;
		}

		DataTable FormatStatistics(Contest con)
		{
			List<int> pids = new List<int>();

			DataTable dt = new DataTable();
			dt.Columns.Add("Показатель");
			foreach ( Problem p in Problem.GetProblems(con.ID) )
			{
				dt.Columns.Add(p.ShortName);
				pids.Add(p.ID);
			}

			DataRow solved = dt.NewRow();
			DataRow percent = dt.NewRow();
			solved[0] = "Команд решили / команд всего";
			percent[0] = "Процент успешных попыток";
			List<double> solvedv = new List<double>(pids.Count);
			List<double> percentv = new List<double>(pids.Count);
			for ( int i = 0; i < pids.Count; i++ )
			{
				solvedv.Add(0);
				percentv.Add(0);
			}
			foreach ( UserData ud in data.Values )
			{
				for ( int i = 0; i < pids.Count; i++ )
				{
					if ( ud.ProblemsInfo[pids[i]].Result > 0 )
						solvedv[i]++;

					percentv[i] += Math.Abs(ud.ProblemsInfo[pids[i]].Result);
				}
			}

			for ( int i = 0; i < pids.Count; i++ )
			{
				if ( percentv[i] != 0 )
					percentv[i] = solvedv[i]/percentv[i];
				else
					percentv[i] = 0;
				if ( data.Values.Count != 0 )
					solvedv[i] /= data.Values.Count;
				percent[1 + i] = String.Format("{0:F2}%", percentv[i]*100);
				solved[1 + i] = String.Format("{0:F2}%", solvedv[i]*100);
			}
			dt.Rows.Add(solved);
			dt.Rows.Add(percent);
			return dt;
		}
	}
}