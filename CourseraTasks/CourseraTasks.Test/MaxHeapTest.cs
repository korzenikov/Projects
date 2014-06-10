using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MaxHeapTest
    {
        [TestMethod]
        public void MaximumTest()
        {
            const int N = 100;
            var minHeap = new MaxHeap<int>(Enumerable.Range(0, N));

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractTop().Should().Be(N - i - 1);
            }
        }
    }
}
