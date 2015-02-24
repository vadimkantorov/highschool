using System;

namespace Ne.Database
{
	public class Contest
	{
		private DateTime beginning;
		private DateTime ending;
		private string name;

		public Contest(DateTime beginning, DateTime ending, string name)
		{
			this.beginning = beginning;
			this.ending = ending;
			this.name = name;
		}

		public DateTime Beginning
		{
			get { return beginning; }
		}

		public DateTime Ending
		{
			get { return ending; }
		}

		public string Name
		{
			get { return name; }
		}

		public bool Old
		{
			get
			{
				if (ending < DateTime.Now)
					return true;
				return false;
			}
		}

		public bool Now
		{
			get
			{
				if (beginning < DateTime.Now && ending > DateTime.Now)
					return true;
				return false;
			}
		}

		public bool Future
		{
			get
			{
				if (beginning > DateTime.Now)
					return true;
				return false;
			}
		}
	}
}