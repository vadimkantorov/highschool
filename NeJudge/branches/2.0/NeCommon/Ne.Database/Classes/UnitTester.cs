using System;
using System.Xml;

using NUnit.Framework;

namespace Ne.Database.New.Classes
{
	[TestFixture]
	public class MsSqlTester
	{
		private XmlDocument createMess(string rootName)
		{
			XmlDocument doc = new XmlDocument();
			XmlElement root = doc.CreateElement(rootName);
			string[] names = new string[] {"foo", "bar", "baz", "xyzzy"};
			Random r = new Random(Environment.TickCount);
			for ( int i = 0; i < 10; i++ )
			{
				int actType = r.Next()%2;
				if ( actType == 0 )
					root.SetAttribute(names[r.Next()%names.Length], names[r.Next()%names.Length]);
				else
				{
					XmlElement ch = doc.CreateElement(names[r.Next()%names.Length]);
					ch.SetAttribute(names[r.Next()%names.Length], names[r.Next()%names.Length]);
					root.AppendChild(ch);
				}
			}
			doc.AppendChild(root);
			return doc;
		}

		[Test]
		public void TestProblemAndContest()
		{
			DateTime begin = new DateTime(2007, 9, 13, 16, 0, 0);
			DateTime end = new DateTime(2007, 10, 16, 16, 0, 0);
			Contest c = new Contest(begin, end, "To hell with the monkeys!");
			c.Store();
			c.Name = "You gotta do what you gotta do";
			c.Beginning = c.Ending = DateTime.Now;
			c.Store();
			Contest temp = Contest.GetContest(c.ID);
			Assert.AreEqual(true, Contest.ValidateID(c.ID));
			Assert.AreEqual(false, Contest.ValidateID(34875893));

			Problem p = new Problem(c.ID, "A", "Knowledge brings fear", 1000, 2000, 3000, "input.txt", "output.txt");

			p.Statement = new ProblemStatement(createMess("problem"));
			p.TesterParams = new AcmTesterParameters(12, ".in", ".out");
			p.CheckerParams = new CheckerParameters();
			p.Store();

			Assert.AreNotEqual(null, p.Statement);
			Assert.AreNotEqual(null, p.TesterParams);
			Assert.AreNotEqual(null, p.CheckerParams);

			p.Name = "Apples";
			p.Store();
			Assert.AreEqual(true, Problem.ValidateID(p.ID));

			p.Remove();
			c.Remove();
		}
	}
}