using System;
using System.IO;
using System.Configuration;

namespace Ne.Tester
{
	public class TestingDirectoryInfo
	{
		string rootDir;
		string sandboxDir;
		string archiveDir;
		string checkerWorkDir;

		public string CheckerDir
		{
			get { return checkerWorkDir; }
			set { checkerWorkDir = value; }
		}

		public string RootDir
		{
			get { return rootDir; }
			set { rootDir = value; }
		}

		public string ArchiveDir
		{
			get { return archiveDir; }
			set { archiveDir = value; }
		}

		public string SandboxDir
		{
			get { return sandboxDir; }
			set { sandboxDir = value; }
		}

		public TestingDirectoryInfo()
		{
			rootDir = Path.Combine(NeTesterConfiguration.RootDir, Guid.NewGuid().ToString());
			Directory.CreateDirectory(rootDir);

			sandboxDir = Path.Combine(rootDir, "Sandbox");
			Directory.CreateDirectory(sandboxDir);

			archiveDir = Path.Combine(rootDir, "Archive");
			Directory.CreateDirectory(archiveDir);

			checkerWorkDir = Path.Combine(rootDir, "Checker");
			Directory.CreateDirectory(checkerWorkDir);
		}
	}
}