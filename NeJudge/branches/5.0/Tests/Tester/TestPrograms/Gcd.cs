using System;
using System.IO;

class Program
{
	static int Gcd(int a, int b)
	{
		return (b == 0) ? a : Gcd(b, a%b);
	}
	
	static void Main()
	{
		string[] numbers = File.ReadAllText("input.txt").Split();
		File.WriteAllText("output.txt", Gcd(int.Parse(numbers[0]), int.Parse(numbers[1])).ToString());
	}
}