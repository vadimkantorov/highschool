using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public class OutcomeInfoCell : LightweightCell
	{
		OutcomeInfo info;

		public OutcomeInfo Info
		{
			get { return info; }
			set { info = value; }
		}

		public OutcomeInfoCell()
		{}

		public OutcomeInfoCell(int colSpan, OutcomeInfo info) : base(colSpan)
		{
			this.info = info;
		}
	}
}
