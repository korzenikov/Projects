using System.Linq;

using CrosswordSolverLib;
using CrosswordSolverLib.RegexBlocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.UnitTests
{
    [TestClass]
    public class CheckerTestClass 
    {
        #region Public Methods and Operators

        [TestMethod]
        public void GetPositionsFromAndGroupBlockTest()
        {
            int patternLength = 4;
            var pattern = new string('\0', patternLength);
            var builder = new Checker(pattern);

            // (a|aa)(b|bb)
            var aBlock = new TextBlock("a");
            var aaBlock = new TextBlock("aa");
            var bBlock = new TextBlock("b");
            var bbBlock = new TextBlock("bb");
            var orGroupBlock1 = new OrGroupBlock(new RegexBlock[] { aBlock, aaBlock });
            var orGroupBlock2 = new OrGroupBlock(new RegexBlock [] { bBlock, bbBlock });
            var andGroupBlock = new AndGroupBlock(new[] { orGroupBlock1, orGroupBlock2 });

            var positions = builder.GetPositionsFromAndGroupBlock(0, andGroupBlock).ToList();

            CollectionAssert.AreEquivalent(new[] { 2, 3, 3, 4 }, positions);
        }

        [TestMethod]
        public void GetPositionsFromOneOrMoreBlockTest()
        {
            int patternLength = 4;
            var pattern = new string('\0', patternLength);
            var builder = new Checker(pattern);
            var textBlock = new TextBlock("a");
            var oneOrMoreBlock = new OneOrMoreBlock(textBlock);

            // a+
            var positions = builder.GetPositionsFromOneOrMoreBlock(0, oneOrMoreBlock).ToList();

            CollectionAssert.AreEquivalent(Enumerable.Range(1, patternLength).ToArray(), positions);

            // (a|b)+
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");
            var orGroupBlock = new OrGroupBlock(new[] { aBlock, bBlock });
            oneOrMoreBlock = new OneOrMoreBlock(orGroupBlock);
            positions = builder.GetPositionsFromOneOrMoreBlock(0, oneOrMoreBlock).ToList();
            Assert.AreEqual(30, positions.Count);
        }

        [TestMethod]
        public void GetPositionsFromOrGroupBlockTest()
        {
            int patternLength = 2;
            var pattern = new string('\0', patternLength);
            var builder = new Checker(pattern);

            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("bb");
            var cBlock = new TextBlock("ccc");
            var orGroupBlock = new OrGroupBlock(new RegexBlock[] { aBlock, bBlock, cBlock });
            var positions = builder.GetPositionsFromOrGroupBlock(0, orGroupBlock).ToList();

            CollectionAssert.AreEquivalent(new[] { 1, 2 }, positions);
        }

        [TestMethod]
        public void GePostionsFromZeroOrMoreBlockTest()
        {
            var pattern = "a\0\0\0";
            var builder = new Checker(pattern);
            var textBlock = new TextBlock("a");
            var zeroOrMoreBlock = new ZeroOrMoreBlock(textBlock);

            var positions = builder.GetPositionsFromZeroOrMoreBlock(0, zeroOrMoreBlock).ToList();
            CollectionAssert.AreEquivalent(Enumerable.Range(0, 5).ToArray(), positions);

            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");
            var orGroupBlock = new OrGroupBlock(new[] { aBlock, bBlock });
            zeroOrMoreBlock = new ZeroOrMoreBlock(orGroupBlock);
            positions = builder.GetPositionsFromZeroOrMoreBlock(0, zeroOrMoreBlock).ToList();
            Assert.AreEqual(16, positions.Count);
        }

        [TestMethod]
        public void GetPositionsFromZeroOrOneBlockTest()
        {
            int patternLength = 3;
            var pattern = new string('\0', patternLength);
            var builder = new Checker(pattern);
            var textBlock = new TextBlock("a");
            var zeroOrOneBlock = new ZeroOrOneBlock(textBlock);

            var positions = builder.GetPositionsFromZeroOrOneBlock(0, zeroOrOneBlock).ToList();

            CollectionAssert.AreEquivalent(new[] { 0, 1 }, positions);
        }

        #endregion
    }
}