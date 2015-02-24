using System;

namespace WinBalans
{
	class Plugin
	{
		#region Конструкторы
		public Plugin(string path)
		{
			this.path = path;
			isValid = Validate();
		}
		public Plugin(string path, string userdescription)
		{
			this.path = path;
			this.userdescription = userdescription;
			isValid = Validate();
		}
		#endregion
		#region Свойства
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
		public string Description
		{
			get
			{
				if(isValid)
				{
					Type asstype = this.PluginAssembly.GetType("MainClass");
					IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
					return i.GetDescription();
				}
				return "Плагин разработан неправильно и не будет работать. Удалите его из списка."
			}
		}
		public string Name
		{
			get
			{
				if(isValid)
				{
					Type asstype = this.PluginAssembly.GetType("MainClass");
					IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
					return i.GetName();
				}
				return "Плагин разработан неправильно и не будет работать. Удалите его из списка."
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
				password = val;ue;
			}
		}
		public bool IsValid
		{
			get
			{
				return isValid;
			}
		}
		#endregion
		#region Методы
		public string GetValue(string username, string password)
		{
			Type asstype = this.PluginAssembly.GetType("MainClass");
			IInfoProvider i = (IInfoProvider)Activator.CreateInstance(asstype);
			return i.GetValue();
		}
		private bool Validate()
		{
			foreach(Type t in this.PluginAssembly.GetTypes())
			{
				if (t.Name == "MainClass")
					if (t.GetInterface("IInfoProvider")!=null)
						return true;
			}
			return false;
		}
		#endregion
		#region Поля
		string path;
		string userdescription = "";
		bool isValid = false;
		string username = "";
		string password = "";
		#endregion
	}
}
