using System;
using System.IO;

namespace LongProblemStatement
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int a = int.Parse(tokens[0]);
			int b = int.Parse(tokens[1]);
			int seconds = Math.Max(2 *a + 39, 2 * b + 40);
			Console.WriteLine(seconds);
		}
	}
}
