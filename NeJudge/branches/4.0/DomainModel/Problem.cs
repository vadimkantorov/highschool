using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;


namespace Ne.DomainModel
{
	public class ProblemTranslation
	{
		string culture;
		string translatedName;
		string translatedStatement;

		#region ORM related fields

		int id;
		ProblemTranslator translator;

		#endregion

		#region Properties

		public CultureInfo Culture
		{
			get { return CultureInfo.GetCultureInfo(culture); }
			set { culture = value.Name; }
		}
		
		public string TranslatedName
		{
			get { return translatedName; }
			set { translatedName = value; }
		}

		public string TranslatedStatement
		{
			get { return translatedStatement; }
			set { translatedStatement = value; }
		}

		#endregion
	}
	
	public class ProblemTranslator
	{
		string defaultName;
		string defaultStatement;
		string defaultCulture;

		public CultureInfo DefaultCulture
		{
			get { return CultureInfo.GetCultureInfo(defaultCulture); }
			set { defaultCulture = value.Name; }
		}

		public void SetDefaultName(string value)
		{
			defaultName = value;
		}

		public void SetDefaultStatement(string value)
		{
			defaultStatement = value;
		}

		public string GetTranslatedName(CultureInfo culture)
		{
			return "";
		}

		public string GetTranslatedStatement(CultureInfo culture)
		{
			return "";
		}
	}
	
	public class Problem
	{
		#region Constants

		public const string StandardInputFile = "$STDIN$";
		public const string StandardOutputFile = "$STDOUT$";

		#endregion

		#region Fields

		string shortName;
		bool isOpen;
		ProblemTranslator translator;
		
		int timeLimit;
		int memoryLimit;
		int outputLimit;
		string inputFile;
		string outputFile;
		byte[] checkerBytes;

		#region ORM related fields

		int id;
		//Contest contest;

		#endregion

		#endregion

		#region Properties

		public ProblemTranslator Translator
		{
			get { return translator; }
		}
		
		public string Name
		{
			get { return translator.GetTranslatedName(CultureInfo.CurrentCulture); }
		}

		public string Statement
		{
			get { return translator.GetTranslatedStatement(CultureInfo.CurrentCulture); }
		}
		
		public string ShortName
		{
			get { return shortName; }
			set { shortName = value; }
		}

		public bool IsOpen
		{
			get { return isOpen; }
			set { isOpen = value; }
		}

		public int TimeLimit
		{
			get { return timeLimit; }
			set { timeLimit = value; }
		}

		public int MemoryLimit
		{
			get { return memoryLimit; }
			set { memoryLimit = value; }
		}

		public int OutputLimit
		{
			get { return outputLimit; }
			set { outputLimit = value; }
		}

		public string InputFile
		{
			get { return inputFile; }
			set { inputFile = value; }
		}

		public string OutputFile
		{
			get { return outputFile; }
			set { outputFile = value; }
		}

		public byte[] CheckerBytes
		{
			get { return checkerBytes; }
			set { checkerBytes = value; }
		}

		#region ORM related properties

		public int ID
		{
			get { return id; }
		}

		#endregion

		#endregion
	}
}
