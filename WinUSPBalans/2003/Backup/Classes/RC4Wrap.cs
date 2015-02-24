using System;

namespace WinBalans
{
	/// <summary>
	/// Summary description for Class2.
	/// </summary>
	public class RC4Wrap
	{
		public static string EncryptText(string text, string key)
		{
			RC4Engine myRC4Engine		= new RC4Engine();
			myRC4Engine.EncryptionKey	= key;
			myRC4Engine.InClearText		= text;
			myRC4Engine.Encrypt();
			return myRC4Engine.CryptedText;
		}

		public static string DeryptText(string text, string key)
		{
			RC4Engine myRC4Engine		= new RC4Engine();
			myRC4Engine.EncryptionKey	= key;
			myRC4Engine.CryptedText		= text;
			myRC4Engine.Decrypt();
			return myRC4Engine.InClearText;
		}
	}
}
