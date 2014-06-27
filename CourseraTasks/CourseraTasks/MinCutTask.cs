using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class MinCutTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("kargerMinCut.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = new DirectedGraph();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture) - 1).ToArray();
                    foreach (var number in numbers.Skip(1))
	                {
		                graph.AddEdge(numbers[0], number);
                    }
                }

                int result = Enumerable.Repeat(0, 250).Select(_ => MinCut.GetMinCut(graph)).Min();

                writer.WriteLine(result);
            }
        }
    }
}
