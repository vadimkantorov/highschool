using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace Ne.Database.Classes
{
	[XmlRoot("testlog")]
	public class TestLog
	{
		string compReport = "";
		List<TestRunInfo> testCollection = new List<TestRunInfo>();

		[XmlArray("testruninfo")]
		public List<TestRunInfo> TestCollection
		{
			get { return testCollection; }
			set { testCollection = value; }
		}
		
		[XmlElement("compilationreport")]
		public string CompilationReport
		{
			get { return compReport; }
			set { compReport = value; }
		}

		public XmlDocument ToXml()
		{
			XmlSerializer xs = new XmlSerializer(typeof(TestLog));
			using ( MemoryStream ms = new MemoryStream() )
			{
				xs.Serialize(ms , this);
				ms.Position = 0;
				XmlDocument doc = new XmlDocument();
				doc.Load(ms);
				return doc;
			}
		}

		public static TestLog FromXml(XmlDocument doc)
		{
			XmlSerializer xs = new XmlSerializer(typeof(TestLog));
			using ( MemoryStream ms = new MemoryStream() )
			{
				doc.Save(ms);
				ms.Position = 0;
				return (TestLog)xs.Deserialize(ms);
			}
		}
	}
}