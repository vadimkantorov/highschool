using System;
using System.IO;
using Broker.Interfaces;
using Model.Utils;

namespace Broker
{
	public class FileSystemArchivedTestInfoRepository : IArchivedTestInfoRepository
	{
		public string StoreTestInfoArchive(Guid testInfoId, byte[] zipArchive)
		{
			var fileName = GetFileName(testInfoId);
			
			var filePath = Path.Combine(baseDirectory, fileName);
			var webPath = Path.Combine(webDirectory, fileName);
			
			File.WriteAllBytes(filePath, zipArchive);
			return webPath;
		}

		public FileSystemArchivedTestInfoRepository(string baseDirectory, string webDirectory)
		{
			if (!Directory.Exists(baseDirectory))
				Directory.CreateDirectory(baseDirectory);
			
			this.baseDirectory = baseDirectory;
			this.webDirectory = webDirectory;
		}

		static string GetFileName(Guid g)
		{
			return g +  "-" + StringUtils.GenerateRandomLatinString(10) + ".zip";
		}

		readonly string baseDirectory;
		readonly string webDirectory;
	}
}