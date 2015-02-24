using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public class LightweightTable
	{
		LightweightRow headerRow;
		List<LightweightRow> rows;

		public int ColumnCount
		{
			get { return HeaderRow.ColumnCount; }
		}

		public LightweightRow HeaderRow
		{
			get { return headerRow; }
		}

		public List<LightweightRow> Rows
		{
			get { return rows; }
		}

		public LightweightRow this[int index]
		{
			get { return Rows[index]; }
		}

		public LightweightTable()
		{
			headerRow = new LightweightRow();
			rows = new List<LightweightRow>();
		}

		public void ApplyDefaultRowStyles()
		{
			for ( int i = 0; i < Rows.Count; ++i )
				Rows[i].IsAlternateStyle = i % 2 == 1;
		}
	}
}
