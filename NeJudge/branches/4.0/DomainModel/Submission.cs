using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	public class Submission
	{
		#region Fields

		string source;
		string outcome;
		DateTime time;
		TestLog testLog;
		bool testLogDeserialized;
		
		int id;
		Language language;
		Problem problem;
		string testLogXml;
		
		#endregion

		#region Properties

		public string Outcome
		{
			get { return outcome; }
			set { outcome = value; }
		}
		
		public string Source
		{
			get { return source; }
			set { source = value; }
		}

		public DateTime Time
		{
			get { return time; }
			set { time = value; }
		}

		public TestLog TestLog
		{
			get
			{
				if( !testLogDeserialized )
				{
					testLog = TestLog.FromXmlString(testLogXml);
					testLogDeserialized = true;
				}
				return testLog;
			}
			set
			{
				testLog = value;
				testLogXml = testLog.ToXmlString();
				testLogDeserialized = true;
			}
		}

		public int ID
		{
			get { return id; }
		}

		public Language Language
		{
			get { return language; }
			set { language = value; }
		}

		public Problem Problem
		{
			get { return problem; }
		}

		#endregion
	}
}
