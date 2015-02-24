using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ZedGraph;

namespace ThermoTools
{
	partial class Visualizer : Form
	{
		FrameProcessor fp;

		public Visualizer(FrameProcessor fp)
		{
			this.fp = fp;
			InitializeComponent();
		}

		PointPairList GetCurve()
		{
			PointPairList pl = new PointPairList();

			IList<Pair<DateTime,double>> ps = fp.GetData();
			foreach(Pair<DateTime,double> p in ps)
			{
				pl.Add(( p.First - ps[0].First ).TotalMilliseconds/1000.0, p.Second);
			}
			return pl;
		}

		void Redraw()
		{
			GraphPane pane = graph.GraphPane;
			pane.CurveList.Clear();
			LineItem curve = pane.AddCurve("Данные", GetCurve(), Color.Red, SymbolType.None);
			
			curve.Line.IsSmooth = true;
			curve.Line.SmoothTension = (float)nudTension.Value;
			// Fill the symbols with white
			curve.Symbol.Fill = new Fill(Color.White);

			if( curve.Points.Count > 0 )
			{
				pane.XAxis.Scale.Min = curve.Points[0].X;
				pane.XAxis.Scale.Max = curve.Points[curve.NPts - 1].X;
				//pane.XAxis.Scale.MinAuto = true;
				//pane.XAxis.Scale.MaxAuto = true;
				graph.AxisChange();
			}
			
			// Make sure the Graph gets redrawn
			graph.Invalidate();
		}
		
		private void Form1_Load(object sender, EventArgs e)
		{
			// Get a reference to the GraphPane instance in the ZedGraphControl
			GraphPane pane = graph.GraphPane;

			// Set the titles and axis labels
			pane.Title.Text = "Визуализация данных АЦП";
			pane.XAxis.Title.Text = "Время (с)";
			pane.YAxis.Title.Text = "Температура (°C)";

			// Show the x axis grid
			pane.XAxis.MajorGrid.IsVisible = true;
			pane.XAxis.Scale.MajorUnit = DateUnit.Second;
			pane.XAxis.Scale.MajorStep = 1;
			pane.XAxis.Scale.MinorStep = 0.1;
		
			// Make the Y axis scale red
			pane.YAxis.Scale.FontSpec.FontColor = Color.Red;
			pane.YAxis.Title.FontSpec.FontColor = Color.Red;
			// turn off the opposite tics so the Y tics don't show up on the Y2 axis
			pane.YAxis.MajorTic.IsOpposite = false;
			pane.YAxis.MinorTic.IsOpposite = false;
			// Don't display the Y zero line
			pane.YAxis.MajorGrid.IsVisible = true;
			pane.YAxis.MinorGrid.IsVisible = false;
			pane.YAxis.MajorGrid.IsZeroLine = false;
			pane.YAxis.Scale.MajorStep = 10;
			pane.YAxis.Scale.MinorStep = pane.YAxis.Scale.MajorStep;
			
			// Align the Y axis labels so they are flush to the axis
			pane.YAxis.Scale.Align = AlignP.Inside;
			// Manually set the axis range
			pane.YAxis.Scale.Min = -40;
			pane.YAxis.Scale.Max = 150;


			// Enable scrollbars if needed
			graph.IsShowHScrollBar = true;
			graph.IsShowVScrollBar = true;
			graph.IsAutoScrollRange = true;

			// OPTIONAL: Show tooltips when the mouse hovers over a point
			graph.IsShowPointValues = true;

			// Tell ZedGraph to calculate the axis ranges
			// Note that you MUST call this after enabling IsAutoScrollRange, since AxisChange() sets
			// up the proper scrolling parameters
			graph.AxisChange();

			Redraw();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			sfd.InitialDirectory = Environment.CurrentDirectory;
			if( sfd.ShowDialog() == DialogResult.OK )
				fp.SaveToFile(sfd.FileName);
		}
	}
}