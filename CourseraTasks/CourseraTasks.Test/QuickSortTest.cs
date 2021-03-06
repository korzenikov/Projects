﻿using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class QuicksortTest
    {
        [TestMethod]
        public void PartitionTest()
        {
            var array1 = new[] { 5, 7, 6, 1, 2 };

            var pivot1 = Quicksort.Partition(array1, 0, array1.Length - 1, (arr, l, r) => l);

            var expected1 = new[] { 2, 1, 5, 7, 6 };

            pivot1.Should().Be(2);

            array1.Should().Equal(expected1);
            
            var array2 = new[] { 5, 7, 6, 1, 2 };

            var pivot2 = Quicksort.Partition(array2, 0, array2.Length - 1, (arr, l, r) => r);

            var expected2 = new[] { 1, 2, 6, 7, 5 };

            pivot2.Should().Be(1);

            array2.Should().Equal(expected2);

            var array3 = new[] { 5, 7, 6, 1, 2 };

            var pivot3 = Quicksort.Partition(array3, 0, array3.Length - 1, Quicksort.GetMedian);

            var expected3 = new[] { 2, 1, 5, 7, 6 };

            pivot3.Should().Be(2);

            array3.Should().Equal(expected3);
        }

        [TestMethod]
        public void SortTest()
        {
            var array1 = new[] { 5, 6, 7, 1, 2 };

            Quicksort.Sort(array1, (arr, l, r) => l);

            var expected1 = new[] { 1, 2, 5, 6, 7 };

            array1.Should().Equal(expected1);

            var array2 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            Quicksort.Sort(array2, (arr, l, r) => l);

            var expected2 = new[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            array2.Should().Equal(expected2);

            var array3 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            Quicksort.Sort(array3, Quicksort.GetMedian);

            var expected3 = new[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };

            array3.Should().Equal(expected3);
        }

        [TestMethod]
        public void SortAndCountTest()
        {
            var expected = new[] { 1, 2, 3, 4, 6, 7, 8, 9, 10 };
            
            var array1 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var count1 = Quicksort.SortAndCount(array1, (arr, l, r) => l);

            array1.Should().Equal(expected);

            count1.Should().Be(19);

            var array2 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var count2 = Quicksort.SortAndCount(array2, (arr, l, r) => r);

            array2.Should().Equal(expected);

            count2.Should().Be(27);

            var array3 = new[] { 2, 4, 6, 8, 1, 3, 7, 9, 10 };

            var count3 = Quicksort.SortAndCount(array3, Quicksort.GetMedian);

            array3.Should().Equal(expected);

            count3.Should().Be(19);
        }

        [TestMethod]
        public void GetMedianTest()
        {
            var array1 = new[] { 5, 6, 7, 1, 2 };
            var median1 = Quicksort.GetMedian(array1, 0, array1.Length - 1);
            median1.Should().Be(0);

            var array2 = new[] { 5, 6, 7, 1, 2, 10 };
            var median2 = Quicksort.GetMedian(array2, 0, array2.Length - 1);
            median2.Should().Be(2);

            Quicksort.GetMedian(new[] { 2, 7, 5 }, 0, 2).Should().Be(2);
            Quicksort.GetMedian(new[] { 5, 7, 10 }, 0, 2).Should().Be(1);
            Quicksort.GetMedian(new[] { 8, 4, 1 }, 0, 2).Should().Be(1);
            Quicksort.GetMedian(new[] { 8, 4, 10 }, 0, 2).Should().Be(0);
            Quicksort.GetMedian(new[] { 8, 4, 5 }, 0, 2).Should().Be(2);
        }
    }
}
