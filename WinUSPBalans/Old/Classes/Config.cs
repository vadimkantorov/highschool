using System;
using System.Text;
using System.IO;
using System.Collections.Specialized;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WinBalans
{
	public class Config
	{
		class Section
		{
			#region Поля Secion
            
			private string name; // имя секции
			public string Name
			{
				get {return this.name;}
			}
			private NameValueCollection entry_coll; // словарь записей
			
			#endregion
			#region Методы Section
			public Section(string name) // конструктор, требует имя секции
			{
				this.name  = name;
				entry_coll = new NameValueCollection();
			}
			public  void AddEntry(string name,string value) // добавл.записи в секцию
			{
				if(CanWrite(name))								   
					entry_coll.Add(name,value); // просто пихаем в словарь
			}
			
			public string GetString() // получение строкового представления
			{
				string result=name  + "\r\n"; // вначале имя секции
				// цикл по всем записям словаря
				//здесь можно сделать foreach
				for(int i=0; i<entry_coll.Count; i++)
				{
					result = result + entry_coll.GetKey(i) + "=" // добавл. имя ключа
						+ entry_coll.Get(entry_coll.GetKey(i)) // имя значения по ключу
						+ "\r\n";
				}
				return result;
			}
			public void ChangeParam(string paramname, string newparamvalue)
			{
				entry_coll.Set(paramname,newparamvalue);
			}
			public string GetParam(string name)
			{
				return entry_coll.Get(name);
			}
			private bool CanWrite(string name)
			{
				foreach(string str in entry_coll.AllKeys)
				{
					if(str == name)
						return false;
				}
				return true;
			}
			#endregion
		}

		#region Поля Config
		private ArrayList sec_list;         // список секций
		private Section   current_sec;      // ссылка на текущую секцию
		#endregion
		#region Конструктор Config
		public Config()
		{
			sec_list = new ArrayList();
		}
		#endregion
		#region Методы Config
		public int GetSectionIndexByName(string name)
		{
			for(int i=0; i<sec_list.Count; i++)
				if(((Section)sec_list[i]).Name == name)
					return i;
				else continue;
			return -1;
		}

		public void ChangeParamInSection(string secname, string paramname, string newparamvalue)
		{
			if(GetSectionIndexByName(secname) == -1)	throw new Exception("Секции с таким именем не существует");
			Section temp = (Section)sec_list[GetSectionIndexByName(secname)];
			temp.ChangeParam(paramname,newparamvalue);
		}
		public string GetParamInSection(string secname, string paramname)
		{
			if(GetSectionIndexByName(secname) == -1)	throw new Exception("Секции с таким именем не существует");
			Section temp = (Section)sec_list[GetSectionIndexByName(secname)];
			return temp.GetParam(paramname);
		}
		public void BeginSection(string name)
		{
			if(GetSectionIndexByName(name) == -1)
			{
				Section s = new Section(name); /// создаем новую секцию
				sec_list.Add(s);    // добавляем ее (ссылку на нее) в список
				current_sec = s;     // делаем текущей секцией
			}
		}
		public void AddEntry(string name,string value)
		{
			// добавл. запись к текущей секции
			current_sec.AddEntry(name,value);
		}
		public string GetString()
		{
			string result="";
			// используем удобный оператор с автоматизированным, так сказать,
			// приведением типов
			foreach(Section s in sec_list)	result = result + s.GetString();
			return result;
		}
		public void Save(string directory, string name) 
		{
			string fullname=directory + name + ".ini";
			StreamWriter stw=new StreamWriter(fullname);
			stw.Write(this.GetString());
			stw.Close();
		}
		public void Save(string filename) 
		{
			string fullname = filename + ".ini";
			StreamWriter stw=new StreamWriter(fullname);
			stw.Write(this.GetString());
			stw.Close();
		} 
		public static Config Load(string path)
		{
			Config p_cn = new Config();
			StreamReader sr = new StreamReader(path);
			if(path.Equals(""))	throw new Exception("Парсить нечего(файл пустой)");
			string line;
			while( (line=sr.ReadLine() ) != null ) 
			{
				switch (line[0])
				{
					case '[':	
						p_cn.BeginSection(line);
						break;
					case '#':
						break;
					default:
						string[] split = line.Split("=".ToCharArray());
						if ( split[0]!="")	p_cn.AddEntry(split[0],split[1]);
						else 
						{
							//Console.WriteLine("Возникла ошибка в параметре");
							//Console.Read();
							//p_cn.AddEntry(split[0],split[1]);
						}
						break;
				}
			}
			sr.Close();
			return p_cn;
		}

        public ViewParameters GetViewParameters()
		{
			ViewParameters pr = new ViewParameters();
			#region вычисление
			//////////////////////////////////////////////////////////////////////////
			string mvalue = this.GetParamInSection("MainWindow","Color");
			string[] msplit = mvalue.Split("[".ToCharArray());
			string mcolorname = msplit[1].Remove(msplit[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			string d1value = this.GetParamInSection("DesktopText","Color1");
			string[] d1split = d1value.Split("[".ToCharArray());
			string d1colorname = d1split[1].Remove(d1split[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			string d2value = this.GetParamInSection("DesktopText","Color2");
			string[] d2split = d2value.Split("[".ToCharArray());
			string d2colorname = d2split[1].Remove(d2split[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			string tvalue = this.GetParamInSection("MainText","Color");
			string[] tsplit = tvalue.Split("[".ToCharArray());
			string tcolorname = tsplit[1].Remove(tsplit[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			double prex = Convert.ToDouble(this.GetParamInSection("DesktopText","X"));
			double prey = Convert.ToDouble(this.GetParamInSection("DesktopText","Y"));		
			//////////////////////////////////////////////////////////////////////////
			string tempeffect = this.GetParamInSection("DesktopText","Effect");
			//////////////////////////////////////////////////////////////////////////
			string dtextfamilyname1 = this.GetParamInSection("DesktopText","FontName");
			double dtextsize = Convert.ToDouble(this.GetParamInSection("DesktopText","FontSize"));
			string[] textsplit = dtextfamilyname1.Split("=".ToCharArray());
			string dtextfamilyname = textsplit[1].Remove(textsplit[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			double ttextsize = Convert.ToDouble(this.GetParamInSection("MainText","FontSize"));
			string ttextfamilyname1 = this.GetParamInSection("MainText","FontName");
			string[] textsplit1 = ttextfamilyname1.Split("=".ToCharArray());
			string ttextfamilyname = textsplit1[1].Remove(textsplit1[1].Length-1,1);
			//////////////////////////////////////////////////////////////////////////
			#endregion
				pr.MOpacity	= Convert.ToDouble(this.GetParamInSection("MainWindow","Opacity"));	
				pr.MColor	= Color.FromKnownColor(Color.FromName(mcolorname).ToKnownColor());
				pr.DeskX = (float)prex;
				pr.DeskY = (float)prey;	
				pr.DeskColor1 = Color.FromKnownColor(Color.FromName(d1colorname).ToKnownColor());
				pr.DeskColor2 = Color.FromKnownColor(Color.FromName(d2colorname).ToKnownColor());	
				pr.DeskFont	= new Font(dtextfamilyname,(float)dtextsize);
				pr.TextColor = Color.FromKnownColor(Color.FromName(tcolorname).ToKnownColor());	
				pr.TextFont	= new Font(ttextfamilyname,(float)ttextsize);	
				if(tempeffect == "BackwardDiagonal")
					pr.DeskEffect = LinearGradientMode.BackwardDiagonal;
				if(tempeffect == "ForwardDiagonal")
					pr.DeskEffect = LinearGradientMode.ForwardDiagonal;
				if(tempeffect == "Horizontal")
					pr.DeskEffect = LinearGradientMode.Horizontal;
				if(tempeffect == "Vertical")
					pr.DeskEffect = LinearGradientMode.Vertical;
			return pr;
		}
		public FirstParameters GetFirstParameters()
		{
			int firstTimeStarted = Convert.ToInt32(GetParamInSection("[FirstParameters]", "FirstTimeStarted"));
			string pathToViewParameters = GetParamInSection("[FirstParameters]", "PathToViewParameters");
			string pathToCurrentPluginInfo = GetParamInSection("[FirstParameters]", "PathToPluginInfo");
			return (new FirstParameters(pathToViewParameters, pathToCurrentPluginInfo,firstTimeStarted));
		}
		public PluginInfo GetPluginInfo()
		{
			PluginInfo pi = new PluginInfo();
				pi.pathToAssembly = this.GetParamInSection("[PluginInfo]","PathToAssembly");
				pi.username = this.GetParamInSection("[PluginInfo]","Username");
				pi.password = this.GetParamInSection("[PluginInfo]","Password");
				pi.timerEnabled = Convert.ToInt32(this.GetParamInSection("[PluginInfo]","TimerEnabled"));
				pi.interval = Convert.ToDouble(this.GetParamInSection("[PluginInfo]","Interval"));
			return pi;
		}
		#endregion	
	}
}
