using System;
using Microsoft.Win32;

namespace WinBalans
{
	public class FirstParameters
	{
		public string pathToViewParameters;
		//public string pathToCurrentPluginInfoList;
		public string pathToCurrentPluginInfo;
		public int firstTimeStarted;

		public FirstParameters()
		{
		}
		public FirstParameters(string ipathToViewParameters, string ipathToCurrentPluginInfo,
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
				cn.AddEntry("PathToViewParameters", pathToViewParameters);	
				cn.AddEntry("PathToCurrentPluginInfo", pathToCurrentPluginInfo);
				cn.AddEntry("FirstTimeStarted", firstTimeStarted.ToString());
			return cn;
		}
		public static FirstParameters CreateDefaultFirstParameters(string directoryToCurrentPluginInfo)
		{
			FirstParameters fp = new FirstParameters();
			RegistryKey rk = Registry.CurrentUser.OpenSubKey("Software").OpenSubKey("Microsoft").OpenSubKey("Windows").OpenSubKey("CurrentVersion").OpenSubKey("Explorer").OpenSubKey("Shell folders");
			fp.pathToViewParameters = rk.GetValue("AppData").ToString()+@"\WinBalans\";
			fp.pathToCurrentPluginInfo = directoryToCurrentPluginInfo+"CyverAss.ini";
			fp.firstTimeStarted = 0;
			return fp;
		}

		public void Save(string directory)
		{
			Config cn = this.GetConfig();
			cn.Save(directory,"FirstParameters");
		}
		public static FirstParameters Load(string directory)
		{
			/*FirstParameters fp = Config.Load(directory + "FirstParameters.ini").GetFirstParameters();
			return fp;*/
			return Config.Load(directory + "FirstParameters.ini").GetFirstParameters();
		}
	}
}
