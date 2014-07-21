using System;
using System.IO;
using System.Text;

namespace SomeWordsAboutSport
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string numberString = inputReader.ReadLine();
			int number = int.Parse(numberString);
			int[,] numbers = new int[number, number];
			for (int i = 0; i < number; i++)
			{
				string line = inputReader.ReadLine();
				string[] parts = line.Split(' ');
				for (int j = 0; j < number; j++)
				{
					numbers[i, j] = int.Parse(parts[j]);
				}
			}

			for (int i = 0; i < number; i++)
			{
				for (int j = 0; j < i + 1; j++)
				{
					Console.Write("{0} ", numbers[i - j, j]);

				}
			}

			for (int i = 0; i < number - 1; i++)
			{
				for (int j = 0; j < number - i - 1; j++)
				{
					Console.Write("{0} ", numbers[number - j - 1, i + j + 1]);
				}
			}
		}
	}
}
