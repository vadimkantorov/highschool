using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	public class Language
	{
		#region Fields

		string name;
		string extension;

		string id;

		#endregion

		#region Properties

		public string Name
		{
			get { return name; }
			set { name = value; }
		}

		public string Extension
		{
			get { return extension; }
			set { extension = value; }
		}

		#endregion
	}
}
