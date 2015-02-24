using System;
using System.Runtime.InteropServices;

namespace WinBalans
{
	public class WinAPI
	{
		[DllImport("user32.dll", CharSet=CharSet.Auto)]
		public static  extern int SystemParametersInfo (int uAction , int uParam , string lpvParam , int fuWinIni) ;
	} 
}
