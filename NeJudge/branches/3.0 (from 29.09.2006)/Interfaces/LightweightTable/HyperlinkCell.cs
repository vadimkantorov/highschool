using System;
using System.Collections.Generic;
using System.Text;

namespace Ne.Interfaces
{
	[Serializable]
	public class HyperlinkCell : LightweightCell
	{
		public enum LinkObjectType
		{
			Submission,
			Problem,
			Monitor,
			Contest
		}

		LinkObjectType linkType;
		int linkObjectID;
		string text;

		public LinkObjectType LinkType
		{
			get { return linkType; }
			set { linkType = value; }
		}

		public int LinkObjectID
		{
			get { return linkObjectID; }
			set { linkObjectID = value; }
		}

		public string Text
		{
			get { return text; }
			set { text = value; }
		}

		public HyperlinkCell()
		{ }

		public HyperlinkCell(int colSpan, LinkObjectType linkObjectType, int linkObjectID)
			: base(colSpan)
		{
			this.linkType = linkObjectType;
			this.linkObjectID = linkObjectID;
		}
	}
}
