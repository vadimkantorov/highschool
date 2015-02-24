using System;
using Microsoft.Win32;

namespace WinBalans
{
	public class TempParameters
	{
		public readonly string firstpath;
		public readonly string secondpath; 
		public readonly string basedirectory;
		public TempParameters()
		{
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("Control Panel").OpenSubKey("Desktop");
			RegistryKey rk2 = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Shell folders");
			this.firstpath = rk.GetValue("Wallpaper").ToString();
			this.secondpath  = @"C:\WINDOWS\Web\Wallpaper\WinBalans.bmp";
			this.basedirectory = System.AppDomain.CurrentDomain.BaseDirectory;
		}
	}
}
