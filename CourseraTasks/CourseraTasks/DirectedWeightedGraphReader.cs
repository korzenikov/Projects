using System;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public static class DirectedWeightedGraphReader
    {
        public static DirectedWeightedGraph GetGraph(TextReader reader, bool makeUndirected)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
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
                var weight = numbers[2];
                graph.AddEdge(node1, node2, weight);
                if (makeUndirected)
                {
                    graph.AddEdge(node2, node1, weight);
                }
            }

            return graph;
        }
    }
}
