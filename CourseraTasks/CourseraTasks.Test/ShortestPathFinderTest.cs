using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    using CourseraTasks.CSharp;

    using FluentAssertions;

    [TestClass]
    public class ShortestPathFinderTest
    {
        [TestMethod]
        public void DepthFirstSeachTest()
        {
            var edges = new[] { new Edge(0, 1, 1), new Edge(0, 2, 4), new Edge(1, 3, 6), new Edge(2, 3, 3) };
            var nodes = ShortestPathFinder.Find(edges, 0);
            nodes.Should().Equal(new[] { 0, 1, 3, 6 });
        }
    }
}
