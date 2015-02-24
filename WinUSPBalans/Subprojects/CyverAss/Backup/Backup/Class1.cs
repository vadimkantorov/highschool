using System;

public class MainClass :IInfoProvider
{
	public string GetValue(string username, string password)
	{
		return "Это тестовая сборка";
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
		return "Кибер-описание на \r\n несколько строчек";
	}
}
