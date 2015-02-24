#region Using directives

using System;
using System.Windows.Forms;
using Ini;
using System.Text;
using System.IO;
using System.Collections;

#endregion

namespace WinBalans
{
	public class PluginCollection	:IEnumerable,IList,ICollection,IComparable
	{
		ArrayList pluginList;
		public static bool operator ==(PluginCollection lhs, PluginCollection rhs)
		{
			if ((lhs == null && rhs != null) || (lhs != null && rhs == null))
				return false;
			if (lhs == null && rhs == null)
				return true;
			//A=lhs, B=rhs
			//A⊂ B
			foreach (Plugin p in lhs)
			{
				Plugin pl = rhs[p.Name, p.Version];
				if (p == pl)
					continue;
				else
					return false;
			}
			//B⊂A
			foreach (Plugin p in rhs)
			{
				Plugin pl = lhs[p.Name, p.Version];
				if (p == pl)
					continue;
				else 
					return false;
			}
			return true;
		}
		public static bool operator!=(PluginCollection lhs, PluginCollection rhs)
		{
			return (lhs == rhs) ? false : true;
		}
		public static bool operator <(PluginCollection lhs, PluginCollection rhs)
		{
			bool result1 = false, result2 = false;
			//A=lhs, B=rhs
			//A⊂ B
			foreach (Plugin p in lhs)
			{
				Plugin pl = rhs[p.Name, p.Version];
				if (p == pl)
					result1 = true;
				else
				{
					result1 = false;
					break;
				}
			}
			//B⊂A
			foreach (Plugin p in rhs)
			{
				Plugin pl = lhs[p.Name, p.Version];
				if (p == pl)
					result2 = true;
				else
				{
					result2 = false;
					break;
				}
			}
			return (result1 == true && result2 == false) ? true : false;
		}
		public int CompareTo(object obj)
		{
			PluginCollection pc = obj as PluginCollection;
			if(pc == null)
				throw new ArgumentException("obj is not a PluginCollection");
			if (this == pc)
				return 0;
			if (this > pc)
				return 1;
			//if (this < pc)
				return -1;
		}
		public static bool operator >(PluginCollection lhs, PluginCollection rhs)
		{
			bool result1 = false, result2 = false;
			//A=lhs, B=rhs
			//A⊂ B
			foreach (Plugin p in lhs)
			{
				Plugin pl = rhs[p.Name, p.Version];
				if (p == pl)
					result1 = true;
				else
				{
					result1 = false;
					break;
				}
			}
			//B⊂A
			foreach (Plugin p in rhs)
			{
				Plugin pl = lhs[p.Name, p.Version];
				if (p == pl)
					result2 = true;
				else
				{
					result2 = false;
					break;
				}
			}
			return (result1 == false && result2 == true) ? true : false;
		}
		/*public static PluginCollection operator -(PluginCollection lhs, PluginCollection rhs)
		{
			PluginCollection pc = new PluginCollection();
			if (lhs > rhs)
			{

				//A=lhs, B=rhs
				//A⊂ B
				foreach (Plugin ParametersInstance in lhs)
				{
					Plugin pl = rhs[ParametersInstance.Name, ParametersInstance.Version];
					if (ParametersInstance == pl)
						continue;
					else
						return false;
				}
				//B⊂A
				foreach (Plugin ParametersInstance in rhs)
				{
					Plugin pl = lhs[ParametersInstance.Name, ParametersInstance.Version];
					if (ParametersInstance == pl)
						continue;
					else
						return false;
				}
				return true;
			}
			else if (rhs > lhs)
			{
			}
			return pc;
		}*/
		public static PluginCollection FindPlugins(string folder)
		{
			//TODO: добавить обработку исключений
			//MessageBox.Show(AppDomain.CurrentDomain.BaseDirectory);
			PluginCollection pc = new PluginCollection();
			string[] files = Directory.GetFiles(folder, "*.dll");
			foreach (string s in files)
			{
				Plugin p = new Plugin(s);
				if (p.Valid)
					pc.Add(p);
			}
			return pc;
		}
		public static PluginCollection FindPlugins()
		{
			//TODO: добавить обработку исключений
			return FindPlugins(@"Plugins\");
		}
		public static PluginCollection Synchronize(PluginCollection from, PluginCollection to)
		{
			PluginCollection pc = new PluginCollection();
			foreach(Plugin p in to)
			{
				Plugin pl = from[p.Name];
				if (pl != null)
				{
					p.Username = pl.Username;
					p.Password = pl.Password;
					p.UserDescription = pl.UserDescription;
					p.Checked = true;
					p.Update();
					pc.Add(p);
				}
			}
			return pc;
		}
		public PluginCollection(params Plugin[] pluginList)
		{
			this.pluginList = new ArrayList();
			foreach(Plugin p in pluginList)
				this.pluginList.Add(p);
		}
		public int GetIndex(string name)
		{
			foreach (Plugin p in pluginList)
				if (p.Name == name)
					return IndexOf(p);
			return -1;
		}
		public Plugin this[string name, string version]
		{
			get
			{
				foreach (Plugin p in pluginList)
					if (p.Name == name && p.Version == version)
						return p;
				return null;
			}
		}
		public Plugin this[string name]
		{
			get
			{
				foreach (Plugin p in pluginList)
					if (p.Name == name)
						return p;
				return null;
			}
		}
		public PluginCollection SelectedPlugins
		{
			get
			{
				PluginCollection pc = new PluginCollection();
				foreach (Plugin p in pluginList)
					if (p.Checked && p.Valid)
						pc.Add(p);
				return pc;
			}
		}
		public PluginCollection()
		{
			pluginList = new ArrayList();
		}
		public IEnumerator GetEnumerator()
		{
			return pluginList.GetEnumerator();
		}
		public Config GetConfig(Logger l)
		{
			Config cn = new Config(l);
			foreach (Plugin p in pluginList)
			{
				cn.BeginSection("[Plugin #" + pluginList.IndexOf(p).ToString() + "]");
				cn.AddEntry("Path", p.Path);
				cn.AddEntry("Username", p.Username);
				cn.AddEntry("Password", p.Password);
				cn.AddEntry("UserDescription", p.UserDescription);
			}
			return cn;
		}
		public void ReverseSelection(int index)
		{
			bool newvalue = (((Plugin)this[index]).Checked == false) ? true : false;
			foreach (Plugin p in pluginList)
				p.Checked = false;
			((Plugin)this[index]).Checked = newvalue;
		}
		#region Реализация интерфейса IList, ICollection
		
		public int Add(Plugin p)
		{
			/*foreach (Plugin pl in pluginList)
			{
				if (pl.Name == ParametersInstance.Name && pl.Version == ParametersInstance.Version)
					return;
			}*/
			return pluginList.Add(p);
		}
		public int Add(object value)
		{
			return pluginList.Add(value);
		}
		public void Clear()
		{
			pluginList.Clear();
		}
		public bool Contains(object value)
		{
			return pluginList.Contains(value);
		}
		public int IndexOf(object value)
		{
			return pluginList.IndexOf(value);
		}
		public void Insert(int index, object value)
		{
			pluginList.Insert(index,value);
		}
		public void Remove(object value)
		{
			pluginList.Remove(value);
		}
		public void RemoveAt(int index)
		{
			pluginList.RemoveAt(index);
		}
		public object this[int index]
		{
			get
			{
				try
				{
					return pluginList[index];
				}
				catch (ArgumentOutOfRangeException)
				{
					return null;
				}
			}
			set
			{
				/*try
				{
					pluginList[index] = value;
				}
				catch (ArgumentOutOfRangeException){} */
				Plugin p = value as Plugin;
				if (!ReferenceEquals(p,null))
				{
					((Plugin)pluginList[index]).Username = p.Username;
					((Plugin)pluginList[index]).Password = p.Password;
					((Plugin)pluginList[index]).UserDescription = p.UserDescription;
				}
			}
		}
		public bool IsReadOnly 
		{
			get
			{
				return pluginList.IsReadOnly;
			}
		}
		public bool IsFixedSize 
		{
			get
			{
				return pluginList.IsFixedSize;
			}
		}
		public void CopyTo(Array array, int index)
		{
			pluginList.CopyTo(array,index);
		}
		public int Count 
		{
			get
			{
				return pluginList.Count;
			}
		}
		public bool IsSynchronized 
		{
			get
			{
				return pluginList.IsSynchronized;
			}
		}
		public object SyncRoot 
		{
			get
			{
				return pluginList.SyncRoot;
			}
		}



		#endregion
		
		public static PluginCollection Parse(Config cn)
		{
			PluginCollection p = new PluginCollection();
			int i  = 1;
			foreach (Section s in cn)
				if (s.Name == "[Plugin #" + i + "]")
					if (s["Path"] != null && s["Username"] != null && s["Password"] != null)
					{
						if (s["UserDescription"] != null)
							p.Add(new Plugin(s["Path"], s["Username"], s["Password"], s["UserDescription"]));
						else
							p.Add(new Plugin(s["Path"], s["Username"], s["Password"]));
						i++;
					}
			return p;
		}
	}
}
