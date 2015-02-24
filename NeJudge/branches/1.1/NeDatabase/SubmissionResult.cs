namespace Ne.Database
{
	public class SubmissionResult
	{
		private string info;
		private float timeworked;
		private int memoryused, testnum;
		private Result code;

		public SubmissionResult(Result code, int testNumber, string info, float timeWorked, int memoryUsed)
		{
			this.code = code;
			this.testnum = testNumber;
			this.info = info;
			this.timeworked = timeWorked;
			this.memoryused = memoryUsed;
		}

		public SubmissionResult(Result code)
		{
			this.code = code;
			this.testnum = this.memoryused = this.memoryused = 0;
			this.info = "";
		}

		public SubmissionResult()
		{
			memoryused = 0;
			timeworked = 0;
			info = "";
			testnum = 0;
			code = Result.FA;
		}

		public Result Code
		{
			get { return code; }
			set { code = value; }
		}

		public int TestNumber
		{
			get { return testnum; }
			set { testnum = value; }
		}

		public string Info
		{
			get { return info; }
			set { info = value; }
		}

		public float TimeWorked
		{
			get { return timeworked; }
			set { timeworked = value; }
		}

		public int MemoryUsed
		{
			get { return memoryused; }
			set { memoryused = value; }
		}

		public string ToHtmlString()
		{
			if ( code == Result.CRASH || code == Result.SV )
			{
				return string.Format("{0}<br><font size='-1'>{1}</font>", ToString(), info);
			}
			return ToString();
		}

		public override string ToString()
		{
			switch (code)
			{
				case Result.AC:
					return "Accepted";
				case Result.CE:
					return "Compilation error";
				case Result.WAIT:
					return "Waiting";
				case Result.CRASH:
					return "Runtime error";
				case Result.MLE:
					return "Memory limit exceeded";
				case Result.OLE:
					return "Output limit exceeded";
				case Result.PE:
					return "Presentation error";
				case Result.TLE:
					return "Time limit exceeded";
				case Result.WA:
					return "Wrong answer";
				case Result.FA:
					return "Failure";
				case Result.SV:
					return "Security violation";
				case Result.RU:
					return "Compiling & running";
				default:
					return "Unknown code";
			}
		}
	}
}