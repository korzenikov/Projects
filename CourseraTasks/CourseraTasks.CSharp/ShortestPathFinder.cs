using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class ShortestPathFinder
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

            var closestNodes = new MinHeap<int, int>(dist.Select((d, i) => new KeyValuePair<int, int>(d, i)));
            var exploredNodes = new HashSet<int>();

            while (!closestNodes.IsEmpty)
            {
                var node = closestNodes.ExtractTop();
                exploredNodes.Add(node);
                foreach (var edge in graph.GetEdges(node).Where(e => !exploredNodes.Contains(e.EndNode)))
                {
                    var alt = dist[node] + edge.Weight;
                    if (alt < dist[edge.EndNode])
                    {
                        dist[edge.EndNode] = alt;
                        closestNodes.ChangeKey(edge.EndNode, alt);
                    }
                }
            }

            return dist;
        }
    }
}
