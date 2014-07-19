using System;
using System.IO;

namespace Hotel
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string numberString = inputReader.ReadLine();
			int number = int.Parse(numberString);
			int[,] numbers = new int[number, number];
			int t = 1;
			for (int i = 0; i < number; i++)
				for (int j = 0; j < i + 1; j++)
					numbers[j, number - 1 - i + j ] = t++;
			
			for (int i = 0; i < number - 1; i++)
				for (int j = 0; j < number - 1 - i; j++)
					numbers[i + j + 1, j] = t++;

			for (int i = 0; i < number; i++)
			{
				for (int j = 0; j < number; j++)
				{
					if (j != 0)
						Console.Write(" ");
					Console.Write("{0}", numbers[i, j]);
				}
				Console.WriteLine();
			}
		}
	}
}
