﻿using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class ClusteringTest
    {
        [TestMethod]
        public void GetMaxSpacingTest()
        {
            var graph = new DirectedWeightedGraph();
            
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 6);
            graph.AddEdge(0, 3, 7);

            graph.AddEdge(1, 0, 1);
            graph.AddEdge(1, 2, 8);
            graph.AddEdge(1, 3, 5);

            graph.AddEdge(2, 0, 6);
            graph.AddEdge(2, 1, 8);
            graph.AddEdge(2, 3, 2);

            graph.AddEdge(3, 0, 7);
            graph.AddEdge(3, 1, 5);
            graph.AddEdge(3, 2, 2);

           Clustering.GetMaxSpacing(graph, 4).Should().Be(1);
           Clustering.GetMaxSpacing(graph, 3).Should().Be(2);
           Clustering.GetMaxSpacing(graph, 2).Should().Be(5);
        }

        [TestMethod]
        public void GetMaxClustersTest()
        {
            var numbers = new int[] { 0, 1, 2, 7 };
            int clusters = Clustering.GetMaxClusters(numbers, 3, 1);
            clusters.Should().Be(2);
        }

        [TestMethod]
        public void GetModificationsTest()
        {
            var modifications = Clustering.GetModifications(0, 24, 2, 0).ToArray();
            modifications.Should().HaveCount(276);
        }
    }
}
