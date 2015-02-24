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
		string filename = "";
		MessageType filter = MessageType.Debug;
		public Logger(string filename):this(filename,MessageType.Debug)
		{
		}
		public Logger(string filename, MessageType filter)
		{
			this.filter = filter;
			try
			{
				this.filename = filename;
				sw = new StreamWriter(filename);
			}
			catch (FileNotFoundException e)
			{
				if (sw != null)
					sw.Close();
				throw new FileNotFoundException("Cannot open log: no such file:" + filename + "\n\n\n\n" + e.Message);
			}
			catch (UnauthorizedAccessException e)
			{
				if (sw != null)
					sw.Close();
				throw new IOException("Cannot open log: access denied:" + filename + "\n\n\n\n" + e.Message);
			}
			catch (IOException e)
			{
				if (sw != null)
					sw.Close();
				//if(e is FileNotFoundException)

				if (this.filename != "")
					throw new IOException("Path contains invalid characters!\n\n\n" + e.Message);
			}
			WriteLine("Начало лога", MessageType.Info);
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
			string m = DateTime.Now.ToString("F",DateTimeFormatInfo.InvariantInfo)+ "  " +prefix + " " + message;
			try 
			{
				if(type >= filter)
					if (filename.Trim() != "" && filename != null)
						TextWriter.Synchronized(sw).WriteLine(m);
			}
			catch (IOException)
			{
				return false; 
			}
			return true;  
		}
		public void Close()
		{
			WriteLine("Конец лога",MessageType.Info);
            if(sw != null) sw.Close();
		}
		public MessageType Filter
		{
			get
			{
				return filter;
			}
			set
			{
				filter = value;
			}
		}
	}
}
