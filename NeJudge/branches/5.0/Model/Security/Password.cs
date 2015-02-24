using System;

namespace Model
{
	public class Password
	{
		public int Hash { get; private set; }

		public int Salt { get; private set; }

		public bool Matches(string clearText)
		{
			return Hash == ComputeHash(clearText, Salt);
		}

		public Password(string clearText)
		{
			Salt = GenerateRandomInt();
			Hash = ComputeHash(clearText, Salt);
		}

		public Password() : this("")
		{
		}

		static int ComputeHash(string text, int salt)
		{
			return GetHashCode(text + "| TRICKY SALT: " + salt);
		}

		static int GenerateRandomInt()
		{
			return new Random().Next();
		}

		static int GetHashCode(string s)
		{
			int res = 0;
			for (int i = 0; i < s.Length; i++)
				res += s[i];
			return res;
		}
	}
}