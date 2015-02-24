using Model;

namespace DataAccess.Mappings
{
	public class BlobMapping : EntityMapping<Blob>
	{
		public BlobMapping()
		{
			Map(x => x.Bytes).CustomType("BinaryBlob").CustomSqlType("varbinary(max)");
		}
	}
}