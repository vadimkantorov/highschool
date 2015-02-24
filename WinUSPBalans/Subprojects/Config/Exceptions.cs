using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;


namespace Ini
{
	public class ConfigException				:ApplicationException
	{
#region Конструкторы	
		public ConfigException()														:base(){}
		public ConfigException(string message)											:base(message){}
		public ConfigException(string message, Exception innerException)				:base(message,innerException){}
		public ConfigException(SerializationInfo info, StreamingContext context)	:base(info,context){}
		#endregion
		public override string Message
		{
			get
			{
				string msg = base.Message;
				if(InnerException != null)
					msg += Environment.NewLine + "Сообщение породившего исключения: " + InnerException.Message;
				return msg;
			}
		}
	}
	
	[Serializable]
	public class IniFormatException					:ConfigException,ISerializable
	{
		#region Конструкторы IniFormatException

		public IniFormatException()														:base(){}
		public IniFormatException(string message)											:base(message){}
		public IniFormatException(string message, Exception innerException)				:base(message,innerException){}
		public IniFormatException(string message, int line)								:this(message){this.line = line;}
		public IniFormatException(string message, int line, Exception innerException)	:this(message, innerException){this.line = line;}

		
		private IniFormatException(SerializationInfo info, StreamingContext context)	:base(info,context){line = info.GetInt32("LineNumber");}
		#endregion
		

		private int line;

		public int LineNumber{get{return line;}}

		public override string Message
		{
			get
			{
				string msg = base.Message;
				if(line != 0)
					msg += Environment.NewLine + "Номер строки, на которой произошла ошибка: " + line.ToString();
				return msg;
			}
		}

		
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("LineNumber",line);
			base.GetObjectData(info, context);
		}

	}
	
	
	[Serializable]
	public sealed class SectionNotFoundException	:ConfigException,ISerializable
	{
		#region Конструкторы SectionNotFoundException
		public SectionNotFoundException()														:base(){}
		public SectionNotFoundException(string message)											:base(message){}
		public SectionNotFoundException(string message, Exception innerException)				:base(message,innerException){}
		public SectionNotFoundException(string message, string secname)								:this(message){this.secname = secname;}
		public SectionNotFoundException(string message, string secname, Exception innerException)	:this(message, innerException)
		{
			this.secname = secname;
		}
		
		private SectionNotFoundException(SerializationInfo info, StreamingContext context)	:base(info,context)
		{
			secname = info.GetString("SectionName");
		}
		#endregion
		

		private string secname;

		public string SectionName{get{return secname;}}

		public override string Message
		{
			get
			{
				string msg = base.Message;
				if(secname != null)
					msg += Environment.NewLine + "Имя секции: " + secname;
				return msg;
			}
		}

		
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("SectionName",secname);
			base.GetObjectData(info, context);
		}

	}
}
