using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public static class Clustering
    {
        public static int GetMaxSpacing(DirectedWeightedGraph graph, int k)
        {
            int clusterCount = graph.NodesCount;
            var unionFind = new UnionFind(clusterCount);
            var edges = new PriorityQueue<WeightedEdge, int>(graph.GetEdges().Select(e => new KeyValuePair<WeightedEdge, int>(e, e.Weight)));
            while (clusterCount >= k)
            {
                var edge = edges.ExtractHighestPriorityElement();
                var cluster1 = unionFind.Find(edge.StartNode);
                var cluster2 = unionFind.Find(edge.EndNode);
                if (cluster1 != cluster2)
                {
                    if (clusterCount == k)
                    {
                        return edge.Weight;
                    }

                    unionFind.Union(cluster1, cluster2);
                    clusterCount--;
                }
            }

            return 0;
        }
    }
}
