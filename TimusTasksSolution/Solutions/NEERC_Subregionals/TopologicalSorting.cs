using System;
using System.IO;
using System.Text;

namespace Solutions.NEERC_Subregionals
{
	class TopologicalSorting
	{
	    private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			string[] parts = input.Split(' ');
			int n = int.Parse(parts[0]);
			int m = int.Parse(parts[1]);
			bool[,] matrix = new bool[n, n];
			for (int i = 0; i < m; i++)
			{
				string limitationString = inputReader.ReadLine();
				string[] limitationParts = limitationString.Split(' ');
				int before = int.Parse(limitationParts[0]) - 1;
				int after = int.Parse(limitationParts[1]) - 1;
				matrix[before, after] = true;
			}

			string orderString = inputReader.ReadLine();
			string[] orderStringParts = orderString.Split(' ');
			int[] order = new int[n];
			for (int i = 0; i < n; i++)
			{
				order[i] = int.Parse(orderStringParts[i]) - 1;
			}

			bool correct = CheckDependencies(matrix, order, n);

			Console.WriteLine(correct ? "YES" : "NO");
		}

		private bool CheckDependencies(bool[,] matrix, int[] order, int n)
		{
			for (int i = 0; i < n; i++)
			{
				int before = order[i];
				for (int j = 0; j < n; j++)
				{
					if (matrix[j, before] && Array.IndexOf(order, j) > i)
						return false;
				}
			}

			return true;
		}

		private TextReader GetFakeInput()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("5 6");
			sb.AppendLine("1 3");
			sb.AppendLine("1 4");
			sb.AppendLine("3 5");
			sb.AppendLine("5 2");
			sb.AppendLine("4 2");
			sb.AppendLine("1 2");
			sb.AppendLine("1 3 4 5 2");
			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

		private TextReader GetFakeInput2()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("5 6");
			sb.AppendLine("1 3");
			sb.AppendLine("1 4");
			sb.AppendLine("3 5");
			sb.AppendLine("5 2");
			sb.AppendLine("4 2");
			sb.AppendLine("1 2");
			sb.AppendLine("1 2 4 5 3");
			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

		private TextReader GetFakeInput3()
		{
			StringBuilder sb = new StringBuilder();
			sb.AppendLine("3 3");
			sb.AppendLine("1 2");
			sb.AppendLine("2 3");
			sb.AppendLine("3 1");
			sb.AppendLine("1 2 3");
			StringReader sr = new StringReader(sb.ToString());
			return sr;
		}

	}
}
