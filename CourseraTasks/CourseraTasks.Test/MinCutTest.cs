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
            var adjacencyList = new[] { new[] { 1, 3 }, new[] { 0, 2, 3 }, new[] { 1, 3 }, new[] { 0, 1, 2 } };

            var actual = MinCut.GetMinCut(adjacencyList);
            actual.Should().BeInRange(2, 3);
        }

        [TestMethod]
        public void GetMinCutNTest()
        {
            var adjacencyList = new[] { new[] { 1, 3 }, new[] { 0, 2, 3 }, new[] { 1, 3 }, new[] { 0, 1, 2 }, };
            var actual = MinCut.GetMinCutN(adjacencyList, 10000);
            actual.Should().Be(2);
        }

        [TestMethod]
        public void AreConnectedTest()
        {
            var adjacencyList = new[] { new[] { 1, 3 }, new[] { 0, 2, 3 }, new[] { 1, 3 }, new[] { 0, 1, 2 }, };

            var node0 = new MinCut.MergedNode(0);
            var node2 = new MinCut.MergedNode(2);
            var node3 = new MinCut.MergedNode(3);

            var actual1 = MinCut.AreConnected(node0, node3, adjacencyList);
            actual1.Should().Be(true);

            var actual2 = MinCut.AreConnected(node0, node2, adjacencyList);
            actual2.Should().Be(false);
        }

        [TestMethod]
        public void GetCrossingEdgesCount()
        {
            var adjacencyList = new[] { new[] { 1, 3 }, new[] { 0, 2, 3 }, new[] { 1, 3 }, new[] { 0, 1, 2 }, };

            var node0 = new MinCut.MergedNode(0);
            var node1 = new MinCut.MergedNode(1);
            var node2 = new MinCut.MergedNode(2);
            var node3 = new MinCut.MergedNode(3);

            var actual1 = MinCut.GetCrossingEdgesCount(node0.Merge(node1), node2.Merge(node3), adjacencyList);
            actual1.Should().Be(3);

            var actual2 = MinCut.GetCrossingEdgesCount(node0, node1.Merge(node2).Merge(node3), adjacencyList);
            actual2.Should().Be(2);
        }
    }
}
