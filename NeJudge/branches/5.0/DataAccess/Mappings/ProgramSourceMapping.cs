using Model;

namespace DataAccess.Mappings
{
	public class ProgramSourceMapping : EntityMapping<ProgramSource>
	{
		public ProgramSourceMapping()
		{
			Map(x => x.LanguageId);
			Map(x => x.Code).CustomType("StringClob").CustomSqlType("nvarchar(max)");
		}
	}
}