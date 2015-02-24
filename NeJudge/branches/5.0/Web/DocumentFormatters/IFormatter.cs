using System;
using Model.Factories;

namespace Web.DocumentFormatters
{
	public interface IFormatter : INamed
	{
		string RenderHtml(string innerRepresentation);
	}
}