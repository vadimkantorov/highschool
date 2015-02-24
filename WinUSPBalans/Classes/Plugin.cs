using System;
using Ini;
using System.Reflection;

namespace WinBalans
{
	public class Plugin :ICloneable, IComparable
	{
		#region Конструкторы
		public Plugin(string path):this(path,false)
		{
		}
		public Plugin(string path, bool ex)
		{
			this.path = path;
			valid = Validate();
			if(ex)
				throw new NotImplementedException();
		}
		public Plugin(string path, string userdescription)
		{
			this.path = path;
			this.userdescription = userdescription;
			valid = Validate();
		}
		public Plugin(string path, string username, string password)
		{
			this.path = path;
			this.username = username;
			this.password = password;
			valid = Validate();
		}
		public Plugin(string path,string username, string password, string userdescription)
		{
			this.path = path;
			this.userdescription = userdescription;
			this.username = username;
			this.password = password;
			valid = Validate();
		}
		#endregion
		#region Свойства
		public bool Checked
		{
			get
			{
				return ischecked;
			}
			set
			{
				ischecked = value;
			}
		}
		public string Path
		{
			get
			{
				return path;
			}
		}
		public Assembly PluginAssembly
		{
			get
			{
				return Assembly.LoadFrom(path);
			}
		}
		public string LastString
		{
			get
			{
				if (valid)
					return (laststring.Trim() != "") ? laststring : GetValue();
				return "";
			}
			set
			{
				laststring = value;
			}
		}
		public string Description
		{
			get
			{
				if(valid)
				{
					Type asstype = PluginAssembly.GetType("MainClass");
					IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
					return i.GetDescription();
				}
				return "Плагин разработан неправильно, и не будет работать. Удалите его из списка.";
			}
		}
		public string Name
		{
			get
			{
				if(valid)
				{
					Type asstype = PluginAssembly.GetType("MainClass");
					IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
					return i.GetName();
				}
				return "Некорректный плагин";
			}
		}
		public string UserDescription
		{
			get
			{
				return userdescription;
			}
			set
			{
				userdescription = value;
			}
		}
		public string Version
		{
			get
			{
				if (valid)
				{
					Type asstype = this.PluginAssembly.GetType("MainClass");
					IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
					return i.GetVersion();
				}
				//return "Плагин разработан неправильно, и не будет работать. Удалите его из списка.";
				return "";
			}
		}
		public string Username
		{
			get
			{
				return username;
			}
			set
			{
				username = value;
			}
		}
		public string Password
		{
			get
			{
				return password;
			}
			set
			{
				password = value;
			}
		}
		public bool Valid
		{
			get
			{
				return valid;
			}
		}
		#endregion
		#region Методы
		public object Clone()
		{
			return new Plugin(path, username, password, userdescription);
		}
		public string GetValue()
		{
			if (valid)
			{
				Type asstype = PluginAssembly.GetType("MainClass");
				IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
				return (laststring = i.GetValue(username, password));
			}
			return "Плагин неправильный";
		}
		private bool Validate()
		{
			foreach(Type t in PluginAssembly.GetTypes())
				if (t.Name == "MainClass")
					if (t.GetInterface("IInfoProvider") != null)
						return true;
			return false;
		}
  		public override string ToString()
		{
			return Name;
		}
		public Config GetConfig()
		{
			return null;
		}
		public string Update()
		{
			if(valid)
				return (laststring = GetValue());
			return null;
		}
		public bool Install()
		{
			/*if (valid)
			{
				Type asstype = PluginAssembly.GetType("MainClass");
				IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
				return i.Install(TempParameters.BaseDirectory+ "/plugins");
			}*/
			return false;
		}
		public bool Unistall()
		{
			/*if (valid)
			{
				Type asstype = PluginAssembly.GetType("MainClass");
				IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
				return i.Uninstall();
			}*/
			return false;
		}
		public static bool operator ==(Plugin lhs, Plugin rhs)
		{
			if(ReferenceEquals(lhs, rhs))
				return true;
			if (!ReferenceEquals(lhs,null) && !ReferenceEquals(rhs, null) && lhs.Valid == rhs.Valid == true && lhs.Checked == rhs.Checked &&		lhs.Username == rhs.Username && lhs.Password == rhs.Password && lhs.UserDescription == rhs.UserDescription && lhs.Path == rhs.Path)
				return true;

			/*if (!ReferenceEquals(lhs, null) && !ReferenceEquals(rhs, null) && lhs.Valid == rhs.Valid == true && lhs.Checked == rhs.Checked &&		lhs.Username == rhs.Username && lhs.Password == rhs.Password && lhs.UserDescription == rhs.UserDescription && lhs.Path != rhs.Path
				&& lhs.Name == rhs.Name && lhs.Version == rhs.Version && lhs.Description == rhs.Description)
				return true;*/
			return false;
		}
		public static bool operator !=(Plugin lhs, Plugin rhs)
		{
			//if(ReferenceEquals(lhs, null))
			return (lhs == rhs) ? false : true;
		}
		public int CompareTo(object obj)
		{
			Plugin p = obj as Plugin;
			if(p == null)
				throw new ArgumentException("obj is not a Plugin");
			if (this == p)
				return 0;
			return 1;
		}
		#endregion
		#region Поля
		bool ischecked = false;
		string path;
		string userdescription = "";
		bool valid = false;
		string username = "";
		string password = "";
		string laststring = "";
		#endregion
	}
}
