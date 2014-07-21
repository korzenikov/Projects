using System;
using System.IO;

namespace UralSteaks
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] parts = inputString.Split(' ');
			int numberOfSteaks = int.Parse(parts[0]);
			int fryingPanSize = int.Parse(parts[1]);
			int numberOfSidesToFry = numberOfSteaks * 2;
			int totalMinutes;
			if (numberOfSteaks < fryingPanSize)
			{
				totalMinutes = 2;
			}
			else
			{
				int remainder;
				totalMinutes = Math.DivRem(numberOfSidesToFry, fryingPanSize, out remainder);
				if (remainder != 0)
				{
					totalMinutes++;
				}
			}

			Console.WriteLine(totalMinutes);
		}
	}
}
