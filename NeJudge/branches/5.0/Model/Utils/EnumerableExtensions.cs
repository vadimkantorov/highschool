using System;
using System.Collections.Generic;

namespace Model.Utils
{
	public static class EnumerableExtensions
	{
		public static int FindIndex<T>(this IEnumerable<T> col, Predicate<T> pred)
		{
			int i = 0;
			foreach (var x in col)
			{
				if (pred(x))
					return i;

				i++;
			}
			return -1;
		}

		public static IEnumerable<T> Generate<T>(Func<T> f)
		{
			while(true)
				yield return f();
		}
	}
}