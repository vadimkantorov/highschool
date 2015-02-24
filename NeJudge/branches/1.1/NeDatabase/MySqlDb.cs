using System;
using System.Collections;
using System.Data;
using System.Globalization;

namespace Ne.Database
{
	public class MySqlDb : BaseDb
	{
		public MySqlDb(string connect) : base(connect)
		{
			conn = new MySqlConnection(connect);
			Open();
		}

		//Методы, относящиеся к соревнования
		public override int AddContest(Contest t)
		{
			string insert = "INSERT INTO Contests (Beginning,Ending,Name) VALUES (?beg,?end,?name);" +
				"SELECT TID FROM Contests WHERE TID=LAST_INSERT_ID();";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?beg", t.Beginning.ToString("yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("?end", t.Ending.ToString("yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("?name", t.Name);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int tid = -1;
			try
			{
				tid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
			return tid;
		}

		public override void UpdateContest(Contest t, int tid)
		{
			string insert = "UPDATE Contests SET Beginning=?beg,Ending=?end,Name=?name WHERE TID=?tid";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?beg", t.Beginning.ToString("yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("?end", t.Ending.ToString("yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("?name", t.Name);
			comm.Parameters.Add("?tid", tid);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteScalar();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		public override Contest GetContest(int tid)
		{
			string select = "SELECT * FROM Contests WHERE TID=" + tid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			reader.Read();
			DateTime beg = (DateTime) reader["Beginning"];
			DateTime end = (DateTime) reader["Ending"];
			string name = (string) reader["Name"];
			reader.Close();
			return new Contest(beg, end, name);
		}

		public override int GetTid(int pid)
		{
			string select = "SELECT TID FROM Problems WHERE PID=" + pid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckTid(int tid)
		{
			string select = "SELECT TID FROM Contests WHERE TID=" + tid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		public override int[] GetTids()
		{
			string select = "SELECT TID FROM Contests ORDER BY TID ASC"; //dd.MM.yy hh:mm:ss
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			ArrayList arr = new ArrayList();
			while (reader.Read())
				arr.Add((int) reader["TID"]);
			reader.Close();
			int[] ret = (int[]) Array.CreateInstance(typeof (int), arr.Count);
			arr.CopyTo(ret);
			return ret;
		}

		public override int[] GetOldTids()
		{
			string select = "SELECT TID FROM Contests WHERE Ending < NOW()"; //dd.MM.yy hh:mm:ss
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			ArrayList arr = new ArrayList();
			while (reader.Read())
				arr.Add((int) reader["TID"]);
			reader.Close();
			int[] ret = (int[]) Array.CreateInstance(typeof (int), arr.Count);
			arr.CopyTo(ret);
			return ret;
		}

		public override int[] GetNowTids()
		{
			string select = "SELECT TID FROM Contests WHERE Beginning < NOW() AND Ending >= NOW()";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			ArrayList arr = new ArrayList();
			while (reader.Read())
				arr.Add((int) reader["TID"]);
			reader.Close();
			int[] ret = (int[]) Array.CreateInstance(typeof (int), arr.Count);
			arr.CopyTo(ret);
			return ret;
		}

		public override int[] GetFutureTids()
		{
			string select = "SELECT * FROM Contests WHERE Beginning > NOW()";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			ArrayList arr = new ArrayList();
			while (reader.Read())
				arr.Add((int) reader["TID"]);
			reader.Close();
			int[] ret = (int[]) Array.CreateInstance(typeof (int), arr.Count);
			arr.CopyTo(ret);
			return ret;
		}

		/*public override bool Old(int tid)
		{
			string select = String.Format("SELECT * FROM Contests WHERE TID={0} AND Ending < DateValue('{1}');",tid,DateTime.Now.ToString("dd.MM.yy hh:mm:ss"));
			MySqlCommand comm = new MySqlCommand(select,(MySqlConnection)conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if(reader.Read())
			{
				reader.Close();
				return true;
			}
			reader.Close();
			return false;
		}*/


		//Методы, относящиеся к задачам
		public override DataTable GetTable(TableType t, params object[] args)
		{
			DataTable dt = new DataTable();
			string select;
			MySqlDataAdapter da;
			switch (t)
			{
				case TableType.foredit:
					select = "SELECT PID,Name,CONCAT('../editproblem.aspx?pid=',PID) AS Url,+LEFT(Text,100)+'...' AS 'Text',ShortName FROM Problems WHERE TID=" + args[0];
					da = new MySqlDataAdapter(select, (MySqlConnection) conn);
					dt.TableName = "Задачи";
					da.Fill(dt);
					break;
				case TableType.forset:
					select = "SELECT Name,PID,CONCAT('../problem.aspx?pid=',PID) AS Url,ShortName FROM Problems WHERE TID=" + args[0];
					da = new MySqlDataAdapter(select, (MySqlConnection) conn);
					dt.TableName = "Задачи";
					da.Fill(dt);
					break;
			}
			return dt;
		}

		public override int GetPid(string problemname)
		{
			string select = "SELECT PID FROM Problems WHERE Name='" + problemname + "';";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckPid(int pid)
		{
			string select = "SELECT PID FROM Problems WHERE PID=" + pid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		public override ArrayList GetProblems(int tid)
		{
			ArrayList arr = new ArrayList();
			string select = "SELECT * FROM Problems WHERE TID=" + tid + " ORDER BY PID ASC";
			//string select = "SELECT PID FROM Problems WHERE TID="+tid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			/*while(reader.Read())
				arr.Add(GetProblem((int)reader["PID"]));*/
			while (reader.Read())
				arr.Add(new Problem((string) reader["Name"], (string) reader["Text"],
				                    (string) reader["InputFormat"], (string) reader["OutputFormat"], (string) reader["InputSample"], (string) reader["OutputSample"],
				                    (string) reader["ShortName"], (string) reader["Author"], (int) reader["PID"], (int) reader["TID"]));
			reader.Close();
			return arr;
		}

		public override Problem GetProblem(int pid)
		{
			string select = "SELECT * FROM Problems WHERE PID=" + pid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			Problem p = null;
			if (reader.Read())
				p = new Problem((string) reader["Name"], (string) reader["Text"],
				                (string) reader["InputFormat"], (string) reader["OutputFormat"], (string) reader["InputSample"], (string) reader["OutputSample"],
				                (string) reader["ShortName"], (string) reader["Author"], pid, (int) reader["TID"]);

			reader.Close();
			return p;
		}

		public override int AddProblem(Problem p)
		{
			string insert =
				"INSERT INTO Problems (TID,Name,Text,InputFormat,OutputFormat,InputSample,OutputSample,ShortName,Author)" +
					"VALUES (?tid,?name,?text,?iform,?oform,?isamp,?osamp,?sname,?auth);" +
					"SELECT PID FROM Problems WHERE PID=LAST_INSERT_ID();";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?tid", p.TID);
			comm.Parameters.Add("?name", p.Name);
			comm.Parameters.Add("?text", p.Text);
			comm.Parameters.Add("?iform", p.InputFormat);
			comm.Parameters.Add("?oform", p.OutputFormat);
			comm.Parameters.Add("?isamp", p.InputSample);
			comm.Parameters.Add("?osamp", p.OutputSample);
			comm.Parameters.Add("?sname", GetNextShortName(p.TID));
			comm.Parameters.Add("?auth", p.Author);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int pid = -1;
			try
			{
				pid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
			return pid;
		}

		public override void UpdateProblem(Problem p)
		{
			string update = "UPDATE Problems SET TID=?tid,Name=?name,Text=?text,InputFormat=?iform" +
				",OutputFormat=?oform,InputSample=?isamp,OutputSample=?osamp,Author=?auth WHERE PID=" + p.PID;
			MySqlCommand comm = new MySqlCommand(update, (MySqlConnection) conn);
			comm.Parameters.Add("?tid", p.TID);
			comm.Parameters.Add("?name", p.Name);
			comm.Parameters.Add("?text", p.Text);
			comm.Parameters.Add("?iform", p.InputFormat);
			comm.Parameters.Add("?oform", p.OutputFormat);
			comm.Parameters.Add("?isamp", p.InputSample);
			comm.Parameters.Add("?osamp", p.OutputSample);
			comm.Parameters.Add("?auth", p.Author);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		public override void RemoveProblem(int pid)
		{
			string delete = "DELETE FROM Problems WHERE PID=" + pid;
			MySqlCommand comm = new MySqlCommand(delete, (MySqlConnection) conn);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch
			{
				trans.Rollback();
			}
		}

		public override string GetNextShortName(int tid)
		{
			string select = "SELECT ShortName FROM Problems WHERE TID=" + tid + " ORDER BY PID DESC LIMIT 1";
			MySqlConnection co = new MySqlConnection(Config.SqlConnectionString);
			co.Open();
			MySqlCommand comm = new MySqlCommand(select, co);
			MySqlDataReader reader = comm.ExecuteReader();
			string shortn = (reader.Read()) ? (string) reader["ShortName"] : "0";
			reader.Close();
			co.Close();
			char c;
			if (shortn.Length == 1)
			{
				if ((c = shortn[0]) == '0')
					return "A";
				else
					return (++c).ToString();
			}
			string news = shortn.Substring(0, shortn.Length - 2);
			c = shortn[shortn.Length - 1];
			if (Char.IsLetter(++c))
				news += c;
			else
			{
				news += --c;
				news += 'A';
			}
			return news;
		}

		//Методы, относящиеся к юзерам
		public override void AddUser(User u)
		{
			string insert = "INSERT INTO Users (Username,Password,Fullname,Mail,Role)" +
				" VALUES (?uname,?pass,?full,?mail,8)";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?uname", u.Username);
			comm.Parameters.Add("?pass", u.Password);
			comm.Parameters.Add("?full", u.Fullname);
			comm.Parameters.Add("?mail", u.Email);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		public override Role GetRoles(string name)
		{
			return Role.Anonymous;
		}

		public override void UpdateUser(User u, int uid)
		{
			string insert = "UPDATE Users SET Password=?pass,Fullname=?full,Mail=?mail" +
				" WHERE UID=?uid";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?uid", uid);
			comm.Parameters.Add("?pass", u.Password);
			comm.Parameters.Add("?full", u.Fullname);
			comm.Parameters.Add("?mail", u.Email);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		public override bool IsRegistered(string username)
		{
			if (username == "admin" || username == "anonymous" || username.Trim() == "")
				return true;
			string select = "SELECT Username FROM Users";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			while (reader.Read())
			{
				if (username == (string) reader["Username"])
				{
					reader.Close();
					return true;
				}
			}
			reader.Close();
			return false;
		}


		public override bool Authenticate(string username, string password)
		{
			string select = "SELECT Password FROM Users WHERE Username='" + username + "'";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
				if ((string) reader["Password"] == password)
				{
					reader.Close();
					return true;
				}
			reader.Close();
			return false;
		}

		public override int GetUid(string name)
		{
			if (name == "admin")
				return 0;
			string select = "SELECT UID FROM Users WHERE Username=?uname";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			comm.Parameters.Add("?uname", name);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckUid(int uid)
		{
			string select = "SELECT UID FROM Users WHERE UID=" + uid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		/*public override User[] GetUsers()
		{
			ArrayList arr = new ArrayList();
			string select = "SELECT FullName FROM Users";
			MySqlCommand comm = new MySqlCommand(select,(MySqlConnection)conn);
			MySqlDataReader reader = comm.ExecuteReader();
			while(reader.Read())
				arr.Add((string)reader["FullName"]);
			reader.Close();
			string[] ret = (string[])Array.CreateInstance(typeof(string),arr.Count);
			arr.CopyTo(ret);
			return ret;
		}*/

		public override User GetUser(int uid)
		{
			string select = "SELECT * FROM Users WHERE UID=" + uid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			reader.Read();
			User u = new User((string) reader["Username"], (string) reader["Password"],
			                  (string) reader["Fullname"], (string) reader["Mail"]);
			reader.Close();
			return u;
		}

		//Методы, относящиеся к вопросам
		public override Question[] GetQuestions(int tid)
		{
			ArrayList arr = new ArrayList();
			string select = "SELECT * FROM Questions WHERE TID=" + tid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			string q;
			string a;
			while (reader.Read())
			{
				q = (string) reader["Question"];
				try
				{
					a = (string) reader["Answer"];
				}
				catch
				{
					a = "";
				}
				int pid = (int) reader["PID"];
				int uid = (int) reader["UID"];
				int qid = (int) reader["QID"];
				arr.Add(new Question(q, a, pid, uid, tid, qid));
			}
			reader.Close();
			Question[] ret = (Question[]) Array.CreateInstance(typeof (Question), arr.Count);
			arr.CopyTo(ret);
			return ret;
		}

		public override int AddQuestion(int pid, int uid, int tid, string question)
		{
			string insert = String.Format("INSERT INTO Questions (PID,UID,TID,Question,Answer) VALUES ({0},{1},{2},'{3}','');",
			                              pid, uid, tid, question) + "SELECT QID From Questions WHERE QID=LAST_INSERT_ID();";

			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int qid = -1;
			try
			{
				//comm.ExecuteNonQuery();
				qid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
			return qid;
		}

		public override void AddAnswer(int qid, string answer, bool all)
		{
			string update = "UPDATE Questions SET Answer='" + answer + "' WHERE QID=" + qid;
			MySqlCommand comm = new MySqlCommand(update, (MySqlConnection) conn);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		public override Question GetQuestion(int qid)
		{
			string select = "SELECT * FROM Questions WHERE QID=" + qid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			string a;
			string q = (string) reader["Question"];

			int pid = (int) reader["PID"];
			int uid = (int) reader["UID"];
			int tid = (int) reader["TID"];
			reader.Read();
			try
			{
				a = (string) reader["Answer"];
			}
			catch
			{
				a = "";
			}
			reader.Close();
			return new Question(q, a, pid, uid, tid, qid);
		}

		public override bool Answered(int qid)
		{
			string select = "SELECT Answer FROM Questions WHERE QID=" + qid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			Object o = comm.ExecuteScalar();
			if (o is DBNull)
				return false;
			return true;
		}

		//Методы, относящиеся к Submissions
		public override ArrayList GetSubmissions(int tid, int uid)
		{
			string select = "SELECT * FROM Submissions WHERE TID=" + tid;
			if (uid != 0)
				select += " AND UID=" + uid;
			select += " ORDER BY SID ASC";
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			ArrayList subs = new ArrayList();
			while (reader.Read())
			{
				subs.Add(new Submission((int) reader["SID"], (int) reader["PID"], (int) reader["UID"], (DateTime) reader["Time"], (Language) Enum.Parse(typeof (Language), ((string) reader["Language"]), true),
				                        new SubmissionResult((Result) Enum.Parse(typeof (Result), (string) reader["Code"], true), (int) reader["Test"],
				                                             (string) reader["Info"], (float) reader["Time Worked"], (int) reader["Memory Used"])));
			}
			reader.Close();
			return subs;
		}

		public override Submission GetSubmission(int sid)
		{
			string select = "SELECT * FROM Submissions WHERE SID=" + sid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			reader.Read();
			Submission s = new Submission((int) reader["SID"], (int) reader["PID"], (int) reader["UID"], (DateTime) reader["Time"], (Language) Enum.Parse(typeof (Language), ((string) reader["Language"]), true),
			                              new SubmissionResult((Result) Enum.Parse(typeof (Result), (string) reader["Code"], true), (int) reader["Test"],
			                                                   (string) reader["Info"], (float) reader["Time Worked"], (int) reader["Memory Used"]));
			reader.Close();
			return s;
		}

		public override bool CheckSid(int sid)
		{
			string select = "SELECT SID FROM Submissions WHERE SID=" + sid;
			MySqlCommand comm = new MySqlCommand(select, (MySqlConnection) conn);
			MySqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		public override int AddSubmission(Submission s, int tid)
		{
			string insert = "INSERT INTO Submissions (PID,UID,TID,Time,Language,Code,Info,[Memory Used],[Time Worked])" +
				"VALUES (?pid,?uid,?tid,?time,?lang,'WAIT','',0,0);" +
				"SELECT SID FROM Submissions WHERE SID=LAST_INSERT_ID();";
			MySqlCommand comm = new MySqlCommand(insert, (MySqlConnection) conn);
			comm.Parameters.Add("?pid", s.PID);
			comm.Parameters.Add("?uid", s.UID);
			comm.Parameters.Add("?tid", tid);
			comm.Parameters.Add("?time", s.Time);
			comm.Parameters.Add("?lang", s.SubmissionLanguage.ToString());
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int sid = -1;
			try
			{
				sid = (int) comm.ExecuteScalar();
				//comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
			return sid;
		}

		public override void AddResult(SubmissionResult r, int sid)
		{
			if (r.TestNumber < 0)
				r.TestNumber = 0;
			r.Info = r.Info.Trim();
			string update = "UPDATE Submissions SET Code=?code,Test=?test,Info=?info,[Memory Used]=?mem,[Time Worked]=?time WHERE SID=?sid";
			/*r.Code.ToString(),r.TestNumber,r.Info,r.MemoryUsed,
				r.TimeWorked.,sid);*/
			MySqlCommand comm = new MySqlCommand(update, (MySqlConnection) conn);
			comm.Parameters.Add("?code", r.Code.ToString());
			comm.Parameters.Add("?test", r.TestNumber);
			comm.Parameters.Add("?info", r.Info);
			comm.Parameters.Add("?mem", r.MemoryUsed);
			comm.Parameters.Add("?time", r.TimeWorked.ToString(CultureInfo.InvariantCulture));
			comm.Parameters.Add("?sid", sid);
			MySqlTransaction trans = ((MySqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (MySqlException)
			{
				trans.Rollback();
			}
		}

		/*public override int GetLastSid()
		{
			string select = "SELECT IDENT_CURRENT('NeJudge.dbo.Submissions')";
			MySqlCommand comm = new MySqlCommand(select,(MySqlConnection)conn);
			return Convert.ToInt32((decimal)comm.ExecuteScalar());
		}*/
	}
}