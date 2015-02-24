using System;
using Microsoft.Win32;

namespace WinBalans
{
	public class SettingsClass
	{
		public ArrayList pluginInfoList;
		public ViewParameters vp;
		public int firstTimeStarted;
		public int timerEnabled;
		public double interval;


		public SettingsClass()
		{
		}
		public SettingsClass(string ipathToViewParameters, string ipathToCurrentPluginInfo,
			int ifirstTimeStarted)
		{
			pathToViewParameters = ipathToViewParameters;
			pathToCurrentPluginInfo = ipathToCurrentPluginInfo;
			firstTimeStarted = ifirstTimeStarted;
		}
		public Config GetConfig()
		{
			Config cn = new Config();
			cn.BeginSection("[FirstParameters]");
				cn.AddEntry("FirstTimeStarted", firstTimeStarted.ToString());
				cn.AddEntry("TimerEnabled", timerEnabled);
				cn.AddEntry("Interval", interval);
				
			cn+=vp.GetConfig();

			foreach(PluginInfo pi in pluginInfoList)
				cn+=pi.GetConfig();
			
			return cn;
		}
		/*public static FirstParameters CreateDefaultSettings(string directoryToCurrentPluginInfo)
		{
			FirstParameters fp = new FirstParameters();
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Shell folders");
			fp.pathToViewParameters = rk.GetValue("AppData").ToString()+@"\WinBalans\";
			fp.pathToCurrentPluginInfo = directoryToCurrentPluginInfo+"CyverAss.ini";
			fp.firstTimeStarted = 0;
			return fp;
		}*/

		public void Save(string directory)
		{
			this.GetConfig().Save(directory,"Settings");
		}
		public static FirstParameters Load(string directory)
		{
			return Config.Load(directory + "Settings.ini").GetFirstParameters();
		}
	}
}
