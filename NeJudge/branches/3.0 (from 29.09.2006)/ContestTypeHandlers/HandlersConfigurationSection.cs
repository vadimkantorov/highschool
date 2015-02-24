using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Ne.ContestTypeHandlers
{
	class HandlersConfigurationSection : ConfigurationSection
	{
		const string HandlersDirectoryKey = "handlersDirectory";
		
		[ConfigurationProperty(HandlersDirectoryKey, IsRequired = true)]
		public string HandlersDirectory
		{
			get { return (string) this[HandlersDirectoryKey]; }
			set { this[HandlersDirectoryKey] = value; }
		}
	}
}
