using System;
using System.IO;
using System.Collections.Specialized;
using Microsoft.Win32;
using System.Collections;

namespace WinBalans
{
	public class FirstParameters
	{
		public string pathToViewParameters;
		public string pathToCurrentPluginInfoList;
		public int firstTimeStarted;

		public FirstParameters()
		
		
		
		
		
		\{
		}
		public FirstParameters(string _pathToViewParameters, string _pathToCurrentPluginInfo,
			int _firstTimeStarted)
		{
			pathToViewParameters = _pathToViewParameters;
			pathToCurrentPluginInfoList = _pathToCurrentPluginInfoList;
			firstTimeStarted = _firstTimeStarted;
		}
		public void Parse()
		{
			ArrayList plugininfocol = new ArrayList();
			StreamReader sr = new StreamReader(pathToCurrentPluginInfoList);
			string line="";
			
			while( (line = sr.ReadLine() ) != null ) 
				plugininfocol.Add(PluginInfo.Load(line));
			
		}
		public Config GetConfig()
		{
			Config cn = new Config();
			cn.BeginSection("[FirstParameters]");
				cn.AddEntry("PathToViewParameters", pathToViewParameters);	
				cn.AddEntry("PathToCurrentPluginInfoList", pathToCurrentPluginInfoList);
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
			this.GetConfig().Save(directory,"FirstParameters");
		}

		public static FirstParameters Load(string directory)
		{
			return Config.Load(directory + "FirstParameters.ini").GetFirstParameters();
		}
	}
}
