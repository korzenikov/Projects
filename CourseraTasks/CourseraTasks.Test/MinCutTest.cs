using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MinCutTest
    {
        [TestMethod]
        public void GetMinCutTest()
        {
            var graph = GetTestGraph();
            var actual = MinCut.GetMinCut(graph);
            actual.Should().BeInRange(2, 3);
        }

        [TestMethod]
        public void GetCrossingEdgesCount()
        {
            var graph = GetTestGraph();

            var node0 = new MergedNode(0);
            var node1 = new MergedNode(1);
            var node2 = new MergedNode(2);
            var node3 = new MergedNode(3);

            var actual1 = MinCut.GetCrossingEdgesCount(node0.Merge(node1), node2.Merge(node3), graph);
            actual1.Should().Be(3);

            var actual2 = MinCut.GetCrossingEdgesCount(node0, node1.Merge(node2).Merge(node3), graph);
            actual2.Should().Be(2);
        }

        private static DirectedGraph GetTestGraph()
        {
            var graph = new DirectedGraph();
            graph.AddEdge(0, 1);
            graph.AddEdge(0, 3);
            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 3);
            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(3, 0);
            graph.AddEdge(3, 1);
            graph.AddEdge(3, 2);
            return graph;
        }
    }
}
