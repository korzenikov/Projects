using System;
using System.IO;

namespace Solutions.NEERC_Subregionals
{
	public class Pinocchio
	{
	    private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			int n = int.Parse(input);
			
			long[] numbers = new long[n];
			for (int i = 0; i < n; i++)
			{
				string numberString = inputReader.ReadLine();
				long number = long.Parse(numberString);
				numbers[i] = number;
			}

			long gcd  = GetGCD(numbers);
			Console.WriteLine(gcd);
		}

		private long GetGCD(long[] numbers)
		{
			long gcd = numbers[0];
			for (int i = 1; i < numbers.Length; i++)
			{
				gcd = GetGCD(gcd, numbers[i]);
			}

			return gcd;
		}

		private long GetGCD(long n1, long n2)
		{
			while (true)
			{
				if (n1 == 0)
					return n2;
				if (n2 == 0)
					return n1;
				if (n1 > n2)
					n1 = n1 % n2;
				else
					n2 = n2 % n1;
			}
		}
	}
}