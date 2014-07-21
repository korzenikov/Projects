using System;
using System.IO;

namespace Eniya
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int N = int.Parse(parts[0]);
			int A = int.Parse(parts[1]);
			int B = int.Parse(parts[2]);
			Console.WriteLine(N * A * B * 2);
		}
	}
}
