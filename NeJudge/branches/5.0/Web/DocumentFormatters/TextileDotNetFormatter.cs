using Textile;

namespace Web.DocumentFormatters
{
	public class TextileDotNetFormatter : FormatterBase
	{
		public override string RenderHtml(string innerRepresentation)
		{
			var outputter = new StringBuilderTextileFormatter();
			var renderer = new TextileFormatter(outputter);
			renderer.Format(innerRepresentation);
			return outputter.GetFormattedText();
		}
	}
}