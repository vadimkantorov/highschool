using Model;

namespace DataAccess.Mappings
{
	public class FormattedDocumentMapping : EntityMapping<FormattedDocument>
	{
		public FormattedDocumentMapping()
		{
			Map(x => x.Name);
			Map(x => x.BodyFormatterId);
			Map(x => x.Body);
		}
	}
}