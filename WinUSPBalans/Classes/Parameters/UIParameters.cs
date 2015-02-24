using System;
using Ini;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Microsoft.Win32;

namespace WinBalans
{
	public class UIParameters :ICloneable
    {
        #region Поля
        double opacity;
        Color color;
        Font textFont;
        Color textColor;

        LinearGradientMode desktopLabelEffect;
        Color desktopLabelColor1;
        Color desktopLabelColor2;
        Point desktopLabelCoordinates;
		

		Font desktopFont;
		/*public static readonly UIParameters Default = new UIParameters(0.5,Color.Brown,
			new Font("Microsoft Sans Serif", 12.75F),Color.Black,LinearGradientMode.Horizontal,Color.Red, Color.Yellow, 700.0F,10.0F,new Font("Times New Roman", 24));*/
        #endregion
        #region Свойства
        public double Opacity
        {
            get
            {
                return opacity;
            }

            set
            {
                opacity = value;
            }
        }
        public Color Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }
        public Font TextFont
        {
            get
            {
                return textFont;
            }

            set
            {
                textFont = value;
            }
        }
        public Color TextColor
        {
            get
            {
                return textColor;
            }

            set
            {
                textColor = value;
            }
        }
        
        public LinearGradientMode DesktopLabelEffect
        {
            get
            {
                return desktopLabelEffect;
            }

            set
            {
                desktopLabelEffect = value;
            }
        }
        public Color DesktopLabelColor1
        {
            get
            {
                return desktopLabelColor1;
            }

            set
            {
                desktopLabelColor1 = value;
            }
        }
        public Color DesktopLabelColor2
        {
            get
            {
                return desktopLabelColor2;
            }

            set
            {
                desktopLabelColor2 = value;
            }
        }
        public Font DesktopLabelFont
        {
            get
            {
                return desktopFont;
            }

            set
            {
                desktopFont = value;
            }
        }
		public Point DesktopLabelCoordinates
		{
			get
			{
				return desktopLabelCoordinates;
			}

			set
			{
				desktopLabelCoordinates = value;
			}
		}
		public static UIParameters Default
		{
			get
			{
				return new UIParameters(0.5, Color.Brown,new Font("Microsoft Sans Serif", 12.75F), Color.Black, LinearGradientMode.Horizontal, Color.Red, Color.Yellow, new Point(700, 10), new Font("Times New Roman", 24));
			}
		}
        #endregion
		public UIParameters(double opacity, Color color, Font textFont, Color textColor,		
							  LinearGradientMode desktopLabelEffect, Color desktopLabelColor1, Color desktopLabelColor2,
							  Point desktopLabelCoordinates, Font desktopLabelFont)
		{
			this.opacity		= opacity;
			this.color			= color;
			this.textFont		= textFont;
			this.textColor		= textColor;
								
			this.desktopLabelEffect	= desktopLabelEffect;
			this.desktopLabelColor1	= desktopLabelColor1;
			this.desktopLabelColor2	= desktopLabelColor2;
			this.desktopLabelCoordinates		= desktopLabelCoordinates;
			this.desktopFont	= desktopLabelFont;
		}
		public UIParameters()
		{
			this.opacity		= Default.opacity;		
			this.color			= Default.color;		
			this.textFont		= Default.textFont;		
			this.textColor		= Default.textColor;		
										  
			this.desktopLabelEffect	= Default.desktopLabelEffect;	
			this.desktopLabelColor1	= Default.desktopLabelColor1;	
			this.desktopLabelColor2	= Default.desktopLabelColor2;
			this.desktopLabelCoordinates = Default.desktopLabelCoordinates;
			this.desktopFont	= Default.desktopFont;	
		}
		public object Clone()
		{
			return new UIParameters(opacity, color, textFont, textColor, desktopLabelEffect, desktopLabelColor1, desktopLabelColor2,
									new Point(desktopLabelCoordinates.X,desktopLabelCoordinates.Y),desktopFont);
		}
		public Config GetConfig(Logger l)
		{
			Config cn = new Config(l);
			
			cn.BeginSection("[MainWindow]");
				cn.AddEntry("Opacity", opacity.ToString());
				cn.AddEntry("Color",color.Name);

			cn.BeginSection("[MainText]");
				cn.AddEntry("FontName", textFont.Name);
				cn.AddEntry("FontSize",textFont.Size.ToString());
				cn.AddEntry("Color", textColor.Name);

			cn.BeginSection("[DesktopText]");
				cn.AddEntry("FontName", desktopFont.Name);
				cn.AddEntry("FontSize",desktopFont.Size.ToString());
				cn.AddEntry("Color1", desktopLabelColor1.Name);
				cn.AddEntry("Color2", desktopLabelColor2.Name);
				cn.AddEntry("X",desktopLabelCoordinates.X.ToString());
				cn.AddEntry("Y",desktopLabelCoordinates.Y.ToString());
				cn.AddEntry("Effect",desktopLabelEffect.ToString());
			return cn;

		}
	}
}