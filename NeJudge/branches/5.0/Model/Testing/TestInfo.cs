using System;
using System.Collections.Generic;
using Model.Utils;

namespace Model.Testing
{
	public class TestInfo
	{
		public TestInfo()
		{
			Tests = new List<Test>();
			MarkDirty();
		}

		public void InsertTestsAt(int ind, IList<Test> newTests)
		{
			for (int i = 0; i < newTests.Count; i++)
			{
				//newTests[i].Problem = Problem;
				Tests.Insert(ind + i, newTests[i]);
			}
			MarkDirty();
		}

		public void RemoveTest(int testId)
		{
			int ind = Tests.FindIndex(x => x.Id == testId);
			//Tests[ind].Problem = null;

			Tests.RemoveAt(ind);
			MarkDirty();
		}

		public void MarkDirty()
		{
			Id = Guid.NewGuid();
		}

		public Guid Id { get; set; }

		public Problem Problem { get; set; }

		public string CheckerArguments { get; set;}

		public ProgramSource Checker { get; set; }

		public ProgramSource TestVerifier { get; set; }

		public IList<Test> Tests { get; set; }
	}
}