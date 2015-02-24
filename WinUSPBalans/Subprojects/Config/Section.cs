using System;
using System.Collections.Specialized;

namespace Ini
{
	public class Section
	{
		#region ���� Secion
            
		private string name; // ��� ������
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
		private NameValueCollection entry_coll; // ������� �������
			
		#endregion
		#region ����������� Section
		public Section(string name) // �����������, ������� ��� ������
		{
			this.name  = name;
			entry_coll = new NameValueCollection();
		}
		#endregion
		#region ������ Section
			
		public void AddEntry(string name,string value) // ������.������ � ������
		{
			if(!Exists(name))								   
				entry_coll.Add(name,value); // ������ ������ � �������
		}
		public void AddEntry(string name,string value, bool overwrite) // ������.������ � ������
		{
			if(overwrite)
			{	
				if(Exists(name))
					entry_coll.Set(name,value);
				else
					entry_coll.Add(name,value); // ������ ������ � �������
			}
			else
				if(!Exists(name))								   
					entry_coll.Add(name,value); // ������ ������ � �������
		}
			
		public void RemoveEntry(string name)
		{
			if(!Exists(name))
				entry_coll.Remove(name);
		}
		public override string ToString() // ��������� ���������� �������������
		{
			string result=name  + "\r\n"; // ������� ��� ������
			// ���� �� ���� ������� �������
			//����� ����� ������� foreach
			for(int i=0; i<entry_coll.Count; i++)
			{
				result = result + entry_coll.GetKey(i) + "=" // ������. ��� �����
					+ entry_coll.Get(entry_coll.GetKey(i)) // ��� �������� �� �����
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
