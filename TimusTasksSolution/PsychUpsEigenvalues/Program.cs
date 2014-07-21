using System;
using System.IO;
using System.Linq;

namespace PsychUpsEigenvalues
{
	class Program
	{
		static void Main(string[] args)
		{
			TextReader inputReader = Console.In;
			int N = 3;
			int[][] eigenvalues = new int[N][];
			for (int i = 0; i < N; i++)
			{
				string numberString = inputReader.ReadLine();
				int number = int.Parse(numberString);
				string valuesString = inputReader.ReadLine();
				string[] parts = valuesString.Split(' ');
				int[] values = new int[number];
				for (int j = 0; j < parts.Length; j++)
				{
					values[j] = int.Parse(parts[j]);
				}
				eigenvalues[i] = values;
			}
			Array.Sort(eigenvalues[1]);
			Array.Sort(eigenvalues[2]);
			int numberOfEigenvalues = eigenvalues[0].Count(value0 => Array.BinarySearch(eigenvalues[1], value0) >= 0 && Array.BinarySearch(eigenvalues[2], value0) >= 0);
			Console.WriteLine(numberOfEigenvalues);
		}
	}
}
