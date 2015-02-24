using System.Collections.Generic;
using Model;

namespace Web.ViewModels
{
	public class MessagesForm
	{
		public IEnumerable<MessageReading> Messages { get; set; }

		public Contest Contest { get; set; }
	}
}