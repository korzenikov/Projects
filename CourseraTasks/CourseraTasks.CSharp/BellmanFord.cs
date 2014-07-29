using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public static class BellmanFord
    {
        public static IReadOnlyList<int?> GetShortestPaths(DirectedWeightedGraph graph, int source)
        {
            int n = graph.NodesCount;
            var dist = new int?[n];
            dist[source] = 0;
            for (int i = 0; i < dist.Length; i++)
            {
                if (i != source)
                {
                    dist[i] = null;
                }
            }

            for (int i = 0; i < n + 1; i++)
            {
                bool changed = false;
                foreach (var edge in graph.GetEdges())
                {
                    if (dist[edge.StartNode] != null && dist[edge.StartNode] + edge.Weight < dist[edge.EndNode].GetValueOrDefault(int.MaxValue))
                    {
                        dist[edge.EndNode] = dist[edge.StartNode] + edge.Weight;
                        changed = true;
                    }
                }

                if (!changed)
                    return dist;
            }

            return null;
        }
    }
}
