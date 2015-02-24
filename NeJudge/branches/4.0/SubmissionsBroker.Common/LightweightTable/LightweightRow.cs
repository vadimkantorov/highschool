using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Ne.DomainModel
{
	[Serializable]
	public class LightweightRow : IEnumerable<LightweightCell>
	{
		bool isAlternateStyle;
		List<LightweightCell> cells;

		public bool IsAlternateStyle
		{
			get { return isAlternateStyle; }
			set { isAlternateStyle = value; }
		}

		public List<LightweightCell> Cells
		{
			get { return cells; }
		}

		public int ColumnCount
		{
			get
			{
				int ret = 0;

				foreach ( LightweightCell cell in cells )
					ret += cell.ColSpan;

				return ret;
			}
		}

		public LightweightCell this[int index]
		{
			get { return cells[index]; }
		}

		public LightweightRow()
		{
			cells = new List<LightweightCell>();
		}

		#region IEnumerable<LightweightCell> Members

		public IEnumerator<LightweightCell> GetEnumerator()
		{
			foreach ( LightweightCell cell in cells )
				yield return cell;
		}

		#endregion

		#region IEnumerable Members

		IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion
	}
}
