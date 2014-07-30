using System;

namespace CourseraTasks.CSharp
{
    public static class FloydWarshall
    {
        public static int?[,] GetShortestPaths(DirectedWeightedGraph graph)
        {
            int n = graph.NodesCount;
            var distances = new int?[n, n];

            for (int i = 0; i < n; i++)
            {
                distances[i, i] = 0;
            }

            foreach (var edge in graph.GetEdges())
            {
                distances[edge.StartNode, edge.EndNode] = edge.Weight;
            }

            for (int k = 0; k < n; k++)
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        int? prevDistance = distances[i, j];
                        int? newDistance = distances[i, k] + distances[k, j];
                        if (prevDistance == null)
                        {
                            distances[i, j] = newDistance;
                        }
                        else if (newDistance == null)
                        {
                            distances[i, j] = prevDistance;
                        }
                        else
                        {
                            distances[i, j] = Math.Min(prevDistance.GetValueOrDefault(), newDistance.GetValueOrDefault());
                        }
                    }

            for (int i = 0; i < n; i++)
            {
                if (distances[i, i] < 0) return null;
            }

            return distances;
        }
    }
}
