using System;
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
        }
    }
}
