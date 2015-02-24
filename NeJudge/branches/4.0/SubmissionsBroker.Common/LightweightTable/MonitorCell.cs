using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public class MonitorCell : LightweightCell
	{
		bool showTime;
		TimeSpan time;
		string text;
		InformationKind kind;

		public bool ShowTime
		{
			get { return showTime; }
			set { showTime = value; }
		}

		public TimeSpan Time
		{
			get { return time; }
			set { time = value; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public InformationKind Kind
		{
			get { return kind; }
			set { kind = value; }
		}

		public MonitorCell()
		{}

		public MonitorCell(int colSpan, bool showTime, TimeSpan time, string cellText, InformationKind kind) : base(colSpan)
		{
			this.showTime = showTime;
			this.time = time;
			this.text = cellText;
			this.kind = kind;
		}
	}
}
