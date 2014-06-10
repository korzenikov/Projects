using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void MinimumTest()
        {
            const int N = 100;
            var minHeap = MinHeap<int>.Create(Enumerable.Range(0, N).Reverse());

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractTop().Should().Be(i);
            }
        }
    }
}
