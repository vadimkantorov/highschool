using System;
using System.IO;
using System.Xml;

using Ne.Database.Classes;

namespace Ne.Tester
{
	public class Checker
	{
		string exe;
		string wDir;
		string comment;

		public string CheckerComment
		{
			get { return comment; }
		}

		CheckStatus MapOutcomeToCheckStatus(string outcome)
		{
			outcome = outcome.ToLower();
			switch ( outcome )
			{
				case "accepted":
				case "ok":
					return CheckStatus.Ok;
				case "wrong-answer":
				case "wa":
					return CheckStatus.WrongAnswer;
				case "presentation-error":
				case "pe":
					return CheckStatus.PresentationError;
				default:
					throw new NeTesterException("Checker reported of failure");
			}
		}

		public CheckStatus Check(string[] files)
		{
			bool okParams = true;
			foreach ( string s in files )
				if ( s == null || s == "" )
					okParams = false;

			if ( files.Length != 3 || !okParams )
				throw new NeTesterException("Invalid checker parameters");

			string resultXml = Path.Combine(wDir, "result.xml");

			if ( !File.Exists(exe) )
				throw new NeTesterException("Checker file doesn't exist");

			DfyzProc prc = new DfyzProc(exe, wDir, null);

			foreach ( string s in files )
				prc.AddArgument(s);

			prc.AddArgument(resultXml);
			prc.AddArgument("-appes");

			prc.SetCommonParams();
			prc.StdinRedirection = prc.StdoutRedirection = prc.StderrRedirection = DfyzProc.NULL_DEVICE;

			RunResult rr = prc.Run();
			if ( rr.Status != RunStatus.Ok )
			{
				string message = "Running checker failed";
				if ( rr.Status == RunStatus.Failure )
					message += String.Format(": {0}", prc.Comment);
				throw new NeTesterException(message);
			}

			XmlDocument doc = new XmlDocument();
			try
			{
				doc.Load(resultXml);
			}
			catch ( XmlException ex )
			{
				throw new NeTesterException(String.Format("Cannot load checker's result file: {0}", ex.Message));
			}
			if ( doc.DocumentElement.Name != "result" || !doc.DocumentElement.HasAttribute("outcome") )
				throw new NeTesterException("Checker's result file has invalid format");
			comment = doc.DocumentElement.InnerText;
			return MapOutcomeToCheckStatus(doc.DocumentElement.Attributes["outcome"].Value);
		}

		public Checker(string checkerExe, string workDir)
		{
			exe = checkerExe;
			wDir = workDir;
		}
	}
}