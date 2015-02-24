using System;
using Ini;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;

namespace WinBalans
{
	public class Parameters : ICloneable
	{
		#region Поля
		PluginCollection pluginList;
		Logger l;
		
		UIParameters vp;
        
        bool firstTimeStarted;
        bool timerEnabled;
        double interval;
		#endregion
		
		#region Свойства
		public static Parameters Default
		{
			get
			{
				return new Parameters(PluginCollection.FindPlugins(), 
					UIParameters.Default, false, false, 1000*60*10);
			}
		}
		public bool FirstTimeStarted
		{
			get
			{
				return firstTimeStarted;
			}
			set
			{
				firstTimeStarted = value;
			}
		}
        public bool TimerEnabled
        {
            get
            {
                return timerEnabled;
            }

            set
            {
                timerEnabled = value;
            }
        }
		public Logger LoggerInstance
		{
			get
			{
				return l;
			}

			set
			{
				l = value;
			}
		}

		public double Interval
        {
            get
            {
                return interval;
            }

            set
            {
                interval = value;
            }
        }
        public UIParameters UIParameters
        {
            get
            {
                return vp;
            }

            set
            {
                vp = value;
            }
        }

        public PluginCollection PluginList
        {
            get
            {
                return pluginList;
            }

            set
            {
                pluginList = value;
            }
        }
		#endregion
		public Parameters(PluginCollection pluginList, UIParameters vp, bool firstTimeStarted, bool timerEnabled, double interval)
		{
			this.pluginList = pluginList;
			this.vp = vp;
			this.firstTimeStarted = firstTimeStarted;
			this.timerEnabled = timerEnabled;
			this.interval = interval;
		}
		public static Parameters Parse(Config cn)
		{
			UIParameters pr = new UIParameters();
			bool firstTimeStarted = Default.FirstTimeStarted;
			bool timerEnabled = Default.TimerEnabled;
			double interval = Default.Interval;

			try { firstTimeStarted = Boolean.Parse(cn["[FirstParameters]", "FirstTimeStarted"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { timerEnabled = Boolean.Parse(cn["[FirstParameters]", "TimerEnabled"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { interval = Convert.ToInt32(cn["[FirstParameters]", "Interval"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { double x = Convert.ToDouble(cn["MainWindow", "Opacity"]); pr.Opacity = (x != 0) ? x : UIParameters.Default.Opacity; }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.Color = Color.FromName(cn["MainWindow", "Color"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.TextColor = Color.FromName(cn["MainText", "Color"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.TextFont = new Font(cn["MainText", "FontName"], (float)Convert.ToDouble(cn["MainText", "FontSize"])); }
			catch (ArgumentException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.DesktopLabelCoordinates = new Point(Convert.ToInt32(cn["DesktopText", "X"]), pr.DesktopLabelCoordinates.Y); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.DesktopLabelCoordinates = new Point(pr.DesktopLabelCoordinates.X, Convert.ToInt32(cn["DesktopText", "Y"])); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.DesktopLabelColor1 = Color.FromName(cn["DesktopText", "Color1"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.DesktopLabelColor2 = Color.FromName(cn["DesktopText", "Color2"]); }
			catch (ArgumentNullException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			try { pr.DesktopLabelFont = new Font(cn["DesktopText", "FontName"], ((float)Convert.ToDouble(cn["DesktopText", "FontSize"]))); }
			catch (ArgumentException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }
			catch (FormatException e) { cn.LoggerInstance.WriteLine(e.Message, MessageType.Warning); }

			switch (cn["DesktopText", "Effect"])
			{
				case "BackwardDiagonal":
					pr.DesktopLabelEffect = LinearGradientMode.BackwardDiagonal;
					break;
				case "ForwardDiagonal":
					pr.DesktopLabelEffect = LinearGradientMode.ForwardDiagonal;
					break;
				case "Horizontal":
					pr.DesktopLabelEffect = LinearGradientMode.Horizontal;
					break;
				case "Vertical":
					pr.DesktopLabelEffect = LinearGradientMode.Vertical;
					break;
				default:
					pr.DesktopLabelEffect = UIParameters.Default.DesktopLabelEffect;
					break;
			}
			if(pr.Color.ToArgb() == 0)
				pr.Color = UIParameters.Default.Color;
			if(pr.TextColor.ToArgb() == 0)
				pr.TextColor = UIParameters.Default.TextColor;
			if(pr.DesktopLabelColor1.ToArgb() == 0)
				pr.DesktopLabelColor1 = UIParameters.Default.DesktopLabelColor1;
			if(pr.DesktopLabelColor2.ToArgb() == 0)
				pr.DesktopLabelColor2 = UIParameters.Default.DesktopLabelColor2;
			interval = (interval == 0) ? Default.Interval : interval;
			return new Parameters(PluginCollection.Parse(cn), pr, firstTimeStarted, timerEnabled, interval);
}
		public Config GetConfig(Logger l)
		{
			Config cn = new Config(l);
			cn.BeginSection("[FirstParameters]");
			cn.AddEntry("FirstTimeStarted", firstTimeStarted.ToString());
			cn.AddEntry("TimerEnabled", timerEnabled.ToString());
			cn.AddEntry("Interval", interval.ToString());
				
			cn+=vp.GetConfig(l);
			cn+=pluginList.GetConfig(l);
			return cn;
		}
		public object Clone()
		{
			return new Parameters(pluginList, vp,firstTimeStarted,timerEnabled,interval);
		}
	}
}
