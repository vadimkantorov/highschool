using System;
using System.Collections.Generic;

namespace Model
{
	public class User : Entity
	{
		public string DisplayName { get; set; }

		public string UserName { get; set; }

		public Password Password { get; set; }
	}
}
