using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks
{
    public class DirectedWeightedGraphReader
    {
        public static DirectedWeightedGraph GetGraph(TextReader reader)
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
                var weight = numbers[2];
                graph.AddEdge(node1, node2, weight);
                graph.AddEdge(node2, node1, weight);
            }

            return graph;
        }
    }
}
