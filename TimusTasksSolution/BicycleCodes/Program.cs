using System;
using System.IO;

namespace BicycleCodes
{
	class Program
	{
		private static bool CanBeUnlocked(int firstLockCode, int secondLockCode)
		{
			return (firstLockCode % 2 == 0) || (secondLockCode % 2 == 1);
		}

		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string firstLockString = inputReader.ReadLine();
			int firstLockCode = int.Parse(firstLockString);
			string secondLockString = inputReader.ReadLine();
			int secondLockCode = int.Parse(secondLockString);

			bool canBeUnlocked = CanBeUnlocked(firstLockCode, secondLockCode);
			Console.WriteLine(canBeUnlocked ? "yes" : "no");
		}
	}
}
