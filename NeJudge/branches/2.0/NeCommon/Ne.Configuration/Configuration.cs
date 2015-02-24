using System;
using System.Configuration;
using System.Web.Configuration; 

namespace Ne.Configuration
{
	public static class Configurator
	{
		static System.Configuration.Configuration jconf;
		static System.Configuration.Configuration tconf;

		public static void InitNeTesterConfiguration()
		{
			tconf = ConfigurationManager.OpenExeConfiguration("NeTester.exe");
		}

		public static void InitNeJudgeConfiguration()
		{
			jconf = WebConfigurationManager.OpenWebConfiguration("/NeJudge");
		}

		public static NeJudgeConfigurationSection NeJudgeConfiguration
		{
			get { return (NeJudgeConfigurationSection)jconf.GetSection("nejudge"); }
		}

		public static NeTesterConfigurationSection NeTesterConfiguration
		{
			get { return (NeTesterConfigurationSection)tconf.GetSection("netester"); }
		}
	}
}
