using System;
using System.IO;

namespace Chocolate2
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int m = int.Parse(parts[0]);
			int n = int.Parse(parts[1]);
			string resultString;
			if (m % 2 == 0 || n % 2 == 0)
				resultString = "[:=[first]";
			else
				resultString = "[second]=:]";
			Console.WriteLine(resultString);
		}
	}
}
