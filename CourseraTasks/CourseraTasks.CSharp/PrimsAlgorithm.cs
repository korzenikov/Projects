using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class PrimsAlgorithm
    {
        public static int GetMinumumSpanningTreeLength(DirectedWeightedGraph graph)
        {
            int length = 0;
            var dist = new int[graph.NodesCount];
            const int StartNode = 0;
            dist[StartNode] = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (i != StartNode)
                {
                    dist[i] = int.MaxValue;
                }
            }

            var closestNodes = new PriorityQueue<int, int>(dist.Select((d, i) => new KeyValuePair<int, int>(i, d)));
            var exploredNodes = new HashSet<int>();

            while (exploredNodes.Count != graph.NodesCount)
            {
                var node = closestNodes.ExtractHighestPriorityElement();
                length += dist[node];
                exploredNodes.Add(node);
                foreach (var edge in graph.GetEdges(node).Where(e => !exploredNodes.Contains(e.EndNode)))
                {
                    var alt = edge.Weight;
                    if (alt < dist[edge.EndNode])
                    {
                        dist[edge.EndNode] = alt;
                        closestNodes.ChangePriority(edge.EndNode, alt);
                    }
                }
            }

            return length;
        }
    }
}