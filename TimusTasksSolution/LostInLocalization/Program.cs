using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LostInLocalization
{
	class Program
	{
		private static List<Tuple<int, string>> numbersToWords = new List<Tuple<int, string>>
		{
			new Tuple<int, string> (1000, "legion"),
			new Tuple<int, string> (500, "zounds"),
			new Tuple<int, string> (250, "swarm"),
			new Tuple<int, string> (100, "throng "),
			new Tuple<int, string> (50, "horde"),
			new Tuple<int, string> (20, "lots"),
			new Tuple<int, string> (10, "pack"),
			new Tuple<int, string> (5, "several"),
			new Tuple<int, string> (1, "few")
		};

		private static string TransformToWord(int number)
		{
			return numbersToWords.First(item => item.Item1 <= number).Item2;
		}

		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string numberString = inputReader.ReadLine();
			int number = int.Parse(numberString);
			Console.WriteLine(TransformToWord(number));
		}
	}
}
