using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class ShortestPathFinder
    {
        public static IEnumerable<int> Find(DirectedWeightedGraph graph, int source)
        {
            var dist = new int[graph.Nodes.Count];
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
                foreach (var edge in graph.Nodes[node].Edges.Where(e => !exploredNodes.Contains(e.Node)))
                {
                    var alt = dist[node] + edge.Weight;
                    if (alt < dist[edge.Node])
                    {
                        dist[edge.Node] = alt;
                        closestNodes.ChangeKey(edge.Node, alt);
                    }
                
                }
            }

            return dist;
        }
    }
}
