using System;
using System.Collections;
using System.Data;
using System.Security.Principal;

namespace Ne.Database
{
	public abstract class BaseDb : IDisposable
	{
		protected IDbConnection conn;
		private string connect;
		
		protected BaseDb(string connect)
		{
			this.connect = connect;
		}

		public void Dispose()
		{
			Close();
			GC.SuppressFinalize(this);
		}

		~BaseDb()
		{
			Close();
		}

		public void Open()
		{
			if(State == ConnectionState.Closed)
				conn.Open();
		}

		public void Close()
		{
			if(State != ConnectionState.Closed)
				conn.Close();
		}

		public ConnectionState State
		{
			get { return conn.State; }
		}

		public static bool IsAdmin(IPrincipal user)
		{
			return user.IsInRole("Administrator");
		}

		public static bool IsAnonymous(IPrincipal user)
		{
			return user.IsInRole("Anonymous") || user.Identity.IsAuthenticated == false || user.Identity.Name == "";
		}

		public static bool IsJudge(IPrincipal user)
		{
			return user.IsInRole("Judge");
		}

		public static bool IsDeveloper(IPrincipal user)
		{
			return user.IsInRole("Developer");
		}

		public static bool IsUser(IPrincipal user)
		{
			return user.IsInRole("User");
		}

		public IDbConnection Connection
		{
			get { return conn; }
		}

		public string ConnectionString
		{
			get { return connect; }
		}


		//Методы, относящиеся к соревнованиям
		public abstract int AddContest(Contest t);
		public abstract void UpdateContest(Contest t, int tid);
		public abstract Contest GetContest(int tid);
		public abstract int GetTid(int pid);
		public abstract bool CheckTid(int tid);
		public abstract int[] GetTids();
		public abstract int[] GetOldTids();
		public abstract int[] GetNowTids();
		public abstract int[] GetFutureTids();
		//public abstract bool Old(int tid);

		//Методы, относящиеся к задачам
		public abstract DataTable GetTable(TableType t, params object[] args);
		public abstract int GetPid(string problemname);
		public abstract bool CheckPid(int pid);
		public abstract string GetProblemShortName(int pid);
		public abstract Problem GetProblem(int pid);
		public abstract ArrayList GetProblems(int tid);
		public abstract int AddProblem(Problem p);
		public abstract void UpdateProblem(Problem p);
		public abstract void RemoveProblem(int pid);
		public abstract string GetNextShortName(int tid);
		public abstract int GetPidByShortName(int tid, string short_name);

		//Методы, относящиеся к юзерам
		public abstract void AddUser(User u);
		public abstract void UpdateUser(User u, int uid);
		public abstract bool IsRegistered(string username);
		public abstract bool Authenticate(string username, string password);
		public abstract int GetUid(string name);
		public abstract bool CheckUid(int uid);
		public abstract User GetUser(int uid);
		public abstract Role GetRoles(string username);
		public abstract ArrayList GetUsers();

		//Методы, относящиеся к вопросам
		public abstract ArrayList GetQuestions(int tid);
		public abstract int AddQuestion(int pid, int uid, int tid, string question);
		public abstract void AddAnswer(int qid, string answer, bool all);
		public abstract Question GetQuestion(int qid);
		public abstract bool Answered(int qid);

		//Методы, относящиеся к Submissions
		public abstract ArrayList GetSubmissions(int tid, int uid);
		public abstract ArrayList GetSubmissions(int tid, int uid, int pid, string result);
		public abstract Submission GetSubmission(int sid);
		public abstract int AddSubmission(Submission s);
		public abstract void AddResult(SubmissionResult s, int sid);
		public abstract bool CheckSid(int sid);
		//public abstract int GetLastSid();
	}
}