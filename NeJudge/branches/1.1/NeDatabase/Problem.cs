using System.Collections;

namespace Ne.Database
{
	public class Problem : IComparer
	{
		private string name, problemtext, inputformat, outputformat,
			inputsample, outputsample, shortname, author;

		private int pid, tid;

		public Problem(string name, string problemtext, string inputformat, string outputformat,
		               string inputsample, string outputsample, string shortname, string author, int pid, int tid)
		{
			this.name = name;
			this.problemtext = problemtext;
			this.inputformat = inputformat;
			this.outputformat = outputformat;
			this.shortname = shortname;
			this.inputsample = inputsample;
			this.outputsample = outputsample;
			this.author = author;
			this.pid = pid;
			this.tid = tid;
		}

		public string Name
		{
			get { return name; }
		}

		public string Text
		{
			get { return problemtext; }
		}

		public string Author
		{
			get { return author; }
		}

		public string InputFormat
		{
			get { return inputformat; }
		}

		public string OutputFormat
		{
			get { return this.outputformat; }
		}

		public string ShortName
		{
			get { return shortname; }
		}

		public string InputSample
		{
			get { return inputsample; }
		}

		public string OutputSample
		{
			get { return this.outputsample; }
		}

		public int PID
		{
			get { return pid; }
		}

		public int TID
		{
			get { return tid; }
		}

		int IComparer.Compare(object x, object y)
		{
			if (((Problem) x).PID > ((Problem) y).PID)
				return 1;
			if (((Problem) x).PID < ((Problem) y).PID)
				return -1;
			return 0;
		}
	}
}