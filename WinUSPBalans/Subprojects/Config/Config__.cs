using System;
using System.IO;



namespace Config
{
	public class Config	:SectionCollection
	{
		#region ���� Config
		SectionCollection s;
		public Logger l;
		#endregion
		#region ������������ Config
		public Config(string logname):base(new Logger(logname))

		{
			try
			{
				l = new Logger(logname);
			}
			catch(IOException)
			{
				l = new Logger("#"+logname);
			}
			s = new SectionCollection(l);
		}
		public Config(string filename, string logname):base(new Logger(logname))

		{
			Logger lo = null;
			try
			{
				lo = new Logger(logname);
			}
			catch(IOException e)
			{
				lo = new Logger("#" + logname);
			}
			s = new SectionCollection(l);
			StreamReader sr = null;
			string str = "";
			Config ret = null;
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
				/*System.Windows.Forms.MessageBox.Show("������ ��� �������� �����" + e.Message + "���������, ���������� � ������������.","������.���������� ������� ���� ��� ���",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error,System.Windows.Forms.MessageBoxDefaultButton.Button1,System.Windows.Forms.MessageBoxOptions.ServiceNotification);*/
				#endregion
				lo.WriteLine(e.Message,MessageType.Error);
				str = "";
			}
			finally
			{
				sr.Close();
			}
			this = Parse(str,logname);
			this.l = lo;
		}
		#endregion
	}
}
