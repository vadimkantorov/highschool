using System;
using System.Linq;

namespace Model.Utils
{
	public static class StringUtils
	{
		public static string GenerateRandomLatinString(int len)
		{
			return string.Join("", EnumerableExtensions
									.Generate(() => (char)('a' + rnd.Next(27)))
									.Take(len));

		}

		static readonly Random rnd = new Random();
	}
}