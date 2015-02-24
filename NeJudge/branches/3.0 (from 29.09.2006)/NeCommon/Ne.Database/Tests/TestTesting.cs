using System;
using System.Text;
using System.IO;
using System.Xml;

using NUnit.Framework;

using Ne.Database.Classes;

namespace NeDatabase.Testing
{
	[TestFixture]
	public class TestTesting
	{
		const string FUR_BASE_DIR = @"D:\NeJudgeTest\2 - fur";

		readonly string FUR_TEST_DIR = Path.Combine(FUR_BASE_DIR, "tests");
		readonly string FUR_TESTS_XML = Path.Combine(FUR_BASE_DIR,
				Path.Combine("test-generators", "tests.xml")
			);

		const int TEST_COUNT = 25;

		[Test]
		public void RunTest()
		{
			int[] tPoints = new int[TEST_COUNT];
			string[] tDescs = new string[TEST_COUNT];

			XmlDocument xDoc = new XmlDocument();
			xDoc.Load(FUR_TESTS_XML);

			int i = 0;
			foreach ( XmlElement el in xDoc.DocumentElement )
			{
				tPoints[i] = int.Parse(el.GetAttribute("points"));
				tDescs[i] = el.GetAttribute("comment");
				++i;
			}

			foreach ( Test test in Test.GetTests(19) )
				Test.RemoveTest(19, test.TestNumber);

			Test first = null;

			for ( i = 1; i <= TEST_COUNT; ++i )
			{
				Test tst = new Test(19, i, tDescs[i - 1], tPoints[i - 1]);
				tst.Input = Encoding.ASCII.GetBytes(new StreamReader(
						Path.Combine(FUR_TEST_DIR, String.Format("{0:D2}", i))
					).ReadToEnd());
				tst.Output = Encoding.ASCII.GetBytes(new StreamReader(
						Path.Combine(FUR_TEST_DIR, String.Format("{0:D2}.a", i))
					).ReadToEnd());
				if ( i == 1 )
					first = tst;
				tst.Store();
			}

			Test t = Test.GetTest(19, 1);
			Assert.AreEqual(1, t.TestNumber);
			Assert.AreEqual(19, t.ProblemID);
			Assert.AreEqual(4, t.Points);

			t.Description = "Новое описание";
			t.Points = 17;

			t.Input = Encoding.ASCII.GetBytes("input");
			t.Output = Encoding.ASCII.GetBytes("output");

			t.Store();

			Test x = Test.GetTest(19, 1);

			Assert.AreEqual(t.Input.Length, x.Input.Length);
			for ( int j = 0; j < t.Input.Length; j++ )
				if ( t.Input[j] != x.Input[j] )
					Assert.Fail();

			Assert.AreEqual(t.Output.Length, x.Output.Length);
			for ( int j = 0; j < t.Output.Length; j++ )
				if ( t.Output[j] != x.Output[j] )
					Assert.Fail();
		}
	}
}
