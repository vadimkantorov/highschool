using System;
using System.IO;
using System.Collections;


namespace Ini
{
	public class Config :IEnumerable
	{
		#region Поля Config
		ArrayList sec_list;
		Section current_sec;
		Logger l;
		public Section CurrentSection
		{
			get
			{
				return current_sec;
			}
		}
		public ArrayList SectionList
		{
			get
			{
				return sec_list;
			}
		}
		public Logger LoggerInstance
		{
			get
			{
				return l;
			}
			set
			{
				l = value;
			}
		}
		public string this[string secname, string paramname]
		{
			get
			{
				string str = null;

				try
				{
					Section temp = (Section)sec_list[GetSectionIndexByName(secname)];
					str = temp[paramname];
				}
				
				catch(SectionNotFoundException e)
				{
					#region Comments
					/*System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);*/
					#endregion
					str = null;
					throw new ConfigException("Ошибка в Config(индексатор)",e);
				}
				catch(ConfigException e)
				{
					l.WriteLine("Ошибка в методе Get(string secname, string paramname)\r\n\r\nsecname = "+secname+"\r\nparamname = "+paramname+e.Message,MessageType.Error);
				}
				return str;
			}
			set
			{
				try
				{
					Section temp = (Section)sec_list[GetSectionIndexByName(secname)];
					temp[paramname] = value;
				}
				catch(SectionNotFoundException e)
				{
					#region Commecnts
					/*System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);*/
					#endregion
					throw new ConfigException("Ошибка в Config(индексатор)",e);
					//вставить логирование
				}
			}
		}
		
		#endregion
		#region Методы Config
		#region Непроблемные методы
		public int GetSectionIndexByName(string name)
		{
			for(int i=0; i<sec_list.Count; i++)
				if(((Section)sec_list[i]).Name == name)
					return i;
			throw new SectionNotFoundException("Секция не найдена.",name);//return -1;
		}

		public string ReadFromSectionToEnd(int index)
		{
			ArrayList v = new ArrayList();
			string str = "";
			for(int i = index;i<=sec_list.Count;i++)
				v.Add(sec_list[i]);
			foreach(Section s in v)
				str+=s.ToString();
			return str;
		}

		public void BeginSection(string name)
		{
			try
			{
				GetSectionIndexByName(name); 
			}
			catch(SectionNotFoundException)
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
		public override string ToString()
		{
			string result = "";
			// используем удобный оператор с автоматизированным, так сказать,
			// приведением типов
			foreach(Section s in sec_list)	result += s.ToString();
			return result;
		}
		/*public void Close()
		{

		}*/
		#endregion
		
		public static Config operator+(Config lhs, Config rhs)
		{
			Config ret = new Config(@"C:\log.txt");
			Config.Parse(lhs.ToString()+rhs.ToString(),lhs.LoggerInstance,ret);
			return ret;
		}
		public string ReadFromSectionToEnd(string name)
		{
			string str = null;
			try
			{
				str = ReadFromSectionToEnd(GetSectionIndexByName(name));
			}
			catch(SectionNotFoundException e)
			{
				#region Comments
				/*System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.Forms				.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.					Windows.Forms.MessageBoxOptions.ServiceNotification);*/
				#endregion
				l.WriteLine("Ошибка в методе ReadfromSectionToEnd(string name)\r\n\r\nname = "+name+e.Message,MessageType.Error);
				str = null;
			}
			return str;
		}
		#region Save'n'Parse
		public void Save(string directory, string name) 
		{
			string fullname=directory + name + ".ini";
			Save(fullname);
		}
		public void Save(string fullname) 
		{
			StreamWriter sw = null;

			try
			{
				sw = new StreamWriter(fullname);
				sw.Write(this.ToString());
			}
			
			catch (FileNotFoundException e)
			{
				throw new FileNotFoundException("Cannot open configfile: no such file:" +fullname+"\n\n\n\n"+e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				throw new IOException("Cannot open configfile: access denied:" +fullname+"\n\n\n\n"+e.Message);
			}
			catch (IOException e)
			{
				#region Comments
				/*System.Windows.Forms.MessageBox.Show("Ошибка при открытии файла" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка.Попробуйте открыть файл ещё раз",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);*/
				#endregion
				l.WriteLine(e.Message,MessageType.Error);
				this.Save("#" + fullname);
			}
			finally
			{
				if(sw != null) sw.Close();
			}
		} 
		#region Comments
		/*public static Config Load(string path)
		{
			StreamReader sr = null;
			string s = "";
			try
			{
				 sr = new StreamReader(path);
				 s = sr.ReadToEnd();
			}
			catch(IOException e)
			{
		#region Comments
				System.Windows.Forms.MessageBox.Show("Ошибка при открытии файла" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка.Попробуйте открыть файл ещё раз",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);
		#endregion
				return new Config("");
			}
			finally
			{
				sr.Close();
			}
			
			return Parse(s);
		}*/
		#endregion
		private static Config Parse(StringReader sr, Logger l,Config destCol)
		{
			string line;
			int number = 1;
			for( ;(line=sr.ReadLine() ) != null;number++) 
			{
				if(line == "" || line.Trim() == "" )
					continue;
				switch (line[0])
				{
					case '[':	
						destCol.BeginSection(line);
						break;
					case '#':
						break;
					default:
						try
						{
							if(destCol.CurrentSection != null)
							{
								string[] split = line.Split("=".ToCharArray());
								if ( split[0].Trim() != "" && split.Length != 1)
								{
									if(split.Length >2)
									{
										string val = "";
										for(int i = 1; i < split.Length; i++)
											val += split[i];
										destCol.AddEntry(split[0],val);
										break;
									}
									destCol.AddEntry(split[0],split[1]);
								}
								else
								{
									string message = "Произошла ошибка при разборе файла настроек.Для устранения этой ошибки в будущих версиях программы передайте разработчику следующую информацию:\r\n\r\np_cn. CurrentSection != null, split[0] = \"\"\r\n";
									throw new IniFormatException(message, number);
								}
							}
							else
							{
								string message = "Произошла ошибка при разборе файла настроек.Для устранения этой ошибки в будущих версиях программы передайте разработчику следующую информацию:\r\n\r\np_cn.CurrentSection == null\r\n";
								throw new IniFormatException(message , number);
							}
						}
						catch(IniFormatException e)
						{
							l.WriteLine(e.Message,MessageType.Error);
							break;
						}
						break;
				}
			}
			sr.Close();
			return destCol;
		}
		public static Config Parse(string s,Logger l, Config destConfig)
		{
			return Parse(new StringReader(s),l,destConfig);
		}
		#endregion
		#region Comments
		/*public string ReadFromSectionToSection(int fromIndex,int toIndex)
		{
			ArrayList v = new ArrayList();
			string str = "";
			for(int i = fromIndex;i<=toIndex;i++)
			{
		#region
				try
				{
					v.Add((Section)sec_list[i]);
				}
				catch(IndexOutOfRangeException e)
				{
					System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.					Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,						System.Windows.Forms.MessageBoxOptions.ServiceNotification);
					//сделать возможность выбора:продолжать или остановить выполнение программы
					//вставить логирование
					return "";//return null;
				}
		#endregion
			}
			foreach(Section s in v)
				str+=s.ToString();
			return str;
		}
		public string ReadFromSectionToSection(string fromName,string toName)
		{
			try
			{
				return ReadFromSectionToSection(GetSectionIndexByName(fromName),GetSectionIndexByName(toName));
			}
			catch(SectionNotFoundException e)
			{
				System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.Forms				.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.					Windows.Forms.MessageBoxOptions.ServiceNotification);
				//сделать возможность выбора:продолжать или остановить выполнение программы
				//вставить логирование
				return "";//return null;
			}
			catch(IndexOutOfRangeException e)
			{
				System.Windows.Forms.MessageBox.Show("Ошибка" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка!",System.Windows.Forms				.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.					Windows.Forms.MessageBoxOptions.ServiceNotification);
				//сделать возможность выбора:продолжать или остановить выполнение программы
				//вставить логирование
				return "";//return null;
			}
			//return "";//return null
		}
		*/
		#endregion
		#endregion	
		#region Конструкторы Config
		public Config(string logname)

		{
			try
			{
				l = new Logger(logname);
			}
			catch(IOException)
			{
				l = new Logger("#"+logname);
			}
			sec_list = new ArrayList();
		}
		public Config(Logger l)

		{
			this.l = l;
			sec_list = new ArrayList();
		}
		~Config()
		{
			this.Close();
		}
		public Config Load(string filename)
		{
			StreamReader sr = null;
			string str = null;
			try
			{
				sr = new StreamReader(filename);
				str = sr.ReadToEnd();
			}
			catch (FileNotFoundException e)
			{	
				throw new FileNotFoundException("Cannot open configfile: no such file:" +filename+"\n\n\n\n"+e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				throw new IOException("Cannot open configfile: access denied:" +filename+"\n\n\n\n"+e.Message);
			}
			catch (IOException e)
			{
				#region Comments
				/*System.Windows.Forms.MessageBox.Show("Ошибка при открытии файла" + e.Message + "Пожалуста, обратитесь к разработчику.","Ошибка.Попробуйте открыть файл ещё раз",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);*/
				#endregion
				this.l.WriteLine(e.Message,MessageType.Error);
				str = null;
			}
			finally
			{
				sr.Close();
			}
			return Parse(str,l, this);
		}
		public void Close()
		{
			l.Close();
		}
		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return sec_list.GetEnumerator();
		}

		#endregion
	}
}
