using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void MinimumTest()
        {
            const int N = 100;
            var minHeap = new MinHeap<int>(Enumerable.Range(0, N).Reverse());

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractTop().Should().Be(i);
            }
        }

        [TestMethod]
        public void MinimumTest2()
        {
            const int N = 100;
            var minHeap = new MinHeap<int, int>(Enumerable.Range(0, N).Select(x => new KeyValuePair<int, int>(x, x)).Reverse());

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractTop().Should().Be(i);
            }
        }
    }
}
