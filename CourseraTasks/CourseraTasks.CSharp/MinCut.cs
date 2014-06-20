﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class MinCut
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static int GetMinCut(IReadOnlyList<IEnumerable<int>> adjacencyList)
        {
            var nodes = new List<MergedNode>();
            var edges = new List<Edge>();
            for (int i = 0; i < adjacencyList.Count; i++)
            {
                nodes.Add(new MergedNode(i));
                edges.AddRange(adjacencyList[i].Select(x => new Edge(i, x)));
            }

            var random = new Random();

            while (nodes.Count > 2)
            {
                var edgeIndex = random.Next(edges.Count);
                var edge = edges[edgeIndex];
                var node1 = nodes.First(node => node.Nodes.Contains(edge.From));
                var node2 = nodes.First(node => node.Nodes.Contains(edge.To));
                edges.RemoveAt(edgeIndex);
                nodes.Remove(node1);
                nodes.Remove(node2);
                nodes.Add(node1.Merge(node2));
            }

            return GetCrossingEdgesCount(nodes[0], nodes[1], adjacencyList);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        public static int GetCrossingEdgesCount(MergedNode node1, MergedNode node2, IReadOnlyList<IEnumerable<int>> adjacencyList)
        {
            return node1.Nodes.Sum(n1 => node2.Nodes.Count(n2 => adjacencyList[n1].Contains(n2)));
        }

        private class Edge
        {
            public Edge(int from, int to)
            {
                From = from;
                To = to;
            }

            public int From { get; private set; }

            public int To { get; private set; }
        }
    }
}
