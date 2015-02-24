using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public class PlaintextCell : LightweightCell
	{
		string text;
		InformationKind kind;

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

		public PlaintextCell() 
			: this(string.Empty)
		{}

		public PlaintextCell(string text)
			: this(1, text, InformationKind.Neutral)
		{}

		public PlaintextCell(int colSpan, string text, InformationKind kind)
			: base(colSpan)
		{
			this.text = text;
			this.kind = kind;
		}
	}
}
