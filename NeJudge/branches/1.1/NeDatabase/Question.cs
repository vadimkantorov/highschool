namespace Ne.Database
{
	public class Question
	{
		private string question, answer;
		private int pid, uid, tid, qid;

		public Question(string question, string answer, int pid, int uid, int tid, int qid)
		{
			this.question = question;
			this.answer = answer;
			this.pid = pid;
			this.uid = uid;
			this.tid = tid;
			this.qid = qid;
		}

		public string QuestioN
		{
			get { return question; }
		}

		public string Answer
		{
			get { return answer; }
		}

		public int Pid
		{
			get { return pid; }
		}

		public int Uid
		{
			get { return uid; }
		}

		public int Tid
		{
			get { return tid; }
		}

		public int Qid
		{
			get { return qid; }
		}

		public bool Answered
		{
			get
			{
				if (answer.Trim() != "" && answer != null)
					return true;
				return false;
			}
		}
	}
}