using System.IO;

public class Program
{
	static void Main(string[] args)
	{
		File.WriteAllText("output.txt", string.Join(" ", args));
	}
}