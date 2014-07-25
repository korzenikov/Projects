using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class OptimalSearchTreeFinderTest
    {
        [TestMethod]
        public void GetMinAverageSearchTimeTest()
        {
            double result = OptimalSearchTreeFinder.GetMinAverageSearchTime(new[] { 0.05, 0.4, 0.08, 0.04, 0.1, 0.1, 0.23 });
            result.Should().Be(2.18);
        }

        [TestMethod]
        public void GetTreeTest()
        {
            var tree = OptimalSearchTreeFinder.GetTree(new[] { 0.05, 0.4, 0.08, 0.04, 0.1, 0.1, 0.23 });
        }
    }
}
