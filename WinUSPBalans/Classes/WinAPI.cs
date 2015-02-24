using System;
using System.Runtime.InteropServices;

namespace WinBalans
{
	public class WinAPI
	{
		const int SPI_SETDESKWALLPAPER = 0x0014;
		const int SPI_GETDESKWALLPAPER = 0x0073;
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		private static extern int SystemParametersInfo (int uiAction , int uiParam , string pvParam , int fWinIni) ;
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern int GetSystemMetrics(int nIndex);
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr GetDesktopWindow();
		public static int XResolution
		{
			get
			{
				return GetSystemMetrics(0);
			}
		}
		public static int YResolution
		{
			get
			{
				return GetSystemMetrics(1);
			}
		}
		public static System.Drawing.Graphics Desktop
		{
			get
			{
				return System.Drawing.Graphics.FromHwnd(WinAPI.GetDesktopWindow());
			}
		}
		public static void ChangeWalpaper(string path)
		{
			SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, path, 0x1 | 0x2);//20
		}
		
	} 
}
