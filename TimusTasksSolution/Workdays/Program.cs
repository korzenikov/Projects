using System;
using System.IO;

namespace Workdays
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int N = int.Parse(parts[0]);
			int M = int.Parse(parts[1]);
			Console.WriteLine(N * (M + 1));
		}
	}
}
