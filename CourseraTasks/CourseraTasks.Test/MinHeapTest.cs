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
        public void ExtractMinTest()
        {
            const int N = 100;
            var minHeap = new MinHeap<int>(Enumerable.Range(0, N).Reverse());

            for (int i = 0; i < N; i++)
            {
                minHeap.ExtractMin().Should().Be(i);
            }
        }
    }
}
