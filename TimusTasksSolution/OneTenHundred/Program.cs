using System;
using System.IO;
using System.Text;

namespace OneTenHundred
{
	class Program
	{
		private static int GetNumberAtPosition(int position)
		{
			ulong discriminant = 1 + 8 * ((ulong)position - 1);
			double sqrt = Math.Sqrt(discriminant);
			if (Math.Floor(sqrt) == sqrt)
			{
				return 1;
			}

			return 0;
		}

		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string numberString = inputReader.ReadLine();
			int number = int.Parse(numberString);
			for (int i = 0; i < number; i++)
			{
				string positionString = inputReader.ReadLine();
				int position = int.Parse(positionString);
				int numberAtPosition = GetNumberAtPosition(position);
				if (i != 0)
					Console.Write(" ");
				Console.Write(numberAtPosition);
			}
			Console.WriteLine();
		}
	}
}