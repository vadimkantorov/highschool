using System.IO;
using System.Reflection;

namespace Tests
{
	public class RsbTestFixture
	{
		public RsbTestFixture()
		{
			var baseDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			foreach (var dir in Directory.GetDirectories(baseDirectory, "*.esent"))
				Directory.Delete(dir, true);
		}
	}
}