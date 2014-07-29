using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseraTasks.CSharp;
using FluentAssertions;

namespace CourseraTasks.Test
{
    [TestClass]
    public class BellmanFordTest
    {
        [TestMethod]
        public void GetShortestPathsTest()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 4);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(1, 3, 6);
            graph.AddEdge(2, 3, 3);
            var distances = BellmanFord.GetShortestPaths(graph, 0);
            distances.Should().Equal(new[] { 0, 1, 3, 6 });
        }

        [TestMethod]
        public void GetShortestPathsTest2()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, 2);
            graph.AddEdge(0, 2, 1);
            graph.AddEdge(1, 3, -5);
            graph.AddEdge(3, 2, 3);
            var distances = BellmanFord.GetShortestPaths(graph, 0);
            distances.Should().Equal(new[] { 0, 2, 0, -3 });
        }

        [TestMethod]
        public void GetShortestPathsTest3()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(2, 3, 3);
            graph.AddEdge(3, 1, -6);
            graph.AddEdge(4, 3, 7);

            var distances = BellmanFord.GetShortestPaths(graph, 0);

            distances.Should().BeNull("Graph contains a negative-weight cycle");
        }
    }
}
