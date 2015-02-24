using System;
using System.IO;

class Program
{
	static int Fib(int n)
	{
		if(n <= 2) return 1;
		return Fib(n - 1) + Fib(n - 2);
	}

	static void Main()
	{
		int n = int.Parse(File.ReadAllText("input.txt"));
		File.WriteAllText("output.txt", Fib(n).ToString());
	}
}