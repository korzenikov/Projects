namespace CourseraTasks.CSharp
{
    public static class Johnson
    {
        public static int?[,] GetShortestPaths(DirectedWeightedGraph graph)
        {
            int n = graph.NodesCount;
            for (int i = 0; i < n; i++)
            {
                graph.AddEdge(n, i, 0);
            }

            var weights = BellmanFord.GetShortestPaths(graph, n);
            if (weights == null)
            {
                return null;
            }

            graph.RemoveNode(n);

            var reweightedGraph = new DirectedWeightedGraph();
            foreach (var edge in graph.GetEdges())
            {
               reweightedGraph.AddEdge(edge.StartNode, edge.EndNode, edge.Weight + weights[edge.StartNode].Value - weights[edge.EndNode].Value);
            }

            var distances = new int?[n, n];
            for (int i = 0; i < n; i++)
            {
                int j = 0;
                foreach (var distance in Dijkstra.Find(reweightedGraph, i))
                {
                    distances[i, j] = distance - weights[i] + weights[j];
                    j++;
                }
            }

            return distances;
        }
    }
}
