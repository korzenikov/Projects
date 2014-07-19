using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
	class Penguins
	{
		static string[] penguinsKinds = new[] { "Emperor Penguin", "Little Penguin", "Macaroni Penguin" };

		private void Run()
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			int numberOfPenguins = int.Parse(inputString);
			int[] numberOfPenguinsByKind = new int[penguinsKinds.Length];
			for (int i = 0; i < numberOfPenguins; i++)
			{
				string penguinKind = inputReader.ReadLine();
				int indexOfKind = Array.IndexOf(penguinsKinds, penguinKind);
				if (indexOfKind != -1)
				{
					numberOfPenguinsByKind[indexOfKind]++;
				}
			}

			int mostNumerousKindIndex = 0;
			for (int i = 1; i < penguinsKinds.Length; i++)
			{
				if (numberOfPenguinsByKind[mostNumerousKindIndex] < numberOfPenguinsByKind[i])
				{
					mostNumerousKindIndex = i;
				}
			}

			Console.WriteLine(penguinsKinds[mostNumerousKindIndex]);
		}
	}
}
