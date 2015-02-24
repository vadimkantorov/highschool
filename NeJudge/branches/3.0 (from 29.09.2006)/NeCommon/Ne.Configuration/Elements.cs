using System;
using System.Configuration;

namespace Ne.Configuration
{
	[ConfigurationCollection(typeof(ContestTypeConfigurator))]
	public class ContestTypeConfiguratorCollection : ConfigurationElementCollection
	{
		protected override ConfigurationElement CreateNewElement()
		{
			return new ContestTypeConfigurator();
		}

		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((ContestTypeConfigurator)element).ContestType;
		}

		public void Add(ContestTypeConfigurator c)
		{
			BaseAdd(c);
		}

		public void Remove(string key)
		{
			BaseRemove(key);
		}

		public void Clear()
		{
			BaseClear();
		}

		public ContestTypeConfigurator this[int ind]
		{
			get { return (ContestTypeConfigurator)BaseGet(ind); }
		}
	}

	public class ContestTypeConfigurator : ConfigurationElement
	{
		[ConfigurationProperty("ContestType", IsRequired = true, IsKey = true)]
		public string ContestType
		{
			get { return (string)this["ContestType"]; }
			set { this["ContestType"] = value; }
		}

		[ConfigurationProperty("AssemblyPath", IsRequired = true)]
		public string AssemblyPath
		{
			get { return (string)this["AssemblyPath"]; }
			set { this["AssemblyPath"] = value; }
		}
	}

	public class RunAsParameters : ConfigurationElement
	{
		[ConfigurationProperty("Username" , IsRequired = true)]
		public string Username
		{
			get
			{
				object o = this["Username"];
				return o != null ? (string)o : string.Empty;
			}
			set { this["Username"] = value; }
		}

		[ConfigurationProperty("Password" , IsRequired = true)]
		public string Password
		{
			get
			{
				object o = this["Password"];
				return o != null ? (string)o : string.Empty;
			}
			set { this["Password"] = value; }
		}
	}
}