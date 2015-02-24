using System;

namespace Model.Utils
{
	public class EnumUtils
	{
		public static T[] Values<T>()
		{
			return (T[]) Enum.GetValues(typeof (T));
		}
	}
}