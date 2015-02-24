using System;
using System.IO;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;

namespace WinBalans
{
	
	class IB
	{
		[STAThread]
		public static string GetValue(string data)
		{
			string text="";
			string pattern = @"-{0,1}\d{2,4},\d{2,2}";
			//////////////////////////////////////////////////////////////////////////
			
			#region Готовим данные к отправке
			byte[]  DataByte=new ASCIIEncoding().GetBytes(data);
			#endregion
			#region Первый запрос
			HttpWebRequest rq1 =(HttpWebRequest)WebRequest.Create("http://212.220.66.104/personal/check.asp");
			#region Headers
			rq1.Method = "POST";
			rq1.ContentType="application/x-www-form-urlencoded";
			rq1.ContentLength=data.Length;
			rq1.Referer="http://www.usp.ru/pages/?id=1";
			rq1.Accept = "image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*";
			rq1.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.0; .NET CLR 1.0.3705)";
			rq1.AllowAutoRedirect = false; //false
			#endregion
			#region stm1+Отправка данных
			Stream stm1=rq1.GetRequestStream();
			stm1.Write(DataByte,0,DataByte.Length);
			stm1.Close();
			#endregion
		
			#endregion
			#region Первый ответ
			HttpWebResponse rs1 = (HttpWebResponse)rq1.GetResponse(); 
			#endregion
			#region Второй запрос
			HttpWebRequest rq2 = (HttpWebRequest)WebRequest.Create("http://212.220.66.104/personal/default.asp");
			rq2.Method = "GET";
			rq2.ContentType="application/x-www-form-urlencoded";
			#region Заморочки с куки
			string cookie = rs1.Headers["Set-Cookie"].Remove(rs1.Headers["Set-Cookie"].Length-8,8);
			rq2.Headers.Add("Cookie", cookie);
			#endregion
			#endregion
			#region Второй ответ
			HttpWebResponse rs2 = (HttpWebResponse)rq2.GetResponse(); 
			#region Чтение второго ответа
			//Stream receiveStream2 = rs2.GetResponseStream();
			StreamReader readStream2 = new StreamReader(rs2.GetResponseStream(), Encoding.GetEncoding("windows-1251"));
			Char[] read2 = new Char[256];
			int count2 = readStream2.Read(read2, 0, 256 );
			while (count2 > 0) 
			{
				String str = new String(read2, 0, count2);
				text+=str;
				count2 = readStream2.Read(read2, 0, 256);
			}
			rs1.Close();
			rs2.Close();
			//receiveStream2.Close();
			readStream2.Close();
			#endregion
			#endregion
			#region Получение числа
			MatchCollection matches = Find(text, pattern);
			if(matches[0]!= null)
				return matches[0].ToString();
			else
				throw new ArgumentException("Получить статистику не удалось(возможно, имя и пароль некорректны)");
			#endregion
		}
		private static MatchCollection Find(string inputstr,string pattern)
		{
			return Regex.Matches(inputstr, pattern, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);
		}
	}    
}
