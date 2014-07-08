using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class ShortestPathFinderTest
    {
        [TestMethod]
        public void FindTest()
        {
            var graph = new DirectedWeightedGraph();
            graph.AddEdge(0, 1, 1);
            graph.AddEdge(0, 2, 4);
            graph.AddEdge(1, 2, 2);
            graph.AddEdge(1, 3, 6);
            graph.AddEdge(2, 3, 3);
            var nodes = ShortestPathFinder.Find(graph, 0);
            nodes.Should().Equal(new[] { 0, 1, 3, 6 });
        }
    }
}
