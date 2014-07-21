using System;
using System.IO;

namespace Sum
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			int N = int.Parse(inputString);
			int sum = (1 + N) * (Math.Abs(N  - 1) + 1) / 2;
			Console.WriteLine(sum);
		}
	}
}
