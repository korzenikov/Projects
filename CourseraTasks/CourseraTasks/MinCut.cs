using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Immutable;

namespace CourseraTasks
{
    public class MinCut
    {
        public static int GetMinCutN(IEnumerable<int>[] adjacencyList, int n)
        {
            var r = new Random();
            return Enumerable.Repeat(0, n).Select(_ => GetMinCut(adjacencyList, r)).Min();
        }

        public static int GetMinCut(IEnumerable<int>[] adjacencyList, Random r)
        {
            var nodes = new List<MergedNode>();
            for (int i = 0; i < adjacencyList.Length; i++)
            {
                nodes.Add(new MergedNode(i));
            }

            while (nodes.Count > 2)
            {
                var node1 = nodes[r.Next(nodes.Count)];
                var node2 = nodes[r.Next(nodes.Count)];
                if (node1 == node2 || !AreConnected(node1, node2, adjacencyList))
                {
                    continue;
                }

                nodes.Remove(node1);
                nodes.Remove(node2);
                nodes.Add(node1.Merge(node2));
            }

            return GetCrossingEdgesCount(nodes[0], nodes[1], adjacencyList);
        }

        public static bool AreConnected(MergedNode node1, MergedNode node2, IEnumerable<int>[] adjacencyList)
        {
            return node1.Nodes.Any(n1 => node2.Nodes.Any(n2 => adjacencyList[n1].Contains(n2)));
        }

        public static int GetCrossingEdgesCount(MergedNode node1, MergedNode node2, IEnumerable<int>[] adjacencyList)
        {
            int crossingEdges = 0;
            foreach (var n1 in node1.Nodes)
            {
                foreach (var n2 in node2.Nodes)
                {
                    if (adjacencyList[n1].Contains(n2))
                        crossingEdges++;
                }
            }

            return crossingEdges;
        }

        public class MergedNode
        {
            public MergedNode(IEnumerable<int> nodes)
            {
                Nodes = nodes.ToImmutableList();
            }

            public MergedNode(int node)
            {
                Nodes = ImmutableList.Create(node);
            }

            public MergedNode Merge(MergedNode node)
            {
                return new MergedNode(Nodes.Concat(node.Nodes));
            }

            public ImmutableList<int> Nodes { get; private set; }
        }
    }
}
