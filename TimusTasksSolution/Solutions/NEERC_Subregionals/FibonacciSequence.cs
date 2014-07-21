using System;
using System.IO;
using System.Numerics;

namespace Solutions.NEERC_Subregionals
{
	class FibonacciSequence
	{
	    private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			string[] parts = input.Split(' ');

			int i = int.Parse(parts[0]);
			int fi = int.Parse(parts[1]); ;

			int j = int.Parse(parts[2]); ;
			int fj = int.Parse(parts[3]); ;

			int n = int.Parse(parts[4]); ;

			int f = GetFn(i, fi, j, fj, n);

			Console.WriteLine(f);
		}

		private int GetFn(int i, int fi, int j, int fj, int n)
		{
			// Consider primitive cases
			if (n == i)
			{
				return fi;
			}

			if (n == j)
			{
				return fj;
			}

			if (i > j)
			{
				int temp = j;
				j = i;
				i = temp;
				int ftemp = fj;
				fj = fi;
				fi = ftemp;
			}

			BigInteger kPrevious = 0;
			BigInteger k = 1;
			BigInteger kNext;

			for (int t = 0; t < j - i - 1; t++)
			{
				kNext = k + kPrevious;
				kPrevious = k;
				k = kNext;
			}

			BigInteger bfi = new BigInteger(fi);
			BigInteger bfj = new BigInteger(fj);
			BigInteger bNextToFi = (bfj - bfi * kPrevious) / k;
			if (i < n)
			{
				int fNext;
				int fPrevious = fi;
				int f = (int)bNextToFi;
				for (int t = 0; t < n - i - 1; t++)
				{
					fNext = f + fPrevious;
					fPrevious = f;
					f = fNext;
				}
				return f;
			}
			else
			{
				int fPrevious;
				int f = fi;
				int fNext = (int)bNextToFi;
				for (int t = 0; t < i - n; t++)
				{
					fPrevious = fNext - f;
					fNext = f;
					f = fPrevious;
				}

				return f;
			}
		}
	}
}
