using System;
using Model.Testing;

namespace Model
{
	public class Problem : Entity
	{
		public ResourceUsage Limits { get; set; }
		
		public string ShortName { get; set; }

		public FormattedDocument Statement { get; set; }

		public TestInfo TestInfo { get; set; }

		public Contest Contest { get; set; }

		public Problem()
		{
			//TODO: really create checker and test verifier even if they don't exist?
			TestInfo = new TestInfo {Checker = new ProgramSource(), TestVerifier = new ProgramSource()};
			Limits = new ResourceUsage();
		}
	}
}