using System;
using System.IO;

namespace Solutions
{
	class MnemonicsAndPalindromes
	{
		static void Main2(string[] args)
		{
			MnemonicsAndPalindromes task = new MnemonicsAndPalindromes();
			task.Run();
		}

		private const byte Basis = 3;

		private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			int n = int.Parse(input);
			int totalLength = n * (n == 1 ? 3 : 6);
			if (totalLength > 100000)
			{
				Console.WriteLine("TOO LONG");
			}
			else
				GenerateCombinations(n);
		}

		private void GenerateCombinations(int n)
		{
			char[] number = new char[n];
			for (byte i = 0; i < Basis; i++)
			{
				number[0] = ConvertToChar(i);
				if (n > 1)
				{
					for (byte j = 0; j < Basis; j++)
						if (i != j)
						{
							number[1] = ConvertToChar(j);
							if (n > 2)
							{
								number[2] = ConvertToChar((byte)(3 - i - j));
								for (int t = 3; t < n; t++)
								{
									number[t] = number[t % 3];
								}

							}
							Console.WriteLine(number);
						}
				}
				else
					Console.WriteLine(number);
				
			}
		}

		private void GetPeriod(char[] number, int position)
		{
			if (position == number.Length)
			{
				Console.WriteLine(number);
			}
			else
			{
				for (byte i = 0; i < Basis; i++)
				{
					char symbol = ConvertToChar(i);
					if (position > 1 && number[position - 2] == symbol)
						continue;

					if (position > 0 && number[position - 1] == symbol)
						continue;

					number[position] = symbol;
					GetPeriod(number, position + 1);
					
				}
			}
		}

		private char ConvertToChar(byte digit)
		{
				switch (digit)
				{
					case 0:
						return 'a';
					case 1:
						return 'b';
					default:
						return 'c';
				}
		}
	}
}
