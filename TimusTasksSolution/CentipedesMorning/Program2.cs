using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace LongProblemStatement
{
	class Program2
	{
		private const int L_rMinutes = 2;
		static bool improved = false;

		static int maxSeconds = 0;
		static int calculatedMaxSeconds = 0;

		static Stack<bool> takenSlippers = new Stack<bool>();

		static TextReader GetFakeInput()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("90 50");
			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

		static void EmulatePuttingShoes(int leftSlippers, int rightSlippers, int unshodLeftFeet, int unshodRightFeet, int seconds)
		{
			if (improved)
				return;
			// Start with left foot
			if (unshodLeftFeet > 0)
			{
				// Case 1 - pick right slipper if any
				if (rightSlippers > 0)
				{
					takenSlippers.Push(true);
					EmulatePuttingShoes(leftSlippers, rightSlippers - 1, unshodLeftFeet, Math.Max(0, unshodRightFeet - 1), seconds + 2);
					takenSlippers.Pop();
				}
				// Case 2 - pick left slipper if any
				if (leftSlippers > 0)
				{
					takenSlippers.Push(false);
					EmulatePuttingShoes(leftSlippers - 1, rightSlippers, unshodLeftFeet - 1, unshodRightFeet, seconds + 1);
					takenSlippers.Pop();
				}
			}
			else if (unshodRightFeet > 0)
			{
				// Case 1 - pick left slipper if any
				if (leftSlippers > 0)
				{
					takenSlippers.Push(false);
					EmulatePuttingShoes(leftSlippers - 1, rightSlippers, unshodLeftFeet, unshodRightFeet, seconds + L_rMinutes);
					takenSlippers.Pop();
				}
				// Case 2 - pick right slipper if any
				if (rightSlippers > 0)
				{
					takenSlippers.Push(true);
					EmulatePuttingShoes(leftSlippers, rightSlippers - 1, unshodLeftFeet, unshodRightFeet - 1, seconds + 1);
					takenSlippers.Pop();
				}

			}
			else
			{
				if (seconds > maxSeconds)
				{
					maxSeconds = seconds;
					if (maxSeconds > calculatedMaxSeconds)
					{
						improved = true;
						int count = 0;
						var takenSlippersArray = takenSlippers.Reverse().ToArray();
						bool prev = takenSlippers.Peek();
						foreach (var slipper in takenSlippersArray)
						{
							if (slipper != prev)
							{
								Console.WriteLine("{0} {1}", prev ? "right" : "left", count);
								count = 0;
							}
							prev = slipper;
							count++;
						}
						Console.WriteLine("{0} {1}", prev ? "right" : "left", count);
					}
				}
			}
		}

		static void Main2(string[] args)
		{
			TextReader inputReader = GetFakeInput();
			//TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int a = int.Parse(tokens[0]);
			int b = int.Parse(tokens[1]);
			//int seconds = Math.Max((a - 40) * L_rMinutes + 80, 2 * b + 40);
			int seconds = Math.Max((a - 40) * L_rMinutes + 119, 2 * b + 40);
			Console.WriteLine(seconds);
			calculatedMaxSeconds = seconds;
			EmulatePuttingShoes(a, b, 40, 40, 0);
			Console.WriteLine("Emulate putting shoes: {0}", maxSeconds);

			Console.ReadLine();
		}
	}
}
