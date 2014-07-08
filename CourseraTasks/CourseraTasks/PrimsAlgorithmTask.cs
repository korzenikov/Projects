using System;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public class PrimsAlgorithmTask
    {
        public void Run()
        {
            using (var reader = new StreamReader("edges.txt"))
            using (var writer = new StreamWriter("output.txt"))
            {
                var graph = new DirectedWeightedGraph();
                reader.ReadLine();
                while (true)
                {
                    string row = reader.ReadLine();
                    if (row == null)
                    {
                        break;
                    }

                    var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                    var numbers = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();
                    var node1 = numbers[0] - 1;
                    var node2 = numbers[1] - 1;
                    graph.AddEdge(node1, node2, numbers[2]);
                    graph.AddEdge(node2, node1, numbers[2]);
                }

                var length = PrimsAlgorithm.GetMinumumSpanningTreeLength(graph);
                writer.WriteLine(length);
            }
        }
    }
}
