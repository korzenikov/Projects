using System;
using System.IO;
using System.Numerics;

namespace ChernobylEagles
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			int N = int.Parse(inputString);
			int remainder;
			int factor = 3;
			int integer = Math.DivRem(N, factor, out remainder);
			if (integer != 0 && remainder == 1)
			{
				integer--;
				remainder += factor;
			}
			BigInteger total = BigInteger.Pow(factor, integer);
			if (remainder != 0)
				total *= remainder;
			Console.WriteLine(total);
		}
	}
}
