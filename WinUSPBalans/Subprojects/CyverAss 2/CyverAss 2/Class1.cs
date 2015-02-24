#region Using directives

using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Resources;
#endregion

namespace CyverAss_2
{
	public class MainClass:IInfoProvider
	{
		public string GetValue(string username, string password)
		{
			return "Это тестовая сборка 2";
		}
		public string GetName()
		{
			return "CyverAss2";
		}
		public string GetVersion()
		{
			return "2.2a";
		}
		public string GetDescription()
		{
			return "Кибер-описание на \r\n несколько строчек\r с \n ерундой";
		}
		public bool Uninstall()
		{
			return false;
		}
	}
}
