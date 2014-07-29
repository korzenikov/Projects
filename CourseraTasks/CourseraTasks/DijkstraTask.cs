using System;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class DijkstraTask : ITask
    {
        public void Run()
        {
            using (var reader = new StreamReader("dijkstraData.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = new DirectedWeightedGraph();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var from = int.Parse(parts[0], CultureInfo.InvariantCulture) - 1;
                    foreach (var part in parts.Skip(1))
                    {
                        var tuple = part.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        var to = int.Parse(tuple[0], CultureInfo.InvariantCulture) - 1;
                        var weight = int.Parse(tuple[1], CultureInfo.InvariantCulture);
                        graph.AddEdge(from, to, weight);
                    }
                }

                var vertices = new[] { 6, 36, 58, 81, 98, 114, 132, 164, 187, 196 };

                var shortestPaths = Dijkstra.Find(graph, 0).ToArray();
                var result = string.Join(",", vertices.Select(v => shortestPaths[v]));
                writer.WriteLine(result);
            }
        }
    }
}
