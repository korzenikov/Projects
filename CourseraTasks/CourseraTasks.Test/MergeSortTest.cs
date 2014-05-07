using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class MergeSortTest
    {
        [TestMethod]
        public void SortTest()
        {
            var array1 = new[] { 5, 6, 7, 1, 2 };

            var actual1 = MergeSort.Sort(array1);

            var expected1 = new[] { 1, 2, 5, 6, 7 };

            actual1.Should().Equal(expected1);

            int[] array2 = { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var actual2 = MergeSort.Sort(array2);

            var expected2 = new[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            actual2.Should().Equal(expected2);
        }

        [TestMethod]
        public void CountInversionsTest()
        {
            var array1 = new[] { 5, 6, 7, 1, 2 };

            var actual1 = MergeSort.CountInversions(array1);

            actual1.Should().Be(6);

            int[] array2 = { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var actual2 = MergeSort.CountInversions(array2);

            actual2.Should().Be(8);

            int[] array3 = { 1, 3, 5, 2, 4, 6};

            var actual3 = MergeSort.CountInversions(array3);

            actual3.Should().Be(3);
        }
    }
}
