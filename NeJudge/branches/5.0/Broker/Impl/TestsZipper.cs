using System;
using System.Collections.Generic;
using System.IO;
using Broker.Interfaces;
using ICSharpCode.SharpZipLib.Zip;
using Model;

namespace Broker
{
	public class TestsZipper : ITestsZipper
	{
		public TestsZipper(string inputExt, string outputExt)
		{
			this.inputExt = inputExt;
			this.outputExt = outputExt;
		}

		public byte[] ZipTests(IList<Test> tests)
		{
			using (var ms = new MemoryStream())
			{
				using (var zip = new ZipOutputStream(ms))
				{
					ZipCollection(zip, inputExt, tests, t => t.Input.Bytes);
					ZipCollection(zip, outputExt, tests, t => t.Output.Bytes);
				}
				return ms.ToArray();
			}
		}

		public IList<Test> UnzipTests(byte[] zipArchive)
		{
			var tests = new List<Test>();
			using (var zip = new ZipFile(new MemoryStream(zipArchive)))
			{
				var dic = new SortedDictionary<string, Test>();
				UnzipCollection(zip, inputExt, dic, (t, bs) => t.Input = new Blob{Bytes = bs});
				UnzipCollection(zip, outputExt, dic, (t, bs) => t.Output = new Blob{Bytes = bs});
				foreach (var kvp in dic)
				{
					kvp.Value.Description = string.Format("{0} от {1}", kvp.Key, DateTime.Now);
					tests.Add(kvp.Value);
				}
			}
			return tests;
		}

		void UnzipCollection(ZipFile zip, string ext, IDictionary<string, Test> dic, Action<Test, byte[]> setter)
		{
			if(ext != "")
				ext = "." + ext;
			foreach (ZipEntry entry in zip)
			{
				if(!entry.IsDirectory && Path.GetExtension(entry.Name) == ext)
				{
					var withoutExt = Path.GetFileNameWithoutExtension(entry.Name);
					if (!dic.ContainsKey(withoutExt))
						dic[withoutExt] = new Test();

					var bytes = new byte[entry.Size];
					zip.GetInputStream(entry).Read(bytes, 0, bytes.Length);
					setter(dic[withoutExt], bytes);
				}
			}
		}

		void ZipCollection(ZipOutputStream zip, string ext, IList<Test> col, Func<Test, byte[]> f)
		{
			for (int i = 0; i < col.Count; i++)
			{
				var name = string.Format("{0:D3}.{1}", i+1, ext);
				zip.PutNextEntry(new ZipEntry(name));

				var bytes = f(col[i]);
				zip.Write(bytes, 0, bytes.Length);
			}
		}

		readonly string inputExt;
		readonly string outputExt;
	}
}