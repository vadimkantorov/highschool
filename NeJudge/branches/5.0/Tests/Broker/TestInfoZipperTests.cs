using System.Collections.Generic;
using Model;
using Xunit;

namespace Broker.Tests
{
	public class TestInfoZipperTests
	{
		[Fact]
		public void zipper_zips_and_unzips_consistently()
		{
			var zipper = new TestsZipper("in", "out");
			IList<Test> tests = new[]
				{
					new Test
						{
							Input = new Blob {Bytes = new byte[] {10, 9, 8, 7, 6, 5, 4, 3, 2, 1}},
							Output = new Blob {Bytes = new byte[] {1, 2, 3}},
						},
					new Test
						{
							Input = new Blob {Bytes = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}},
							Output = new Blob {Bytes = new byte[] {3, 2, 1}},
						},
				};

			var zip = zipper.ZipTests(tests);
			var newTests = zipper.UnzipTests(zip);

			Assert.Equal(tests.Count, newTests.Count);
			for (int i = 0; i < tests.Count; i++)
			{
				Assert.Equal(tests[i].Input.Bytes, newTests[i].Input.Bytes);
				Assert.Equal(tests[i].Output.Bytes, newTests[i].Output.Bytes);
			}
		}
	}
}