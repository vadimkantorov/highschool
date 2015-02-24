using System.Drawing;
using System.Drawing.Drawing2D;

namespace Timeline
{
	public static class GraphicsExtensions
	{
		public static void DrawTextTransformSafe(this Graphics g, string str, int fontSize, int x, int y, float angle = 0f)
		{
			using (var textPath = new GraphicsPath())
			{
				textPath.AddString(str, FontFamily.GenericSansSerif, (int)FontStyle.Regular, fontSize, new Point(0, 0), StringFormat.GenericTypographic);
				var transformMatrix = new Matrix();
				transformMatrix.Translate(x,y);
				transformMatrix.Scale(1,-1);
				transformMatrix.Rotate(angle);
				textPath.Transform(transformMatrix);
				g.FillPath(Brushes.Black, textPath);
			}
		}
	}
}