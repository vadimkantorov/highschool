using System.IO;
using System; 
using System.Globalization;

namespace Ini
{
	public enum MessageType
	{
		Debug,
		Info,
		Warning,
		Error
	}
	public class Logger
	{
		StreamWriter sw  = null;
		private string filename = "";
		// Na vhod idet imya faila
		public Logger(string filename)
		{
			try
			{
				this.filename = filename;
				sw = new StreamWriter(filename);
			}
			catch (FileNotFoundException e)
			{
				sw.Close();
				throw new FileNotFoundException("Cannot open log: no such file:" +filename+"\n\n\n\n"+e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				throw new IOException("Cannot open log: access denied:" +filename+"\n\n\n\n"+e.Message);
			}
			catch (IOException e)
			{
				sw.Close();
				if(this.filename != "")
					throw new IOException("Path contains invalid characters!\n\n\n" + e.Message);
			}
			this.WriteLine("Начало лога", MessageType.Info);
		}
		public bool WriteLine(string message,MessageType type)
		{
			string prefix = "";
			switch (type)
			{
				case MessageType.Debug:
					prefix = "[DD] ";
					break;
				case MessageType.Info:
					prefix = "{II} ";
					break;
				case MessageType.Warning:
					prefix = "(WW) ";
					break;
				case MessageType.Error:
					prefix = "<EE> ";
					break;	
			}
			string m = DateTime.Today.ToString("F",DateTimeFormatInfo.InvariantInfo)+ "  " +prefix + message;
			try 
			{
				if(this.filename != "")
					sw.WriteLine(m);
			}
			catch (IOException)
			{
				return false; 
			}
			return true;  
		}
		public void Close()
		{
			this.WriteLine("Конец лога",MessageType.Info);
            if(sw != null) sw.Close();
		}
	}
}
