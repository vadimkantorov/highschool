using System;

public interface IInfoProvider
{
	string GetValue(string username, string password);
	string GetName();
	string GetDescription();
	string GetVersion();
	//bool Install();
	//bool Uninstall();
}

