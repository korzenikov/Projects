using System;
using System.IO;

namespace TwoGangsters
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int HarryShots = int.Parse(parts[0]);
			int LarryShots = int.Parse(parts[1]);
			int totalCans = HarryShots + LarryShots - 1;
			Console.WriteLine(string.Format("{0} {1}", totalCans - HarryShots, totalCans - LarryShots));
		}
	}
}
