using System;

namespace WinBalans.Classes.Parameters
{
	/// <summary>
	/// Summary description for Parameters.
	/// </summary>
	public class Parameters
	{
		#region ����
		private ArrayList pluginlist;
		private ViewParameters vp;
		private int firstTimeStarted;
		private int timerEnabled;
		private double interval;
		#endregion
		
		#region ��������
		public int FirstTimeStarted
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
		#endregion
		public Parameters()
		{
		}
		public Parameters(string ipathToViewParameters, string ipathToCurrentPluginInfo,
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
	}
}
