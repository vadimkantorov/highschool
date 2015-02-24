using System;

namespace Broker
{
	public interface IPrinter
	{
		void PrintText(string text, string waterMark);
	}
}