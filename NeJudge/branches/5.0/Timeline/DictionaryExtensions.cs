using System.Collections.Generic;

namespace Timeline
{
	public static class DictionaryExtensions
	{
		public static void Increment<TKey>(this IDictionary<TKey, int> dic, TKey key)
		{
			dic[key] = GetDefaulted(dic, key) + 1;
		}

		public static TValue GetDefaulted<TKey, TValue>(this IDictionary<TKey, TValue> dic, TKey key)
		{
			TValue value;
			dic.TryGetValue(key, out value);
			return value;
		}
	}
}