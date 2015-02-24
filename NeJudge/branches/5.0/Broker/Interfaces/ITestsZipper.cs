using System.Collections.Generic;
using Model;

namespace Broker.Interfaces
{
	public interface ITestsZipper
	{
		byte[] ZipTests(IList<Test> testInfo);

		IList<Test> UnzipTests(byte[] zipArchive);
	}
}