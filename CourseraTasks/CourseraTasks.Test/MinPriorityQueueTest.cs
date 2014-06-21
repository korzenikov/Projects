using System.Collections.Generic;
using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MinPriorityQueueTest
    {
        [TestMethod]
        public void ExtractHighestPriorityElementTest()
        {
            const int N = 100;
            var minHeap = new MinPriorityQueue<int, int>(Enumerable.Range(0, N).Select(x => new KeyValuePair<int, int>(x, x)).Reverse());

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractHighestPriorityElement().Should().Be(i);
            }
        }
    }
}
