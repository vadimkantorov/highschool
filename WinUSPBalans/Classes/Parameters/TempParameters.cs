using System;
using WinBalans.Forms;
using Microsoft.Win32;
using System.Collections;

namespace WinBalans
{
	public abstract class TempParameters
	{
		static string firstpath = Desktop;
		public static string BaseDirectory
		{
			get
			{
				return AppDomain.CurrentDomain.BaseDirectory;
			}
		}
		public static string Desktop
		{
			get
			{
				RegistryKey rk = Registry.CurrentUser.OpenSubKey("Control Panel").OpenSubKey("Desktop");
				return rk.GetValue("Wallpaper").ToString();
			}
		}
		public static string Secondpath
		{
			get
			{
				//RegistryKey rk2 = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Shell folders");
				return @"C:\WINDOWS\Web\Wallpaper\WinBalans.bmp";
			}
		}
		public static string Firstpath
		{
			get
			{
				return firstpath;
			}
			set
			{
				firstpath = value;
			}
		}
		public static Hashtable FormList
		{
			get
			{
				return TempParameters.formList;
			}

			set
			{
				TempParameters.formList = value;
			}
		}
		private static Hashtable formList = new Hashtable();
		public static Parameters ParametersInstance
		{
			get
			{
				return TempParameters.parametersInstance;
			}

			set
			{
				TempParameters.parametersInstance = value;
			}
		}

		private static Parameters parametersInstance;
		

		public static void CreateWindow(Plugin pl)
		{
			pl.Checked = true;
			PluginForm f = new PluginForm(pl);
			if (!FormList.Contains(pl))
			{
				FormList.Add(pl, f);
				f.Show();
			}
		}
	}
}
