using System;
using System.IO;
using Xunit;

namespace Broker.Tests
{
	public class TestInfoArchiveRepositoryTests
	{
		/*[Fact]
		public void store_commits_to_the_file_system()
		{
			const string tempDir = "testDir";
			var someGuid = new Guid("41D68F1E-B611-41B1-9D46-3DC15C39AE88");

			if(Directory.Exists(tempDir))
				Directory.Delete(tempDir, true);

			Directory.CreateDirectory(tempDir);
			Assert.True(Directory.GetFiles(tempDir).Length == 0);
			
			var rep = new FileSystemArchivedTestInfoRepository(tempDir);
			Assert.False(rep.HasArchiveFor(someGuid));

			rep.StoreTestInfoArchive(someGuid, new byte[] {1,2,3});
            Assert.True(rep.HasArchiveFor(someGuid));

			Assert.True(Directory.GetFiles(tempDir).Length > 0);
		}*/

		[Fact]
		public void creates_base_directory_if_it_doesnt_exist()
		{
			const string dir = "foobar";
			if(Directory.Exists(dir))
				Directory.Delete(dir, true);
			
			new FileSystemArchivedTestInfoRepository(dir, "");
			Assert.True(Directory.Exists(dir));
		}
	}
}