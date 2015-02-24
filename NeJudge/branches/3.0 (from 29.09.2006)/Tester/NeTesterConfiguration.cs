using System;
using System.Configuration;
using System.IO;

namespace Ne.Tester
{
	public struct RunAsParameters
	{
		public string Username;
		public string Password;

		public RunAsParameters(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}

	public static class NeTesterConfiguration
	{
		public readonly static string ROOT_DIR = ConfigurationManager.AppSettings["TesterRoot"];

		public const string CHECKER_FILE = "check.exe";
		public const string COMPILE_SCRIPT = "compile.bat";
		public const string REPORT_FILE = "compilation.report";
		public const string SOLUTION_FILE_PATTERN = "sln.{0}";
		public const string SOLUTION_EXE = "sln.exe";
		public const string SHELL_COMMAND = @"C:\WINDOWS\system32\cmd.exe";
		public const string SHELL_SCRIPT_PARAM = "/c";
		public const string ANSWER_FILE = "answer.txt";

		public static RunAsParameters RunAsParams
		{
			get
			{
				return new RunAsParameters(ConfigurationManager.AppSettings["RunAsUsername"],
				                           ConfigurationManager.AppSettings["RunAsPassword"]);
			}
		}

		public static int IdlenessLimit
		{
			get 
			{
				string ret = ConfigurationManager.AppSettings["IdlenessLimit"];
				return ( ret != null ) ? int.Parse(ret) : 0; 
			}
		}

		public static void CheckConfig()
		{
			if ( ROOT_DIR == null )
				throw new NeConfigurationException("\"TesterRoot\" parameter is missing in the configuration file");
		}
	}
}