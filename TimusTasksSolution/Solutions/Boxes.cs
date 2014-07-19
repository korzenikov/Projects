using System;
using System.IO;

namespace Solutions
{
	class Boxes
	{

		static void Main2(string[] args)
		{
			Boxes boxes = new Boxes();
			boxes.Run();
		}

		private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			string[] parts = input.Split(' ');
			int n = int.Parse(parts[0]);
			ulong a = ulong.Parse(parts[1]);
			ulong b = ulong.Parse(parts[2]);
			ulong combinations = GetNumberOfCombinations(n, a, b);
			Console.WriteLine(combinations);
		}

		private ulong GetNumberOfCombinations(int n, ulong a, ulong b)
		{
			if (n == 1)
				return (a + 1) * (b + 1);
			ulong[][,] layers = new ulong[n - 1 ][,];
			for (int t = n - 2; t >= 0 ; t--)
			{
				ulong[,] layer = new ulong[a + 1, b + 1];
				for (ulong i = 0; i <= a; i++)
					for (ulong j = 0; j <= b; j++)
					{
						if (t == n - 2)
						{
							layer[i, j] = (i + 1) * (j + 1);
						}
						else
						{
							ulong sum = 0;
							for (ulong p = 0; p <= i; p++)
								for (ulong r = 0; r <= j; r++)
								{
									sum += layers[t + 1][p, r];
								}

							layer[i, j] = sum;
						}

					}
				layers[t] = layer;
			}

			ulong result = 0;
			for (ulong i = 0; i <= a; i++)
				for (ulong j = 0; j <= b; j++)
				{
					result += layers[0][i, j];
				}
			return result;
		}
	}
}
