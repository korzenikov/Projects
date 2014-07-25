using System;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class StronglyConnectedComponentsCalculatorTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("SCC.txt"))
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
                    var from = numbers[0];
                    var to = numbers[1];
                    if (from != to)
                    {
                        graph.AddEdge(from, to);
                    }
                }

                var calculator = new StronglyConnectedComponentsCalculator(graph);

                var components = calculator.GetStronglyConnectedComponents().ToArray();
                var top5Components = components.OrderByDescending(x => x.Length).Take(5).ToArray();
                var result = string.Join(",", top5Components.Select(x => x.Length));
                writer.WriteLine(result);
            }
        }
    }
}
