using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CourseraTasks.CSharp;
using FluentAssertions;
using System.Linq;

namespace CourseraTasks.Test
{
    [TestClass]
    public class TwoSumAlgorithmTest
    {
        [TestMethod]
        public void CheckSumTest()
        {
            var numbers1 = Enumerable.Range(1, 10).Select(x => (long)x);
            var twoSumAlgorithm1 = new TwoSumAlgorithm(numbers1);
            twoSumAlgorithm1.CheckSum(2).Should().BeFalse();
            twoSumAlgorithm1.CheckSum(5).Should().BeTrue();
            twoSumAlgorithm1.CheckSum(19).Should().BeTrue();
            twoSumAlgorithm1.CheckSum(20).Should().BeFalse();

            var numbers2 = Enumerable.Range(1, 5).Select(x => (long)x * 2);
            var twoSumAlgorithm2 = new TwoSumAlgorithm(numbers2);
            twoSumAlgorithm2.CheckSum(7).Should().BeFalse();
            twoSumAlgorithm2.CheckSum(12).Should().BeTrue();
        }

        [TestMethod]
        public void CheckSumsTest()
        {
            var numbers1 = Enumerable.Range(1, 10).Select(x => (long)x);
            var twoSumAlgorithm1 = new TwoSumAlgorithm(numbers1);
            int count1 = 0;
            foreach (var number in Enumerable.Range(1, 20))
            {
                if (twoSumAlgorithm1.CheckSum(number))
                {
                    count1++;
                }
            }
            count1.Should().Be(17);

            var numbers2 = Enumerable.Range(1, 5).Select(x => (long)x * 2);
            var twoSumAlgorithm2 = new TwoSumAlgorithm(numbers2);
            int count2 = 0;
            foreach (var number in Enumerable.Range(1, 20))
            {
                if (twoSumAlgorithm2.CheckSum(number))
                {
                    count2++;
                }
            }
            count2.Should().Be(7);
        }
    }
}
