﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class MinCut
    {
        public static int GetMinCutN(HashSet<int>[] adjacencyList, int n)
        {
            return Enumerable.Repeat(0, n).Select(_ => GetMinCut(adjacencyList, new Random((int)DateTime.Now.Ticks & 0x0000FFFF))).Min();
        }

        public static int GetMinCut(HashSet<int>[] adjacencyList, Random r)
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

        public static bool AreConnected(MergedNode node1, MergedNode node2, HashSet<int>[] adjacencyList)
        {
            return node1.Nodes.Any(n1 => node2.Nodes.Any(n2 => adjacencyList[n1].Contains(n2)));
        }

        public static int GetCrossingEdgesCount(MergedNode node1, MergedNode node2, HashSet<int>[] adjacencyList)
        {
            return node1.Nodes.Sum(n1 => node2.Nodes.Count(n2 => adjacencyList[n1].Contains(n2)));
        }

        public class MergedNode
        {
            public MergedNode(List<int> nodes)
            {
                Nodes = nodes;
            }

            public MergedNode(int node)
            {
                Nodes = new List<int> { node };
            }

            public List<int> Nodes { get; private set; }

            public MergedNode Merge(MergedNode node)
            {
                var nodes = Nodes.ToList();
                nodes.AddRange(node.Nodes);
                return new MergedNode(nodes);
            }
        }
    }
}