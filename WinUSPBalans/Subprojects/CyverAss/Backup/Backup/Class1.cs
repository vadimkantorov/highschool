using System;

public class MainClass :IInfoProvider
{
	public string GetValue(string username, string password)
	{
		return "��� �������� ������";
	}
	public string GetName()
	{
		return "CyverAss";
	}
	public string GetVersion()
	{
		return "2.0a";
	}
	public string GetDescription()
	{
		return "�����-�������� �� \r\n ��������� �������";
	}
}
