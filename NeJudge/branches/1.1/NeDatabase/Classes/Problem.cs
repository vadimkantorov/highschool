using System;
using System.Xml;

namespace Ne.Database.New
{
	public class Problem : IStoreable
	{
		private int _id = -1;
		private XmlDocument _statement;
		private string _testPattern;
		private string _answerPattern;
		private string _inputFile;
		private string _outputFile;
		private int _timeLimit;
		private int _memoryLimit;
		private int _outputLimit;
		private string _checker;
		private string _shortName;

		public int ID
		{
			get { return _id; }
		}

		public XmlDocument Statement
		{
			get { return _statement; }
			set { _statement = value; }
		}

		public string TestPattern
		{
			get { return _testPattern; }
			set { _testPattern = value; }
		}

		public string AnswerPattern
		{
			get { return _answerPattern; }
			set { _answerPattern = value; }
		}

		public string InputFile
		{
			get { return _inputFile; }
			set { _inputFile = value; }
		}

		public string OutputFile
		{
			get { return _outputFile; }
			set { _outputFile = value; }
		}

		public int TimeLimit
		{
			get { return _timeLimit; }
			set { _timeLimit = value; }
		}

		public int MemoryLimit
		{
			get { return _memoryLimit; }
			set { _memoryLimit = value; }
		}

		public int OutputLimit
		{
			get { return _outputLimit; }
			set { _outputLimit = value; }
		}

		public string Checker
		{
			get { return _checker; }
			set { _checker = value; }
		}

		public string ShortName
		{
			get { return _shortName; }
		}

		public Problem() {}

		public Problem(string testPattern, string answerPattern, string inputFile, string outputFile,
			int timeLimit, int memoryLimit, int outputLimit, string checker, string shortName)
		{
			_testPattern = testPattern;
			_answerPattern = answerPattern;
			_inputFile = inputFile;
			_outputFile = outputFile;
			_timeLimit = timeLimit;
			_memoryLimit = memoryLimit;
			_outputLimit = outputLimit;
			_checker = checker;
			_shortName = shortName;
		}

		public bool ValidateID(int problemID)
		{
			return true;
		}

		public static Problem GetProblem(int problemID)
		{
			return null;
		}

		public static Problem[] GetProblems(int contestID)
		{
			return null;
		}

		public void AddToContest(int contestID)
		{}

		public void RemoveFromContest(int contestID)
		{}

		public void Store()
		{}
	}
}
