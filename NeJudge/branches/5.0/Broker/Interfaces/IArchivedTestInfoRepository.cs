using System;

namespace Broker.Interfaces
{
	public interface IArchivedTestInfoRepository
	{
		string StoreTestInfoArchive(Guid testInfoId, byte[] zipArchive);
	}
}