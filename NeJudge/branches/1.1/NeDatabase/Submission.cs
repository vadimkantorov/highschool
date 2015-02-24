using System;

namespace Ne.Database
{
	public class Submission
	{
		private int sid, pid, uid, tid;
		private DateTime time;

		private Language lang;
		private SubmissionResult r;

		public Submission()
		{
		}

		public Submission(int sid, int pid, int uid, int tid, DateTime time, Language lang, SubmissionResult r)
			: this(sid, pid, uid, tid, time, lang)
		{
			this.r = r;
		}

		public Submission(int sid, int pid, int uid, int tid, DateTime time, Language lang)
			: this(pid, uid, tid, time, lang)
		{
			this.sid = sid;
		}

		public Submission(int pid, int uid, int tid, DateTime time, Language lang)
		{
			this.pid = pid;
			this.uid = uid;
			this.tid = tid;
			this.time = time;
			this.lang = lang;
		}

		public SubmissionResult Result
		{
			get { return r; }
		}

		public int SID
		{
			get { return sid; }
		}

		public int PID
		{
			get { return pid; }
		}

		public int UID
		{
			get { return uid; }
		}

		public DateTime Time
		{
			get { return time; }
		}

		public Language SubmissionLanguage
		{
			get { return lang; }
		}

		public int TID
		{
			get { return tid; }
		}
	}
}