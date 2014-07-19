using System;
using System.Globalization;
using System.IO;
using System.Text;

namespace Elections
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			string inputString = inputReader.ReadLine();
			string[] tokens = inputString.Split(' ');
			int numberOfCandidates = int.Parse(tokens[0]);
			int numberOfElectors = int.Parse(tokens[1]);
			int[] votesForCandidates = new int[numberOfCandidates];
			for (int i = 0; i < numberOfElectors; i++)
			{
				string numberString = inputReader.ReadLine();
				int candidateNumber = int.Parse(numberString);
				votesForCandidates[candidateNumber - 1]++; 
			}

			NumberFormatInfo nfi = CultureInfo.InvariantCulture.NumberFormat;

			foreach (var votes in votesForCandidates)
			{
				double percents = (double)votes / numberOfElectors;
				Console.WriteLine(string.Format(nfi, "{0:#0.00%}", percents));
			}
		}
	}
}
