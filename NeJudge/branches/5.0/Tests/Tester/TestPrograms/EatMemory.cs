using System;
using System.Collections.Generic;
using System.IO;

public class Program
{
	static int Next(int x)
	{
		return x*43495+4892348;
	}

	static void Main()
	{
		int n = int.Parse(File.ReadAllText("input.txt"));
		List<int> list = new List<int>();
		list.Add(1);
		for(int i = 1; i < n; i++)
			list.Add(Next(list[list.Count - 1]));
		File.WriteAllText("output.txt", list[list.Count - 1].ToString());
	}
}