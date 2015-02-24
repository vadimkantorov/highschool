using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public abstract class LightweightCell
	{
		int colSpan;

		public int ColSpan
		{
			get { return colSpan; }
			set { colSpan = value; }
		}

		protected LightweightCell()
			: this(1)
		{ }

		protected LightweightCell(int colSpan)
		{
			this.ColSpan = colSpan;
		}
	}
}
