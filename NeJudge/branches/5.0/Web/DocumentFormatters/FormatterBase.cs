using Model.Factories;

namespace Web.DocumentFormatters
{
	public abstract class FormatterBase : NamedBase, IFormatter
	{
		public abstract string RenderHtml(string innerRepresentation);
	}
}