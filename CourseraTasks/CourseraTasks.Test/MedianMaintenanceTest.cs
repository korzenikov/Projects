using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MedianMaintenanceTest
    {
        [TestMethod]
        public void GetMediansTest()
        {
            var sequence1 = Enumerable.Range(1, 10);
            var expected1 = new[] { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
            MedianMaintenance.GetMedians(sequence1).Should().Equal(expected1);
            var sequence2 = Enumerable.Range(1, 10).Reverse();
            var expected2 = new[] { 10, 9, 9, 8, 8, 7, 7, 6, 6, 5 };
            MedianMaintenance.GetMedians(sequence2).Should().Equal(expected2);
        }
    }
}
