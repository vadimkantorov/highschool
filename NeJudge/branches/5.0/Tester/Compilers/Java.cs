using System;
using Tester.Runner;

namespace Tester.Compilers
{
	public class Java : CompilerBase
	{
		protected override string SourceFileName
		{
			get { return "Main.java"; }
		}

		protected override string OutputFileName
		{
			get { return "Main.class"; }
		}

		protected override string CompilerExecutable
		{
			get { return JavaPaths.Javac; }
		}

		protected override string CompilerArguments
		{
			get { return ""; }
		}

		public override string Name
		{
			get { return "Sun Java"; }
		}

		public override bool ShowToContestants
		{
			get { return true; }
		}
	}
}