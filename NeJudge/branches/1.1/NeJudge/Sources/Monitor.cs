using System;
using System.Collections;
using System.Data;
using System.Web;
using Ne.Database;

namespace Ne.Judge
{
	public class Monitor
	{
		public class SubmissionStatus
		{
			public int SID;
			public bool Accepted;
			public DateTime Time;

			public SubmissionStatus(int sid, bool ac, DateTime dt)
			{
				SID = sid;
				Accepted = ac;
				Time = dt;
			}
		}

		public class ProblemSubmissions
		{
			public ArrayList Submissions;
			public DateTime AcTime;
			public int Penalty;
			public int Result;

			public ProblemSubmissions()
			{
				Submissions = new ArrayList();
				AcTime = new DateTime();
				Result = 0;
			}
		}

		public class UserData
		{
			private Contest _con;
			private int _uid;

			public ProblemSubmissions[] Problems;
			public int Solved;
			public int Time;

			public int UID
			{
				get { return _uid; }
			}

			public void AddSubmission(int probIndex, SubmissionStatus subm)
			{
				Problems[probIndex].Submissions.Add(subm);
				UpdateProblem(probIndex);
			}

			public void ReplaceSubmission(int probIndex, SubmissionStatus subm)
			{
				foreach ( SubmissionStatus s in Problems[probIndex].Submissions )
				{
					if ( s.SID == subm.SID )
					{
						subm = s;
						break;
					}
				}
				UpdateProblem(probIndex);
			}

			public UserData(int probCount, Contest con, int uid)
			{
				Problems = new ProblemSubmissions[probCount];
				for ( int i = 0; i < Problems.Length; i++ )
					Problems[i] = new ProblemSubmissions();
				Solved = 0;
				Time = 0;
				_con = con;
				_uid = uid;
			}

			private void UpdateProblem(int probIndex)
			{
				Problems[probIndex].Result = 0;
				Problems[probIndex].Penalty = 0;
				foreach ( SubmissionStatus s in Problems[probIndex].Submissions )
				{
					if ( Problems[probIndex].Result <= 0 )
					{
						if ( s.Accepted )
						{
							Problems[probIndex].AcTime = s.Time;
							Problems[probIndex].Result = -Problems[probIndex].Result+1;
							break;
						}
						else
						{
							Problems[probIndex].Result--;
							Problems[probIndex].Penalty++;
						}
					}
				}
				Update();
			}

			private void Update()
			{
				Solved = 0;
				Time = 0;
				for ( int i = 0; i < Problems.Length; i++ )
				{
					if ( Problems[i].Result > 0 )
					{
						Solved++;
						Time += (int)TimeUtils.ZeroTimeSpan(
							Problems[i].AcTime - TimeUtils.ZeroDateTime(_con.Beginning)
							).TotalMinutes;
						Time += Problems[i].Penalty * 20;
					}
				}
			}
		}

		private ArrayList _users;
		private int[] _prob_nums;
		private string[] _short_names;
		private int[] _solved_count;
		private double[] _solved_percent;
		private BaseDb _db;
		private Contest _con;
		private int _tid;
		private LastAcceptedData _last_ac;
		private bool _has_ac;

		public LastAcceptedData LastAccepted
		{
			get { return _last_ac; }
		}

		public bool HasAccepted
		{
			get { return _has_ac; }
		}

		public Monitor(int tid)
		{
			_tid = tid;
			_db = DbFactory.ConstructDatabase();
			_con = _db.GetContest(_tid);
			_users = new ArrayList();
			_last_ac = new LastAcceptedData();
			_has_ac = false;
			gen_monitor();
		}

		public void Reload()
		{
			gen_monitor();
		}

		public void UpdateSubmission(Submission s, bool addNew)
		{
			UserData ud = null;
			foreach ( UserData u in _users )
			{
				if ( u.UID == s.UID )
				{
					ud = u;
					break;
				}
			}
			if ( ud == null )
			{
				ud = new UserData(_prob_nums.Length, _con, s.UID);
				_users.Add(ud);
			}
			int pind = -1;
			for ( int i = 0; i < _prob_nums.Length; i++ )
			{
				if ( _prob_nums[i] == s.PID )
				{
					pind = i;
					break;
				}
			}
			if ( pind == -1 )
				throw new NeJudgeException("Внутренняя ошибка монитора: pid не найден");
			SubmissionStatus st = new SubmissionStatus(s.SID, s.Result.Code == Result.AC, s.Time);
			if ( addNew )
				ud.AddSubmission(pind, st);
			else
				ud.ReplaceSubmission(pind, st);
			_users.Sort(new UserCompare());
			if ( s.Result.Code == Result.AC )
			{
				_has_ac = true;
				if ( s.Time > LastAccepted.Time )
				{
					_last_ac.Time = s.Time;
					_last_ac.ProblemShortName = _short_names[pind];
				}
			}
		}

		public DataTable FormatMonitor()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("ID");
			dt.Columns.Add("Имя участника");
			foreach ( string s in _short_names )
			{
				dt.Columns.Add(s);
			}
			dt.Columns.Add("Всего решено");
			dt.Columns.Add("Время");
			dt.Columns.Add("Место");

			UserCompare uc = new UserCompare();
			int count = 1;
			init_db();
			for ( int i = 0; i < _users.Count; i++ )
			{
				DataRow dr = dt.NewRow();
				UserData ud = (UserData)_users[i];
				dr[0] = ud.UID;
				dr[1] = _db.GetUser(ud.UID).Fullname;
				for ( int j = 0; j < _prob_nums.Length; j++ )
				{
					if ( ud.Problems[j].Result > 0)
					{
						// Write a result to the problem's column
						dr[2+j] = "<span style='color:blue;'>+";
						if ( ud.Problems[j].Result != 1 )
						{
							dr[2+j] += (ud.Problems[j].Result - 1).ToString();
						}
						dr[2+j] += "</span><span style='font-size:smaller;display:block;'>(";
						dr[2+j] += HtmlFunctions.BeautifyTimeSpan(
							TimeUtils.ZeroTimeSpan(ud.Problems[j].AcTime - _con.Beginning), true);
						dr[2+j] += ")</span>";
					}
					else if ( ud.Problems[j].Result < 0 )
					{
						dr[2+j] = "<span style='color:red;'>";
						dr[2+j] += ud.Problems[j].Result.ToString();
						dr[2+j] += "</span>";
					}
					else
					{
						dr[2+j] = "";
					}
				}
				dr[dr.Table.Columns.Count-3] = ud.Solved;
				dr[dr.Table.Columns.Count-2] = ud.Time;
				if ( i > 0 && uc.Compare(_users[i], _users[i-1]) != 0 )
					count++;
				dr[dr.Table.Columns.Count-1] = count;
				dt.Rows.Add(dr);
			}
			close_db();
			return dt;
		}

		public DataTable FormatStats()
		{
			DataTable dt = new DataTable();
			dt.Columns.Add("Показатель");
			foreach ( string s in _short_names )
			{
				dt.Columns.Add(s);
			}
			DataRow solved = dt.NewRow();
			DataRow percent = dt.NewRow();
			solved[0] = "Команд решили";
			percent[0] = "Процент успешных сдач";
			for ( int i = 0; i < _prob_nums.Length; i++ )
			{
				solved[1+i] = _solved_count[i];
				double ac_percent = (_solved_count[i] == 0)
					? 0 : (double)_solved_count[i] / (double)_users.Count * 100.0;
				percent[1+i] = String.Format("{0:F2}%", ac_percent);
			}
			dt.Rows.Add(solved);
			dt.Rows.Add(percent);
			return dt;
		}

		private class UserCompare : IComparer
		{
			public int Compare(object x, object y)
			{
				UserData dx = x as UserData;
				UserData dy = y as UserData;
				if ( dx == null || dy == null )
				{
					return 0;
				}
				if (dx.Solved > dy.Solved)
				{
					return -1;
				}
				else if (dx.Solved < dy.Solved)
				{
					return 1;
				}
				else
				{
					if (dx.Time > dy.Time)
					{
						return 1;
					}
					else if (dx.Time < dy.Time)
					{
						return -1;
					}
					else
					{
						return 0;
					}
				}
			}
		}
	
		private void gen_monitor()
		{
			init_db();

			ArrayList ps = _db.GetProblems(_tid);
			_users = new ArrayList();
			_prob_nums = new int[ps.Count];
			_short_names = new string[ps.Count];
			_solved_count = new int[ps.Count];
			_solved_percent = new double[ps.Count];

			for ( int i = 0; i < ps.Count; i++ )
			{
				Problem p = ps[i] as Problem;
				_prob_nums[i] = p.PID;
				_short_names[i] = p.ShortName;
			}

			foreach ( Submission s in _db.GetSubmissions(_tid, 0) )
			{
				UpdateSubmission(s, true);
			}
			_users.Sort(new UserCompare());

			close_db();

			for ( int i = 0; i < _prob_nums.Length; i++ )
			{
				foreach ( UserData ud in _users )
				{
					if ( ud.Problems[i].Result > 0 )
						_solved_count[i]++;	
				}
				_solved_percent[i] = (double)_solved_count[i] / _users.Count;
			}
		}

		private void close_db()
		{
			_db.Close();
		}

		private void init_db()
		{
			if ( _db == null )
				_db = DbFactory.ConstructDatabase();
			else if ( _db.Connection.State != ConnectionState.Open )
				_db.Open();
		}
	}

	public class LastAcceptedData
	{
		public string ProblemShortName;
		public DateTime Time = new DateTime(1, 1, 1);
	}

	public class MonitorManager
	{
		private static string _uniq_key = "monitor__";

		public static Monitor GetMonitor(int tid, HttpContext con)
		{
			string key = _uniq_key + tid.ToString();
			lock ( con.Cache )
			{
				if ( con.Cache[key] == null )
				{
					Monitor cm = new Monitor(tid);
					con.Cache[key] = cm;
				}
			}
			return (Monitor)con.Cache[key];
		}
	}
}
