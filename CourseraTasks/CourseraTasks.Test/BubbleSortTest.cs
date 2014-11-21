using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class BubbleSortTest
    {
        [TestMethod]
        public void SortTest()
        {
            var array1 = new[] { 5, 6, 7, 1, 2 };

            var actual1 = BubbleSort.Sort(array1);

            var expected1 = new[] { 1, 2, 5, 6, 7 };

            actual1.Should().Equal(expected1);

            var array2 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var actual2 = BubbleSort.Sort(array2);

            var expected2 = new[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            actual2.Should().Equal(expected2);
        }
    }
}
