using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
	class ArrayTask
	{
		private void Run()
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int numberOfDimensions = int.Parse(tokens[0]);
			uint upperMultiplier = uint.Parse(tokens[1]);
			for (int i = 0; i < numberOfDimensions; i++)
			{
				string multiplierString = inputReader.ReadLine();
				uint multiplier = uint.Parse(multiplierString);
				Console.WriteLine(upperMultiplier / multiplier - 1);
				upperMultiplier = multiplier;
			}
		}
	}
}
