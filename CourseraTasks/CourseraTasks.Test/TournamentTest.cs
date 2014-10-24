using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class TournamentTest
    {
        [TestMethod]
        public void GetMaxWithPredecessorSimpleTest()
        {
            var array = new[] { 10, 1, 2, 3, 9, 4, 5 };
            var result = Tournament.GetMaxWithPredecessorSimple(array);
            result.Item1.Should().Be(9);
            result.Item2.Should().Be(10);
        }

        [TestMethod]
        public void GetMaxWithPredecessorTest()
        {
            var array = new[] { 10, 1, 2, 3, 9, 4, 5 };
            var result = Tournament.GetMaxWithPredecessor(array);
            result.Item1.Should().Be(9);
            result.Item2.Should().Be(10);
        }
    }
}
