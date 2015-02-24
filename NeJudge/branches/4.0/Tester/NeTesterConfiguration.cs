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
		public static string ProblemsFolder
		{
			get
			{
				return "Problems";
			}
		}
		
		public static string RootDir
		{
			get
			{
				return ConfigurationManager.AppSettings["TesterRoot"];
			}
		}

		public static string CheckerFile
		{
			get
			{
				return "check.exe";
			}
		}

		public static string CompileScript
		{
			get
			{
				return "compile.bat";
			}
		}

		public static string ReportFile
		{
			get
			{
				return "compilation.report";
			}
		}

		public static string SolutionFilePattern
		{
			get
			{
				return "sln.{0}";
			}
		}

		public static string SoluitionExe
		{
			get
			{
				return "sln.exe";
			}
		}

		public static string ShellCommand
		{
			get
			{
				return @"C:\WINDOWS\system32\cmd.exe";
			}
		}

		public static string ShellScriptParam
		{
			get
			{
				return "/c";
			}
		}

		public static string AnswerFile
		{
			get
			{
				return "answer.txt";
			}
		}
		
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
			if ( RootDir == null )
				throw new NeConfigurationException("\"TesterRoot\" parameter is missing in the configuration file");
		}
	}
}