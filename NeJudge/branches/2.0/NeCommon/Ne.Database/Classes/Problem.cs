using System;
using System.Xml;
using System.Xml.Xsl;
using System.Web;
using System.Collections.Generic;

using Ne.Database.Interfaces;

namespace Ne.Database.Classes
{
	[Serializable]
	public class ProblemStatement
	{
		string inputFormat;
		string outputFormat;
		string text;
		string inputSample;
		string outputSample;
		string author;
		System.Collections.Generic.List<string> hints;
		XmlDocument document;

		public ProblemStatement()
		{
			inputFormat = "";
			outputFormat = "";
			inputSample = "";
			outputSample = "";
			text = "";
			author = "";
			XmlDocument doc = new XmlDocument();
			doc.LoadXml("<problem>" +
				"<introduction />" +
				"<input-format />" +
				"<output-format />" +
				"<input-sample />" +
				"<output-sample />" +
				"</problem>");
			document = doc;
		}

		public ProblemStatement(XmlDocument doc)
		{
			XmlNode prt = doc.SelectSingleNode("problem/introduction");
			XmlNode inf = doc.SelectSingleNode("problem/input-format");
			XmlNode ouf = doc.SelectSingleNode("problem/output-format");
			XmlNode ins = doc.SelectSingleNode("problem/input-sample");
			XmlNode ous = doc.SelectSingleNode("problem/output-sample");
			XmlNode aut = doc.SelectSingleNode("problem/author");
			XmlNodeList hintl = doc.SelectNodes("problem/hints/hint");

			hints = new System.Collections.Generic.List<string>();
			foreach( XmlNode n in hintl )
				hints.Add(n.InnerXml);
			if( prt != null ) text = HttpUtility.HtmlDecode(prt.InnerXml);
			if( inf != null ) inputFormat = HttpUtility.HtmlDecode(inf.InnerXml);
			if( ouf != null ) outputFormat = HttpUtility.HtmlDecode(ouf.InnerXml);
			if( ins != null ) inputSample = ins.InnerXml;
			if( ous != null ) outputSample = ous.InnerXml;
			if( aut != null ) author = aut.InnerXml;
			document = doc;
		}

		public string InputFormat
		{
			get { return inputFormat; }
			set { inputFormat = value; }
		}

		public string OutputFormat
		{
			get { return outputFormat; }
			set { outputFormat = value; }
		}

		public string InputSample
		{
			get { return inputSample; }
			set { inputSample = value; }
		}

		public string OutputSample
		{
			get { return outputSample; }
			set { outputSample = value; }
		}

		public string Author
		{
			get { return author; }
			set { author = value; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public System.Collections.Generic.List<string> Hints
		{
			get { return hints; }
		}

		public XmlDocument XmlDocument
		{
			get
			{
				/*string fxml = "<problem>\r\n";
				fxml += "<introduction>" + HttpUtility.HtmlEncode(text) + "</introduction>";
				if( inputFormat != string.Empty )
					fxml += "<input-format>" + HttpUtility.HtmlEncode(inputFormat) + "</input-format>";
				if( outputFormat != string.Empty )
					fxml += "<output-format>" + HttpUtility.HtmlEncode(outputFormat) + "</output-format>";
				if( inputSample != string.Empty )
					fxml += "<input-example>" + inputSample + "</input-example>";
				if( outputSample != string.Empty )
					fxml += "<output-example>" + outputSample + "</output-example>";
				if( author != string.Empty )
					fxml += "<author>" + author + "</author>";
				if( hints.Count != 0 )
				{
					fxml += "<hints>";
					foreach( string hint in hints )
						fxml += "<hint>" + hint + "</hint>";
					fxml += "</hints>";
				}
				fxml += "</problem>";
				//TODO: Add replacing with "&lt;bold&gt;" etc
				XmlDocument c = new XmlDocument();
				c.LoadXml(fxml);
				return c;*/
				return document;
			}
		}

	}

	[Serializable]
	public class Problem
	{
		#region Fields

		int _id = -1;
		int _contestID;
		string _shortName;
		string _name;

		ProblemStatement _statement;

		int _timeLimit;
		int _memoryLimit;
		int _outputLimit;

		string _inputFile = "";
		string _outputFile = "";

		byte[] ckeckerBytes;

		#endregion

		public const string STDIN_NAME = "$std.in";
		public const string STDOUT_NAME = "$std.out";

		#region Properties

		public int ID
		{
			get { return _id; }
			set { _id = value; }
		}

		public int ContestID
		{
			get { return _contestID; }
			set { _contestID = value; }
		}

		public string ShortName
		{
			get { return _shortName; }
			set { _shortName = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public byte[] CheckerBytes
		{
			get
			{
				//if ( ckeckerBytes == null )
				//	LoadCheckerBytes();
				return ckeckerBytes; 
			}
			set { ckeckerBytes = value; }
		}

		public ProblemStatement Statement
		{
			get
			{
				//if( _statement == null )
				//	LoadStatement();
				return _statement;
			}
			set { _statement = value; }
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

		#endregion

		#region Constructors

		public Problem(int contestID, string name, int timeLimit, int memoryLimit, int outputLimit,
			string inputFile, string outputFile) : this()
		{
			_contestID = contestID;
			_name = name;
			_timeLimit = timeLimit;
			_memoryLimit = memoryLimit;
			_outputLimit = outputLimit;
			_inputFile = inputFile;
			_outputFile = outputFile;
		}

		public Problem()
		{
			_statement = new ProblemStatement();
			ckeckerBytes = new byte[] { };
		}

		#endregion

		#region Database Access Members

		public static bool ValidateID(int problemID)
		{
			return DataProvider.Provider.ProblemManager.ValidateID(problemID);
		}

		public static Problem GetProblem(int problemID)
		{
			return DataProvider.Provider.ProblemManager.GetProblem(problemID);
		}

		public static Problem[] GetProblems(int contestID)
		{
			return DataProvider.Provider.ProblemManager.GetProblems(contestID);
		}

		public void Remove()
		{
			DataProvider.Provider.ProblemManager.RemoveProblem(_id);
		}

		public void Store()
		{
			//TODO: check if object to store is valid
			if( _id == -1 )
			{
				string maxs = "@";
				foreach( Problem p in GetProblems(_contestID) )
					if( string.Compare(p._shortName,maxs) > 0 )
						maxs = p._shortName;
				char[] T = maxs.ToCharArray();
				T[T.Length - 1]++;
				int mod = 0;
				for ( int i = maxs.Length - 1; i >= 0; i-- )
				{
					int tmp = mod + T[i] - 'A';
					mod = tmp / 10;
					T[i] = (char)(tmp % 10 + 'A');
				}
				if ( mod != 0 )
					_shortName = ( mod + 'A' ) + new string(T);
				else
					_shortName = new string(T);
				DataProvider.Provider.ProblemManager.AddProblem(this);
			}
			else
				DataProvider.Provider.ProblemManager.UpdateProblem(this);
		}

		public void LoadStatement()
		{
			if ( _id != -1 )
				_statement = new ProblemStatement(
					DataProvider.Provider.ProblemManager.GetProblemXmlData(_id, "Statement"));
		}

		public void LoadCheckerBytes()
		{
			if ( _id != -1 )
				ckeckerBytes = DataProvider.Provider.ProblemManager.GetCheckerBytes(_id);
		}
		#endregion
	}
}