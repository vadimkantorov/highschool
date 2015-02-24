using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using Broker.ContestTypeHandlers;
using Model;

namespace Timeline
{
	public class TimelineBuilder
	{
		const int PlotMargin = 40;

		const int YGapBetweenTicks = 30;
		const int YSmallStep = 3;

		const int MinuteStep = 3;
		const int XSmallStep = 10;
		const int XBigStep = 60;
		const int XLastTickPosition = 300;

		const int PlotWidth = 2*PlotMargin + XLastTickPosition * MinuteStep;
		readonly int PlotHeight;

		readonly Color[] contestantColors =
			new[]
			{
				Color.Red,
				Color.Orange,
				Color.Yellow,
				Color.Green,
				Color.Blue,
				Color.Indigo,
				Color.Violet,
				Color.Black,
				Color.DarkRed,
				Color.DarkOrange,
				Color.LightYellow,
				Color.DarkGreen,
				Color.DarkBlue,
				Color.DarkViolet,
				Color.Gold,
			};
		readonly Dictionary<User, Pen> penByContestant = new Dictionary<User, Pen>();
		readonly IList<int> bigSteps = new List<int>();

		public int MaxContestants
		{
			get { return contestantColors.Length; }
		}
		
		public TimelineBuilder(int plotHeight = 1024)
		{
			PlotHeight = plotHeight;
		}

		public Timeline BuildTimeline(IEnumerable<Submission> submissions, int plotHeight = 600)
		{
			var solvedProblemsByContestant = new Dictionary<User, int>();
			var allContestants = new HashSet<User>();

			var points = new List<TimelinePoint>();

			foreach (var s in submissions.Where(x => x.Result.Verdict == GenericVerdict.Accepted.ToString()).OrderBy(x => x.SubmittedAt))
			{
				var p = new TimelinePoint
				{
					Contestant = s.Author,
					Problem = s.Problem,
					SolvedProblemCount = GetDefaulted(solvedProblemsByContestant, s.Author),
					Minute = MinutesFromContestBeginning(s),
					PenaltyTime = MinutesFromContestBeginning(s) //TODO: прикрутить нормальное определение штрафного времени
				};

				points.Add(p);
				Increment(solvedProblemsByContestant, s.Author);
				allContestants.Add(s.Author);
			}
			GeneratePens(allContestants);

			var plot = GeneratePlot(points);
			var legend = GenerateLegend();
			return new Timeline {Plot = plot.Item1, Legend = legend, Tooltips = plot.Item2};
		}

		Tuple<Bitmap, IList<TooltipInfo>> GeneratePlot(IEnumerable<TimelinePoint> points)
		{
			var bitmap = new Bitmap(PlotWidth, PlotHeight);
			var g = Graphics.FromImage(bitmap);
			g.TextRenderingHint = TextRenderingHint.AntiAlias;

			Action<Func<Point, Point>> setTransform = f =>
				g.Transform = new Matrix(
					new Rectangle(0, 0, PlotWidth, PlotHeight),
					new[] { f(new Point(0, 0)), f(new Point(PlotWidth, 0)), f(new Point(0, PlotHeight)) });

			setTransform(p => new Point(PlotMargin + p.X, PlotHeight - 1 - p.Y - PlotMargin));
			var graph = DrawGraph(g, points);
			g.ResetTransform();

			setTransform(p => new Point(p.X, PlotHeight - 1 - p.Y - PlotMargin));
			DrawAxis(g);
			g.ResetTransform();

			return Tuple.Create(bitmap, graph);
		}

		void GeneratePens(IEnumerable<User> allContestants)
		{
			foreach (var c in allContestants)
				penByContestant[c] = new Pen(contestantColors[penByContestant.Count%contestantColors.Length], 3);
		}

		void DrawText(Graphics g, string str, int fontSize, int x, int y, float angle = 0f)
		{
			using (var textPath = new GraphicsPath())
			{
				textPath.AddString(str, FontFamily.GenericSansSerif, (int)FontStyle.Regular, fontSize, new Point(0, 0), StringFormat.GenericTypographic);
				var transformMatrix = new Matrix(
					new Rectangle(0, 0, PlotWidth, PlotHeight),
					new[] { new Point(x, y), new Point(x + PlotWidth, y), new Point(x, y - PlotHeight) });
				transformMatrix.Rotate(angle);
				textPath.Transform(transformMatrix);
				g.FillPath(Brushes.Black, textPath);
			}
		}

		void DrawAxis(Graphics g)
		{
			var pen = new Pen(Color.Black, 1.5f);
			var origin = new Point(PlotMargin, 0);
			var maxX = new Point(PlotMargin + XLastTickPosition * MinuteStep, 0);
			var maxY = new Point(PlotMargin, bigSteps.Sum() + (bigSteps.Count() - 1)*YGapBetweenTicks);
			var axisPen = new Pen(Color.Gray) {DashStyle = DashStyle.DashDotDot};

			int y = 0;
			for (int i = 0; i < bigSteps.Count; i++)
			{
				g.DrawLine(axisPen, origin + new Size(0, y), maxX + new Size(0, y));
				y += bigSteps[i];

				DrawText(g, i.ToString(), 27, 5, y);
				g.DrawLine(axisPen, origin + new Size(0, y), maxX + new Size(0, y));
				y += YGapBetweenTicks;
			}

			const int bigTickHeight = 5;
			for (int x = XSmallStep; x <= XLastTickPosition; x += XSmallStep)
			{
				var realX = x * MinuteStep + PlotMargin;
				var drawBigTick = x%XBigStep == 0;
				if (drawBigTick)
				{
					g.DrawLine(Pens.Black, realX, bigTickHeight, realX, -bigTickHeight);
					DrawText(g, (x/XBigStep).ToString(), 20, realX - 5, -bigTickHeight);
				}
				else
					DrawText(g, (x%XBigStep).ToString(), 13, realX, 0, 45f);
			}
			
			g.DrawLine(pen, origin, maxX);
			g.DrawLine(pen, origin, maxY);
			const int longDelta = 20;
			const int shortDelta = 5;
			g.FillPolygon(Brushes.Gray, new[] { maxY + new Size(-shortDelta, -longDelta), maxY, maxY + new Size(shortDelta, -longDelta) });
			g.FillPolygon(Brushes.Gray, new[] { maxX + new Size(-longDelta, -shortDelta), maxX, maxX + new Size(-longDelta, shortDelta) });
		}

		IList<TooltipInfo> DrawGraph(Graphics graphics, IEnumerable<TimelinePoint> points)
		{
			var lastPointByContestant = new Dictionary<User, Point>();

			const int minBigStep = 60;
			var y = minBigStep + YGapBetweenTicks;
			bigSteps.Add(minBigStep);
			var res = new List<TooltipInfo>();
			foreach (var g in points.OrderBy(x => x.PenaltyTime).GroupBy(x => x.SolvedProblemCount).OrderBy(x => x.Key).ToArray())
			{
				int bigStep = Math.Max(minBigStep, YSmallStep*(g.Count() - 1));
				bigSteps.Add(bigStep);
				int oldY = y;
				int dy;
				if (g.Count() != 1)
				{
					dy = bigStep / (g.Count() - 1);
				}
				else
				{
					dy = bigStep / 2;
					y += dy;
				}
				foreach (var p in g)
				{
					if (p == g.Last() && g.Count() > 1)
						y = oldY + bigStep;
					var x = MinuteStep * p.Minute;

					var pen = penByContestant[p.Contestant];
					var newPoint = new Point(x, y);
					const int markRadius = 10;
					var markCenter = new Point(x - markRadius / 2, y - markRadius / 2);
				
					var lastPoint = GetDefaulted(lastPointByContestant, p.Contestant);
					var intermediary = new Point(x, lastPoint.Y);
					if (lastPoint != default(Point))
						graphics.DrawLine(pen, lastPoint, intermediary);
					graphics.DrawLine(pen, intermediary, newPoint);
					graphics.FillEllipse(pen.Brush, markCenter.X, markCenter.Y, markRadius, markRadius);
					lastPointByContestant[p.Contestant] = newPoint;

					y += dy;

					var ps = new[] {newPoint};
					graphics.Transform.TransformPoints(ps);
					res.Add(new TooltipInfo
						{
							LocationOnPlot = ps[0],
							Minute = p.Minute,
							PenaltyTime = p.PenaltyTime,
							PlotPointRadius = markRadius,
							ProblemShortName = p.Problem.ShortName,
							ProblemDisplayName = p.Problem.Statement.Name,
							UserName = p.Contestant.DisplayName,
						});
				}

				y = oldY + bigStep + YGapBetweenTicks;
			}
			return res;
		}

		Bitmap GenerateLegend()
		{
			var resultSize = new SizeF();
			var iconSize = new Size(20, 20);
			const int gapBetweenIconAndText = 5;
			const int gapBetweenRecords = 3;
			var nameSizes = new Dictionary<User, SizeF>();
			var labelFont = new Font(FontFamily.GenericSansSerif, 15);
			using(var dummyBitmap = new Bitmap(500, 500))
			using (var dummyGraphics = Graphics.FromImage(dummyBitmap))
			{
				foreach (var user in penByContestant.Keys)
				{
					var newSize = nameSizes[user] = dummyGraphics.MeasureString(user.DisplayName, labelFont);
					resultSize.Height += Math.Max(newSize.Height, iconSize.Height) + gapBetweenRecords;
					resultSize.Width = Math.Max(resultSize.Width, newSize.Width + iconSize.Width + gapBetweenIconAndText);
				}
			}
			var result = new Bitmap((int)resultSize.Width, (int)resultSize.Height);
			using (var g = Graphics.FromImage(result))
			{
				g.TextRenderingHint = TextRenderingHint.AntiAlias;
				var y = 0f;
				foreach (var pair in penByContestant.OrderBy(x => x.Key.DisplayName))
				{
					g.FillRectangle(pair.Value.Brush, 0, y, iconSize.Width, iconSize.Height);
					g.DrawString(pair.Key.DisplayName, labelFont, Brushes.Black, iconSize.Width + gapBetweenIconAndText, y);
					y += Math.Max(iconSize.Height, nameSizes[pair.Key].Height) + gapBetweenRecords;
				}
			}
			return result;
		}

		static int MinutesFromContestBeginning(Submission s)
		{
			return (int)(s.SubmittedAt - s.Problem.Contest.Beginning).TotalMinutes;
		}

		static void Increment<TKey>(IDictionary<TKey, int> dic, TKey key)
		{
			dic[key] = GetDefaulted(dic, key) + 1;
		}

		static TValue GetDefaulted<TKey, TValue>(IDictionary<TKey, TValue> dic, TKey key)
		{
			TValue value;
			dic.TryGetValue(key, out value);
			return value;
		}

		class TimelinePoint
		{
			public int SolvedProblemCount { get; set; }
			public int PenaltyTime { get; set; }
			public int Minute { get; set; }
			public User Contestant { get; set; }
			public Problem Problem { get; set; }
		}
	}

	public class TooltipInfo
	{
		public string ProblemShortName;
		public string ProblemDisplayName;
		public string UserName;
		public int PenaltyTime;
		public int Minute;
		public Point LocationOnPlot;
		public int PlotPointRadius;
	}

	public class Timeline
	{
		public Bitmap Plot;
		public Bitmap Legend;
		public IList<TooltipInfo> Tooltips;
	}
}