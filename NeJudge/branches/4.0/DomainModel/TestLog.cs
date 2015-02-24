using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.IO;

namespace Ne.DomainModel
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

		public string ToXmlString()
		{
			XmlSerializer xs = new XmlSerializer(typeof(TestLog));
			StringBuilder sb = new StringBuilder();
			
			using(XmlWriter xw = XmlWriter.Create(sb))
				xs.Serialize(xw,this);
			
			return sb.ToString();
		}

		public static TestLog FromXmlString(string xml)
		{
			XmlSerializer xs = new XmlSerializer(typeof(TestLog));

			using(StringReader sr = new StringReader(xml))
			using(XmlReader xr = XmlReader.Create(sr))
				return (TestLog)xs.Deserialize(xr);
		}
	}
}