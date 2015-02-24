using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Ne.Database
{
	public class MsSqlDb : BaseDb
	{
		public MsSqlDb(string connect) : base(connect)
		{
			conn = new SqlConnection(connect);
			Open();
		}

		//Методы, относящиеся к соревнования
		public override int AddContest(Contest t)
		{
			string insert = "INSERT INTO Contests (Beginning,Ending,Name) VALUES (@beg,@end,@name);" +
				"SELECT TID FROM Contests WHERE TID=@@IDENTITY";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@beg", t.Beginning.ToString("g"));
			comm.Parameters.Add("@end", t.Ending.ToString("g")); //"yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("@name", t.Name);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int tid = -1;
			try
			{
				tid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
			return tid;
		}

		public override void UpdateContest(Contest t, int tid)
		{
			string insert = "UPDATE Contests SET Beginning=@beg,Ending=@end,Name=@name WHERE TID=@tid";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@beg", t.Beginning.ToString("g"));
			comm.Parameters.Add("@end", t.Ending.ToString("g")); //"yyyy.MM.dd hh:mm:ss"));
			comm.Parameters.Add("@name", t.Name);
			comm.Parameters.Add("@tid", tid);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteScalar();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		public override Contest GetContest(int tid)
		{
			string select = "SELECT * FROM Contests WHERE TID=" + tid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckTid(int tid)
		{
			string select = "SELECT TID FROM Contests WHERE TID=" + tid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			string select = "SELECT TID FROM Contests WHERE Ending < GETDATE()"; //dd.MM.yy hh:mm:ss
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			string select = "SELECT TID FROM Contests WHERE Beginning < GETDATE() AND Ending >= GETDATE()";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			string select = "SELECT * FROM Contests WHERE Beginning > GETDATE()";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlCommand comm = new SqlCommand(select,(SqlConnection)conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlDataAdapter da;
			switch (t)
			{
				case TableType.foredit:
					select = "SELECT PID,Name,'../editproblem.aspx?pid='+CAST(PID AS nvarchar) AS 'Url',LEFT(CAST(Text as nvarchar),100)+'...' AS 'Text',ShortName FROM Problems WHERE TID=" + args[0];
					da = new SqlDataAdapter(select, (SqlConnection) conn);
					dt.TableName = "Задачи";
					da.Fill(dt);
					break;
				case TableType.forset:
					select = "SELECT Name,PID,'../problem.aspx?pid='+CAST(PID AS nvarchar) AS 'Url',ShortName FROM Problems WHERE TID=" + args[0];
					da = new SqlDataAdapter(select, (SqlConnection) conn);
					dt.TableName = "Задачи";
					da.Fill(dt);
					break;
			}
			return dt;
		}

		public override int GetPid(string problemname)
		{
			string select = "SELECT PID FROM Problems WHERE Name='" + problemname + "';";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckPid(int pid)
		{
			string select = "SELECT PID FROM Problems WHERE PID=" + pid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			/*while(reader.Read())
				arr.Add(GetProblem((int)reader["PID"]));*/
			while (reader.Read())
				arr.Add(new Problem((string) reader["Name"], (string) reader["Text"],
				                    (string) reader["InputFormat"], (string) reader["OutputFormat"], (string) reader["InputSample"], (string) reader["OutputSample"],
				                    (string) reader["ShortName"], (string) reader["Author"], (int) reader["PID"], (int) reader["TID"]));
			reader.Close();
			return arr;
		}

		public override string GetProblemShortName(int pid)
		{
			string select = "SELECT ShortName FROM Problems WHERE PID=" + pid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			return (string)comm.ExecuteScalar();
		}

		public override Problem GetProblem(int pid)
		{
			string select = "SELECT * FROM Problems WHERE PID=" + pid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
					"VALUES (@tid,@name,@text,@iform,@oform,@isamp,@osamp,@sname,@auth);" +
					"SELECT PID FROM Problems WHERE PID=@@IDENTITY";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@tid", p.TID);
			comm.Parameters.Add("@name", p.Name);
			comm.Parameters.Add("@text", p.Text);
			comm.Parameters.Add("@iform", p.InputFormat);
			comm.Parameters.Add("@oform", p.OutputFormat);
			comm.Parameters.Add("@isamp", p.InputSample);
			comm.Parameters.Add("@osamp", p.OutputSample);
			comm.Parameters.Add("@sname", GetNextShortName(p.TID));
			comm.Parameters.Add("@auth", p.Author);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int pid = -1;
			try
			{
				pid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
			return pid;
		}

		public override void UpdateProblem(Problem p)
		{
			string update = "UPDATE Problems SET TID=@tid,Name=@name,Text=@text,InputFormat=@iform" +
				",OutputFormat=@oform,InputSample=@isamp,OutputSample=@osamp,Author=@auth WHERE PID=" + p.PID;
			SqlCommand comm = new SqlCommand(update, (SqlConnection) conn);
			comm.Parameters.Add("@tid", p.TID);
			comm.Parameters.Add("@name", p.Name);
			comm.Parameters.Add("@text", p.Text);
			comm.Parameters.Add("@iform", p.InputFormat);
			comm.Parameters.Add("@oform", p.OutputFormat);
			comm.Parameters.Add("@isamp", p.InputSample);
			comm.Parameters.Add("@osamp", p.OutputSample);
			comm.Parameters.Add("@auth", p.Author);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		public override void RemoveProblem(int pid)
		{
			string delete = "DELETE FROM Problems WHERE PID=" + pid;
			SqlCommand comm = new SqlCommand(delete, (SqlConnection) conn);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch
			{
				trans.Rollback();
				throw;
			}
		}

		public override string GetNextShortName(int tid)
		{
			string select = "SELECT TOP 1 ShortName FROM Problems WHERE TID=" + tid + " ORDER BY PID DESC";
			SqlConnection co = new SqlConnection(SqlConfig.SqlConnectionString);
			co.Open();
			SqlCommand comm = new SqlCommand(select, co);
			SqlDataReader reader = comm.ExecuteReader();
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

		public override int GetPidByShortName(int tid, string short_name)
		{
			string select = String.Format
				("SELECT PID FROM Problems WHERE TID={0} AND ShortName='{1}'", tid, short_name);
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			object o = comm.ExecuteScalar();
			if ( o == null|| o is DBNull )
			{
				return -1;
			}
			return (int)o;
		}

		//Методы, относящиеся к юзерам
		public override void AddUser(User u)
		{
			string insert = "INSERT INTO Users (Username,Password,Fullname,Mail,Role)" +
				" VALUES (@uname,@pass,@full,@mail,8)";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@uname", u.Username);
			comm.Parameters.Add("@pass", u.Password);
			comm.Parameters.Add("@full", u.Fullname);
			comm.Parameters.Add("@mail", u.Email);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		public override void UpdateUser(User u, int uid)
		{
			string insert = "UPDATE Users SET Password=@pass,Fullname=@full,Mail=@mail" +
				" WHERE UID=@uid";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@uid", uid);
			comm.Parameters.Add("@pass", u.Password);
			comm.Parameters.Add("@full", u.Fullname);
			comm.Parameters.Add("@mail", u.Email);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		public override Role GetRoles(string name)
		{
			if (name == "admin")
			{
				return Role.Administrator | Role.Judge | Role.Developer;
			}
			string select = "SELECT Role FROM Users WHERE Username='" + name + "'";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			return (Role) Enum.Parse(typeof (Role), ((int) comm.ExecuteScalar()).ToString());
		}

		public override bool IsRegistered(string username)
		{
			if (username == "admin" || username == "anonymous" || username.Trim() == "")
				return true;
			string select = "SELECT Username FROM Users";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			if (username == "admin" && password == "admin")
			{
				return true;
			}
			string select = "SELECT Password FROM Users WHERE Username='" + username + "'";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				if ((string) reader["Password"] == password)
				{
					reader.Close();
					return true;
				}
			}
			reader.Close();
			return false;
		}

		public override int GetUid(string name)
		{
			if (name == "admin")
				return 0;
			string select = "SELECT UID FROM Users WHERE Username=@uname";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			comm.Parameters.Add("@uname", name);
			return (int) comm.ExecuteScalar();
		}

		public override bool CheckUid(int uid)
		{
			string select = "SELECT UID FROM Users WHERE UID=" + uid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		public override ArrayList GetUsers()
		{
			ArrayList arr = new ArrayList();
			string select = "SELECT * FROM Users";
			SqlCommand comm = new SqlCommand(select,(SqlConnection)conn);
			SqlDataReader reader = comm.ExecuteReader();
			while(reader.Read())
			{
				arr.Add(new User((string) reader["Username"], (string) reader["Password"],
					(string) reader["Fullname"], (string) reader["Mail"]));
			}
			reader.Close();
			return arr;
		}

		public override User GetUser(int uid)
		{
			string select = "SELECT * FROM Users WHERE UID=" + uid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			reader.Read();
			User u = new User((string) reader["Username"], (string) reader["Password"],
			                  (string) reader["Fullname"], (string) reader["Mail"]);
			reader.Close();
			return u;
		}

		//Методы, относящиеся к вопросам
		public override ArrayList GetQuestions(int tid)
		{
			ArrayList arr = new ArrayList();
			string select = "SELECT * FROM Questions WHERE TID=" + tid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			return arr;
		}

		public override int AddQuestion(int pid, int uid, int tid, string question)
		{
			string insert = String.Format("INSERT INTO Questions (PID,UID,TID,Question,Answer) VALUES (@pid,@uid,@tid,@ques,'');",
			                              pid, uid, tid, question) + "SELECT QID From Questions WHERE QID=@@IDENTITY";

			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@pid", pid);
			comm.Parameters.Add("@uid", uid);
			comm.Parameters.Add("@tid", tid);
			comm.Parameters.Add("@ques", question);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int qid = -1;
			try
			{
				//comm.ExecuteNonQuery();
				qid = (int) comm.ExecuteScalar();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
			return qid;
		}

		public override void AddAnswer(int qid, string answer, bool all)
		{
			string update = "UPDATE Questions SET Answer=@ans WHERE QID=@qid";
			SqlCommand comm = new SqlCommand(update, (SqlConnection) conn);
			//comm.Parameters.Add("@uid", all ? "0" : "UID");
			comm.Parameters.Add("@ans", answer);
			comm.Parameters.Add("@qid", qid);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		public override Question GetQuestion(int qid)
		{
			string select = "SELECT * FROM Questions WHERE QID=" + qid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
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
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			Object o = comm.ExecuteScalar();
			if (o is DBNull)
				return false;
			return true;
		}

		//Методы, относящиеся к Submissions
		public override ArrayList GetSubmissions(int tid, int uid)
		{
			return GetSubmissions(tid, uid, 0, "");
		}

		public override ArrayList GetSubmissions(int tid, int uid, int pid, string result)
		{
			string select = "SELECT * FROM Submissions WHERE TID=@tid";
			if ( uid != 0 )
			{
				select += " AND UID=@uid ";
			}
			if ( pid != 0 )
			{
				select += " AND PID=@sn ";
			}
			if ( result != string.Empty )
			{
				select += " AND Code=@res";
			}
			select += " ORDER BY SID ASC";
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			comm.Parameters.Add("@tid", tid);
			comm.Parameters.Add("@uid", uid);
			comm.Parameters.Add("@sn", pid);
			comm.Parameters.Add("@res", result);
			SqlDataReader reader = comm.ExecuteReader();
			ArrayList subs = new ArrayList();
			while (reader.Read())
			{
				subs.Add(new Submission((int) reader["SID"], (int) reader["PID"], (int) reader["UID"],
					(int) reader["TID"], (DateTime) reader["Time"], (Language) Enum.Parse(typeof (Language), 
					((string) reader["Language"]), true),
						new SubmissionResult((Result) Enum.Parse(typeof (Result), 
						(string) reader["Code"], true), (int) reader["Test"],
						(string) reader["Info"], (float) reader["Time Worked"], (int) reader["Memory Used"]))
					);
			}
			reader.Close();
			return subs;
		}

		public override Submission GetSubmission(int sid)
		{
			string select = "SELECT * FROM Submissions WHERE SID=" + sid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			reader.Read();
			Submission s = new Submission((int) reader["SID"], (int) reader["PID"], (int) reader["UID"], (int) reader["TID"],
				(DateTime) reader["Time"], (Language) Enum.Parse(typeof (Language), ((string) reader["Language"]), true),
					new SubmissionResult((Result) Enum.Parse(typeof (Result), (string) reader["Code"], true), (int) reader["Test"],
			        (string) reader["Info"], (float) reader["Time Worked"], (int) reader["Memory Used"]));
			reader.Close();
			return s;
		}

		public override bool CheckSid(int sid)
		{
			string select = "SELECT SID FROM Submissions WHERE SID=" + sid;
			SqlCommand comm = new SqlCommand(select, (SqlConnection) conn);
			SqlDataReader reader = comm.ExecuteReader();
			if (reader.Read())
			{
				reader.Close();
				return true; //TODO: проверить
			}
			reader.Close();
			return false;
		}

		public override int AddSubmission(Submission s)
		{
			string insert = "INSERT INTO Submissions (PID,UID,TID,Time,Language,Code,Info,[Memory Used],[Time Worked])" +
				"VALUES (@pid,@uid,@tid,@time,@lang,'WAIT','',0,0);" +
				"SELECT SID FROM Submissions WHERE SID=@@IDENTITY";
			SqlCommand comm = new SqlCommand(insert, (SqlConnection) conn);
			comm.Parameters.Add("@pid", s.PID);
			comm.Parameters.Add("@uid", s.UID);
			comm.Parameters.Add("@tid", s.TID);
			comm.Parameters.Add("@time", s.Time);
			comm.Parameters.Add("@lang", s.SubmissionLanguage.ToString());
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			int sid = -1;
			try
			{
				sid = (int) comm.ExecuteScalar();
				//comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
			return sid;
		}

		public override void AddResult(SubmissionResult r, int sid)
		{
			if (r.TestNumber < 0)
				r.TestNumber = 0;
			r.Info = r.Info.Trim();
			string update = "UPDATE Submissions SET Code=@code,Test=@test,Info=@info,[Memory Used]=@mem,[Time Worked]=@time WHERE SID=@sid";
			/*r.Code.ToString(),r.TestNumber,r.Info,r.MemoryUsed,
				r.TimeWorked.,sid);*/
			SqlCommand comm = new SqlCommand(update, (SqlConnection) conn);
			comm.Parameters.Add("@code", r.Code.ToString());
			comm.Parameters.Add("@test", r.TestNumber);
			comm.Parameters.Add("@info", r.Info);
			comm.Parameters.Add("@mem", r.MemoryUsed);
			comm.Parameters.Add("@time", r.TimeWorked.ToString(CultureInfo.InvariantCulture));
			comm.Parameters.Add("@sid", sid);
			SqlTransaction trans = ((SqlConnection) conn).BeginTransaction(IsolationLevel.ReadCommitted);
			comm.Transaction = trans;
			try
			{
				comm.ExecuteNonQuery();
				trans.Commit();
			}
			catch (SqlException)
			{
				trans.Rollback();
				throw;
			}
		}

		/*public override int GetLastSid()
		{
			string select = "SELECT IDENT_CURRENT('NeJudge.dbo.Submissions')";
			SqlCommand comm = new SqlCommand(select,(SqlConnection)conn);
			return Convert.ToInt32((decimal)comm.ExecuteScalar());
		}*/
	}
}