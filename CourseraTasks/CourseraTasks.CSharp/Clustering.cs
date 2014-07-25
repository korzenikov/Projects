using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class Clustering
    {
        public static int GetMaxSpacing(DirectedWeightedGraph graph, int clusterCount)
        {
            int currentClusterCount = graph.NodesCount;
            var unionFind = new UnionFind(currentClusterCount);
            var edges = new PriorityQueue<WeightedEdge, int>(graph.GetEdges().Select(e => new KeyValuePair<WeightedEdge, int>(e, e.Weight)));
            while (currentClusterCount >= clusterCount)
            {
                var edge = edges.ExtractHighestPriorityElement();
                var cluster1 = unionFind.Find(edge.StartNode);
                var cluster2 = unionFind.Find(edge.EndNode);
                if (cluster1 != cluster2)
                {
                    if (currentClusterCount == clusterCount)
                    {
                        return edge.Weight;
                    }

                    unionFind.Union(cluster1, cluster2);
                    currentClusterCount--;
                }
            }

            return 0;
        }

        public static int GetMaxClusters(IEnumerable<int> numbers, int bits, int maxSpacing)
        {
            var distinctNumbers = numbers.Distinct().ToArray();
            int clusterCount = distinctNumbers.Length;
            var numberToCluster = new Dictionary<int, int>();
            
            for (int i = 0; i < clusterCount; i++)
            {
                numberToCluster.Add(distinctNumbers[i], i);
            }

            var unionFind = new UnionFind(clusterCount);
            for (int spacing = 1; spacing <= maxSpacing; spacing++)
            {
                foreach (var number in distinctNumbers)
                {
                    var modifications = GetModifications(number, bits, spacing, 0).ToArray();
                    foreach (var modification in modifications.Where(m => numberToCluster.ContainsKey(m)))
                    {
                        var cluster1 = unionFind.Find(numberToCluster[number]);
                        var cluster2 = unionFind.Find(numberToCluster[modification]);
                        if (cluster1 != cluster2)
                        {
                            unionFind.Union(cluster1, cluster2);
                            clusterCount--;
                        }
                    }
                }
            }

            return clusterCount;
        }

        public static IEnumerable<int> GetModifications(int number, int totalBits, int bitsToModify, int startBit)
        {
            for (int i = startBit; i < totalBits; i++)
            {
                var modifiedNumber = IsBitSet(number, i) ? ClearBit(number, i) : SetBit(number, i);
                if (bitsToModify == 1)
                    yield return modifiedNumber;
                else
                foreach (var n in GetModifications(modifiedNumber, totalBits, bitsToModify - 1, i + 1))
                {
                    yield return n;
                }
            }
        }

        private static bool IsBitSet(int number, int position)
        {
            return (number & (1 << position)) != 0;
        }

        private static int SetBit(int number, int position)
        {
            return number | (1 << position);
        }

        private static int ClearBit(int number, int position)
        {
            return number & ~(1 << position);
        }
    }
}
