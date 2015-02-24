using System;
using System.Configuration;

namespace Ne.Configuration
{
	public class NeJudgeConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("ContestTypeHandlers",IsRequired=true)]
		public ContestTypeConfiguratorCollection ContestTypeConfigurators
		{
			get { return (ContestTypeConfiguratorCollection)this["ContestTypeHandlers"]; }
			set { this["ContestTypeHandlers"] = value; }
		}

		[ConfigurationProperty("ConnectionString", IsRequired = true)]
		public string ConnectionString
		{
			get { return (string)this["ConnectionString"]; }
			set { this["ConnectionString"] = value; }
		}

		[ConfigurationProperty("ProviderPath",IsRequired=true)]
		public string ProviderPath
		{
			get { return (string)this["ProviderPath"]; }
			set { this["ProviderPath"] = value; }
		}
	}

	public class NeTesterConfigurationSection : ConfigurationSection
	{
		[ConfigurationProperty("ConnectionString", IsRequired = true)]
		public string ConnectionString
		{
			get { return (string)this["ConnectionString"]; }
			set { this["ConnectionString"] = value; }
		}

		[ConfigurationProperty("ProviderPath", IsRequired = true)]
		public string ProviderPath
		{
			get { return (string)this["ProviderPath"]; }
			set { this["ProviderPath"] = value; }
		}

		[ConfigurationProperty("ContestTypeHandlers",IsRequired=true)]
		public ContestTypeConfiguratorCollection ContestTypeConfigurators
		{
			get { return (ContestTypeConfiguratorCollection)this["ContestTypeHandlers"]; }
			set { this["ContestTypeHandlers"] = value; }
		}

		[ConfigurationProperty("Runas")]
		public RunAsParameters RunAsParameters
		{
			get { return (RunAsParameters)this["Runas"]; }
			set { this["Runas"] = value; }
		}

		[ConfigurationProperty("CheckerFile")]
		public string CheckerFile
		{
			get { return (string)this["CheckerFile"]; }
			set { this["CheckerFile"] = value; }
		}

		[ConfigurationProperty("CompileScript")]
		public string CompileScript
		{
			get { return (string)this["CompileScript"]; }
			set { this["CompileScript"] = value; }
		}

		[ConfigurationProperty("ReportFile")]
		public string ReportFile
		{
			get { return (string)this["ReportFile"]; }
			set { this["ReportFile"] = value; }
		}

		[ConfigurationProperty("SolutionFilePattern")]
		public string SolutionFilePattern
		{
			get { return (string)this["SolutionFilePattern"]; }
			set { this["SolutionFilePattern"] = value; }
		}

		[ConfigurationProperty("SolutionExe")]
		public string SolutionExe
		{
			get { return (string)this["SolutionExe"]; }
			set { this["SolutionExe"] = value; }
		}

		[ConfigurationProperty("ShellCommand")]
		public string ShellCommand
		{
			get { return (string)this["ShellCommand"]; }
			set { this["ShellCommand"] = value; }
		}

		[ConfigurationProperty("ShellScriptParam")]
		public string ShellScriptParam
		{
			get { return (string)this["ShellScriptParam"]; }
			set { this["ShellScriptParam"] = value; }
		}

		[ConfigurationProperty("AnswerFile")]
		public string AnswerFile
		{
			get { return (string)this["AnswerFile"]; }
			set { this["AnswerFile"] = value; }
		}

		[ConfigurationProperty("IdlenessLimit")]
		public int IdlenessLimit
		{
			get { return (int)this["IdlenessLimit"]; }
			set { this["IdlenessLimit"] = value; }
		}

		[ConfigurationProperty("RootDir")]
		public string RootDir
		{
			get { return (string)this["RootDir"]; }
			set { this["RootDir"] = value; }
		}
	}
}