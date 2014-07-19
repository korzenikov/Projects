using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solutions.NEERC_Subregionals
{
	class IntegerPercentage
	{
	    private void Run()
		{
			TextReader inputReader = Console.In;
			string input = inputReader.ReadLine();
			string[] parts = input.Split(new [] {' '}, StringSplitOptions.RemoveEmptyEntries);
			int n = int.Parse(parts[0]);
			int s = int.Parse(parts[1]);
			Console.WriteLine(FindLongestPath(n, s));
		}
		
		private IEnumerable<int> GetNextSalaries(int maxSalary, int salary)
		{
			for (int percentage = 1; percentage <= 100; percentage++)
			{
				if (percentage * salary % 100 == 0)
				{
					int nextSalary = salary * (100 + percentage) / 100;
					if (nextSalary > maxSalary)
						yield break;
					yield return nextSalary;
				}
			}
		}

		private int FindLongestPath(int maxSalary, int startSalary)
		{
			IDictionary<int, int> longestPaths = new Dictionary<int, int>() { { startSalary, 1 }};
			SortedSet<int> salaries = new SortedSet<int>();
			salaries.Add(startSalary);

			while (salaries.Count != 0)
			{
				int salary = salaries.Min;
				int path = longestPaths[salary];

				foreach (int nextSalary in GetNextSalaries(maxSalary, salary))
				{
					if (!salaries.Contains(nextSalary))
					{
						salaries.Add(nextSalary);
						longestPaths.Add(nextSalary, path + 1);
					}
					else
						if (longestPaths[nextSalary] < path + 1)
							longestPaths[nextSalary] = path + 1;
				}

				salaries.Remove(salary);
			}

			return longestPaths.Values.Max();
		}
	}
}
