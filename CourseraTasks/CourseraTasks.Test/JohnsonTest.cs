using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class JohnsonTest
    {
        [TestMethod]
        public void GetShortestPathsTest()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, -2);
            graph.AddEdge(1, 2, -1);
            graph.AddEdge(2, 0, 4);
            graph.AddEdge(2, 3, 2);
            graph.AddEdge(2, 3, 2);
            graph.AddEdge(2, 4, -3);
            graph.AddEdge(2, 4, -3);
            graph.AddEdge(5, 3, 1);
            graph.AddEdge(5, 4, -4);

            var distances = Johnson.GetShortestPaths(graph);
            var elements = new int?[,]
                {
                    { 0, -2, -3, -1, -6, null }, { 3, 0, -1, 1, -4, null }, { 4, 2, 0, 2, -3, null }, { null, null, null, 0, null, null },
                    { null, null, null, null, 0, null }, { null, null, null, 1, -4, 0 }
                };

            distances.Should().BeEquivalentTo(elements);
        }

        [TestMethod]
        public void GetShortestPathsTest2()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(2, 3, 3);
            graph.AddEdge(3, 1, -6);
            graph.AddEdge(4, 3, 7);

            var distances = Johnson.GetShortestPaths(graph);

            distances.Should().BeNull("Graph contains a negative-weight cycle");
        }
    }
}
