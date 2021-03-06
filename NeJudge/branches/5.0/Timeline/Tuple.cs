﻿namespace Timeline
{
	public class Tuple<T1, T2>
	{
		public T1 Item1 { get; set; }
		public T2 Item2 { get; set;}
	}

	public class Tuple
	{
		public static Tuple<T1, T2> Create<T1, T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2> { Item1 = item1, Item2 = item2 };
		}
	}
}