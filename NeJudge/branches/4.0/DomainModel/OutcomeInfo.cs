using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	public class OutcomeInfo //: IComparable<OutcomeInfo>
	{
		string printableValue;
		InformationKind kind;

		public string PrintableValue
		{
			get { return printableValue; }
		}

		public InformationKind Kind
		{
			get { return kind; }
		}

		public OutcomeInfo()
			: this("", InformationKind.Neutral)
		{}

		public OutcomeInfo(string printableValue, InformationKind kind)
		{
			this.kind = kind;
			this.printableValue = printableValue;
		}

		#region IComparable<OutcomeInfo> Members

		/*public int CompareTo(OutcomeInfo other)
		{
			return printableValue.CompareTo(other.printableValue);
		}*/

		#endregion
	}
}
