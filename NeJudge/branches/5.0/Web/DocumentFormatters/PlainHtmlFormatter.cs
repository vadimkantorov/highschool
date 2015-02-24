using System;

namespace Web.DocumentFormatters
{
	public class PlainHtmlFormatter : FormatterBase
	{
		public override string RenderHtml(string innerRepresentation)
		{
			return innerRepresentation;
		}
	}
}