using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class Dijkstra
    {
        public static IEnumerable<int?> Find(DirectedWeightedGraph graph, int source)
        {
            var dist = new int?[graph.NodesCount];
            dist[source] = 0;
            
            var closestNodes = new PriorityQueue<int, int>(dist.Select((d, i) => new KeyValuePair<int, int>(i, d.GetValueOrDefault(int.MaxValue))));
            var exploredNodes = new HashSet<int>();

            while (closestNodes.Count != 0)
            {
                var node = closestNodes.ExtractHighestPriorityElement();
                exploredNodes.Add(node);
                foreach (var edge in graph.GetEdges(node).Where(e => !exploredNodes.Contains(e.EndNode)))
                {
                    if (dist[node] != null)
                    {
                        var alt = dist[node].Value + edge.Weight;
                        if (alt < dist[edge.EndNode].GetValueOrDefault(int.MaxValue))
                        {
                            dist[edge.EndNode] = alt;
                            closestNodes.ChangePriority(edge.EndNode, alt);
                        }
                    }
                }
            }

            return dist;
        }
    }
}
