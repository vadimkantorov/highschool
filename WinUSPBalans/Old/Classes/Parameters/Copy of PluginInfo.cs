using System;
using System.Reflection;

namespace WinBalans
{
	public class PluginInfo
	{
		public string pathToAssembly;
		public int timerEnabled;
		public string username;
		public string password;
		public double interval;

		public Assembly GetAssembly()
		{
			return Assembly.LoadFrom(pathToAssembly);
		}
		public PluginInfo()
		{
		}
		public PluginInfo(string _path, string _username, string _password, int _timerEnabled, double _interval)
		{
			pathToAssembly = _path;
			username = _username;
			password = _password;
			timerEnabled = _timerEnabled;
			interval = _interval;
		}
		public PluginInfo(string ipath, int itimerEnabled)
		{
			pathToAssembly = ipath;
			timerEnabled = itimerEnabled;
			interval = 10000;
		}
		public PluginInfo(string ipath)
		{
			pathToAssembly = ipath;
			timerEnabled = 0;
			interval = 10000;
		}
		public Config GetConfig()
		{
			Config cn = new Config();
			cn.BeginSection("[PluginInfo]");
				cn.AddEntry("Username",username);
				cn.AddEntry("Password",password);
				cn.AddEntry("PathToAssembly",pathToAssembly);
				cn.AddEntry("TimerEnabled",timerEnabled.ToString());
				cn.AddEntry("Interval",interval.ToString());
			return cn;
		}
		public static PluginInfo CreateDefaultPluginInfo()
		{
			//return (new PluginInfo(@"Plugins\CyverAss.dll","","",0));
			return (new PluginInfo(@"CyverAss.dll","","",0,10000));
		}
		public void Save(string directory, string name,string key)
		{
			string cryptedpass = "";
			if(this.password != "")
				 cryptedpass = RC4Wrap.EncryptText(this.password, key);
			Config cn = this.GetConfig();
			cn.ChangeParamInSection("[PluginInfo]","Password",cryptedpass);
			cn.Save(directory,name);
		}
		public void Save(string fullname)
		{
			Config cn = this.GetConfig();
			cn.Save(fullname);
		}
		public static PluginInfo Load(string path)
		{
			return Config.Load(path).GetPluginInfo();
		}
	}
}
