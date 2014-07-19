﻿using System.IO;

namespace Spirals
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			uint n = uint.Parse(parts[0]);
			uint m = uint.Parse(parts[1]);
			uint numberOfTurns = n > m ? 2*m - 1 : 2*n - 2;
			Console.WriteLine(numberOfTurns);
		}
	}
}
