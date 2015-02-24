using System;
using System.Collections.Specialized;

namespace Ini
{
	public class Section
	{
		#region Поля Secion
            
		private string name; // имя секции
		public string Name
		{
			get {return this.name;}
		}
		public string this[string paramname]
		{
			get
			{
				return entry_coll.Get(name);
			}
			set
			{
				entry_coll.Set(paramname,value);
			}
		}
		private NameValueCollection entry_coll; // словарь записей
			
		#endregion
		#region Конструктор Section
		public Section(string name) // конструктор, требует имя секции
		{
			this.name  = name;
			entry_coll = new NameValueCollection();
		}
		#endregion
		#region Методы Section
			
		public void AddEntry(string name,string value) // добавл.записи в секцию
		{
			if(!Exists(name))								   
				entry_coll.Add(name,value); // просто пихаем в словарь
		}
		public void AddEntry(string name,string value, bool overwrite) // добавл.записи в секцию
		{
			if(overwrite)
			{	
				if(Exists(name))
					entry_coll.Set(name,value);
				else
					entry_coll.Add(name,value); // просто пихаем в словарь
			}
			else
				if(!Exists(name))								   
					entry_coll.Add(name,value); // просто пихаем в словарь
		}
			
		public void RemoveEntry(string name)
		{
			if(!Exists(name))
				entry_coll.Remove(name);
		}
		public override string ToString() // получение строкового представления
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
		
			
		private bool Exists(string name)
		{
			foreach(string str in entry_coll.AllKeys)
				if(str == name)
					return true;
			return false;
		}
		#endregion
	}
}
