using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class Dijkstra
    {
        public static IEnumerable<int> Find(DirectedWeightedGraph graph, int source)
        {
            var dist = new int[graph.NodesCount];
            dist[source] = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (i != source)
                {
                    dist[i] = int.MaxValue;
                }
            }

            var closestNodes = new PriorityQueue<int, int>(dist.Select((d, i) => new KeyValuePair<int, int>(i, d)));
            var exploredNodes = new HashSet<int>();

            while (closestNodes.Count != 0)
            {
                var node = closestNodes.ExtractHighestPriorityElement();
                exploredNodes.Add(node);
                foreach (var edge in graph.GetEdges(node).Where(e => !exploredNodes.Contains(e.EndNode)))
                {
                    var alt = dist[node] + edge.Weight;
                    if (alt < dist[edge.EndNode])
                    {
                        dist[edge.EndNode] = alt;
                        closestNodes.ChangePriority(edge.EndNode, alt);
                    }
                }
            }

            return dist;
        }
    }
}
