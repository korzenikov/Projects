using System;

namespace CourseraTasks.CSharp
{
    public static class FloydWarshall
    {
        public static int?[,] GetShortestPaths(DirectedWeightedGraph graph)
        {
            int n = graph.NodesCount;
            var currentDistances = new int?[n, n];

            for (int i = 0; i < n; i++)
            {
                currentDistances[i, i] = 0;
            }

            foreach (var edge in graph.GetEdges())
            {
                currentDistances[edge.StartNode, edge.EndNode] = edge.Weight;
            }

            var prevDistances = new int?[n, n];

            for (int k = 0; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        prevDistances[i, j] = currentDistances[i, j];
                    }

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                    {
                        int? prevDistance = prevDistances[i, j];
                        int? newDistance = prevDistances[i, k] + prevDistances[k, j];
                        if (prevDistance == null)
                        {
                            currentDistances[i, j] = newDistance;
                        }
                        else if (newDistance == null)
                        {
                            currentDistances[i, j] = prevDistance;
                        }
                        else
                        {
                            currentDistances[i, j] = Math.Min(prevDistance.Value, newDistance.Value);
                        }
                    }
            }

            for (int i = 0; i < n; i++)
            {
                if (currentDistances[i, i] < 0) return null;
            }

            return currentDistances;
        }
    }
}
