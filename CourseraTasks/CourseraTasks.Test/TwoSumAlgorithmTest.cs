using System.Linq;

using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class TwoSumAlgorithmTest
    {
        [TestMethod]
        public void CanGetSumTest()
        {
            var numbers1 = Enumerable.Range(1, 10).Select(x => (long)x);
            var twoSumAlgorithm1 = new TwoSumAlgorithm(numbers1);
            twoSumAlgorithm1.CanGetSum(2).Should().BeFalse();
            twoSumAlgorithm1.CanGetSum(5).Should().BeTrue();
            twoSumAlgorithm1.CanGetSum(19).Should().BeTrue();
            twoSumAlgorithm1.CanGetSum(20).Should().BeFalse();

            var numbers2 = Enumerable.Range(1, 5).Select(x => (long)x * 2);
            var twoSumAlgorithm2 = new TwoSumAlgorithm(numbers2);
            twoSumAlgorithm2.CanGetSum(7).Should().BeFalse();
            twoSumAlgorithm2.CanGetSum(12).Should().BeTrue();

            var numbers3 = Enumerable.Range(1, 10).Select(x => (long)x);
            var twoSumAlgorithm3 = new TwoSumAlgorithm(numbers3);
            int count3 = Enumerable.Range(1, 20).Count(number => twoSumAlgorithm3.CanGetSum(number));
            count3.Should().Be(17);

            var numbers4 = Enumerable.Range(1, 5).Select(x => (long)x * 2);
            var twoSumAlgorithm4 = new TwoSumAlgorithm(numbers4);
            int count4 = Enumerable.Range(1, 20).Count(number => twoSumAlgorithm4.CanGetSum(number));
            count4.Should().Be(7);
        }
    }
}
