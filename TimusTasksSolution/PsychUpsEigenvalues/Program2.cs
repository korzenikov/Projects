using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsychUpsEigenvalues
{
	class Program2
	{
		static TextReader GetFakeInput()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("5");
			sb.AppendLine("13 20 22 43 146");
			sb.AppendLine("4");
			sb.AppendLine("13 22 43 146");
			sb.AppendLine("5");
			sb.AppendLine("13 43 67 89 146");

			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

		private static TextReader GenerateLongInput()
		{
			Random r = new Random();
			StringBuilder resultsBuilder = new StringBuilder();
			int N = 3;
			int countOfValues = 4000;
			List<string> words = new List<string>();
			// Add random error to produce invalid input
			for (int i = 0; i < N; i++)
			{
				resultsBuilder.AppendLine(countOfValues.ToString());
				int[] values = new int[countOfValues];
				for (int j = 0; j < countOfValues; j++)
				{
					values[j] = r.Next(0, 100000);
				}
				resultsBuilder.AppendLine(string.Join(" ", values));
			}

			return new StringReader(resultsBuilder.ToString());
		}

		static void Main2(string[] args)
		{
			//TextReader inputReader = Console.In;
			//TextReader inputReader = GetFakeInput();
			TextReader inputReader = GenerateLongInput();
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
			Stopwatch stopwatch = Stopwatch.StartNew();
			Array.Sort(eigenvalues[1]);
			Array.Sort(eigenvalues[2]);
			int numberOfEigenvalues = eigenvalues[0].Count(value0 => Array.BinarySearch(eigenvalues[1], value0) >= 0 && Array.BinarySearch(eigenvalues[2], value0) >= 0);
			stopwatch.Stop();
			Console.WriteLine(numberOfEigenvalues);
			Console.WriteLine("Elapsed ticks: {0}", stopwatch.ElapsedTicks);
			Console.WriteLine("Elapsed milliseconds: {0}", stopwatch.ElapsedMilliseconds);
			Console.WriteLine("Press ENTER to exit");
			Console.ReadLine();
		}
	}
}
