using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinBalans
{
	public class ViewParameters
	{
		public double MOpacity;
		public Color MColor;
				
		public LinearGradientMode DeskEffect;
		public Color DeskColor1;
		public Color DeskColor2;
		public float DeskX;
		public float DeskY;
		public Font DeskFont;
		
		public Font TextFont;
		public Color TextColor;
		
		public ViewParameters()
		{
		}
		public ViewParameters(double PMOpacity,Color PMColor,LinearGradientMode PDeskEffect,
			Color PDeskColor1,Color PDeskColor2,float PDeskX, float PDeskY,Font PDeskFont,
			Font PTextFont,Color PTextColor)
		{
			MOpacity = PMOpacity;
			MColor = PMColor;
			DeskEffect = PDeskEffect;
			DeskColor1 = PDeskColor1;
			DeskColor2 = PDeskColor2;
			DeskX = PDeskX;
			DeskY = PDeskY;
			DeskFont = PDeskFont;
			TextFont = PTextFont;
			TextColor = PTextColor;
		}
		
		public Config GetConfig()
		{
			Config cn = new Config();
			
			cn.BeginSection("[MainWindow]");
				cn.AddEntry("Opacity", MOpacity.ToString());
				cn.AddEntry("Color",MColor.ToString());

			cn.BeginSection("[MainText]");
				cn.AddEntry("FontName", TextFont.FontFamily.ToString());
				cn.AddEntry("FontSize",TextFont.Size.ToString());
				cn.AddEntry("Color", TextColor.ToString());

			cn.BeginSection("[DesktopText]");
				cn.AddEntry("FontName", DeskFont.FontFamily.ToString());
				cn.AddEntry("FontSize",DeskFont.Size.ToString());
				cn.AddEntry("Color1", DeskColor1.ToString());
				cn.AddEntry("Color2", DeskColor2.ToString());
				cn.AddEntry("X",DeskX.ToString());
				cn.AddEntry("Y",DeskY.ToString());
				cn.AddEntry("Effect",DeskEffect.ToString());
			return cn;
		}
		public static ViewParameters CreateDefaultViewParameters()
		{
			ViewParameters pm = new ViewParameters();
						  
			pm.DeskColor1 = Color.Red;
			pm.DeskColor2 = Color.Yellow;
			pm.DeskEffect = LinearGradientMode.Horizontal;
			pm.DeskFont = new Font("Times New Roman", 24);
			pm.DeskX = 700.0F;
			pm.DeskY = 10.0F;
			  
			pm.MColor = Color.Brown;
			pm.MOpacity = 0.5;
			
			pm.TextColor = Color.Black;
			pm.TextFont = new Font("Microsoft Sans Serif", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			return pm;
		}
		public void Save(string directory)
		{
			Config cn = this.GetConfig();
			cn.Save(directory, "ViewParameters");
		}
		public static ViewParameters Load(string directory)
		{
			return Config.Load(directory + "ViewParameters.ini").GetViewParameters();
		}
	}
}