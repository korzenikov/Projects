using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.LineBuilderClasses;
using System.Collections.Generic;
using System.Text;

namespace CrosswordSolverLibTest
{
    [TestClass]
    public class LineBuilderTestClass : BaseTestClass
    {                  
        [TestMethod]
        public void GetLinesFromZeroOrOneBlockTest()
        {
            int patternLength = 3;
            var pattern = new string('\0', patternLength);
            LineBuilder builder = new LineBuilder(pattern, null);
            string text = "a";
            var textBlock = new TextBlock(text);
            var zeroOrOneBlock = new ZeroOrOneBlock(textBlock);

            List<string> lines = builder.GetLinesFromZeroOrOneBlock(0, zeroOrOneBlock).ToList();

            string[] expectedLines = new[] { "", "a" };
            CollectionAssert.AreEquivalent(expectedLines, lines);
        }


        [TestMethod]
        public void GetLinesFromZeroOrMoreBlockTest()
        {
            int patternLength = 4;
            var pattern = new string('\0', patternLength);
            LineBuilder builder = new LineBuilder(pattern, null);
            string text = "a";
            var textBlock = new TextBlock(text);
            var zeroOrMoreBlock = new ZeroOrMoreBlock(textBlock);

            List<string> lines = builder.GetLinesFromZeroOrMoreBlock(0, zeroOrMoreBlock).ToList();

            Assert.AreEqual(patternLength + 1, lines.Count);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < patternLength + 1; i++)
            {
                Assert.AreEqual(sb.ToString(), lines[i]);
                sb.Append(text);
            }

            TextBlock aBlock = new TextBlock("a");
            TextBlock bBlock = new TextBlock("b");
            OrGroupBlock orGroupBlock = new OrGroupBlock(new[] { aBlock, bBlock });
            zeroOrMoreBlock = new ZeroOrMoreBlock(orGroupBlock);
            lines = builder.GetLinesFromZeroOrMoreBlock(0, zeroOrMoreBlock).ToList();
            Assert.AreEqual(31, lines.Count);
        }

        [TestMethod]
        public void GetLinesFromOneOrMoreBlockTest()
        {
            int patternLength = 4;
            var pattern = new string('\0', patternLength);
            LineBuilder builder = new LineBuilder(pattern, null);
            string text = "a";
            var textBlock = new TextBlock(text);
            OneOrMoreBlock oneOrMoreBlock = new OneOrMoreBlock(textBlock);

            List<string> lines = builder.GetLinesFromOneOrMoreBlock(0, oneOrMoreBlock).ToList();

            Assert.AreEqual(patternLength, lines.Count);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < patternLength; i++)
            {
                sb.Append(text);
                Assert.AreEqual(sb.ToString(), lines[i]);
            }

            TextBlock aBlock = new TextBlock("a");
            TextBlock bBlock = new TextBlock("b");
            OrGroupBlock orGroupBlock = new OrGroupBlock(new[] { aBlock, bBlock });
            oneOrMoreBlock = new OneOrMoreBlock(orGroupBlock);
            lines = builder.GetLinesFromOneOrMoreBlock(0, oneOrMoreBlock).ToList();
            Assert.AreEqual(30, lines.Count);
        }

        [TestMethod]
        public void GetLinesFromOrGroupBlockTest()
        {
            int patternLength = 2;
            var pattern = new string('\0', patternLength);
            LineBuilder builder = new LineBuilder(pattern, null);

            TextBlock aBlock = new TextBlock("a");
            TextBlock bBlock = new TextBlock("bb");
            TextBlock cBlock = new TextBlock("ccc");
            OrGroupBlock orGroupBlock = new OrGroupBlock(new[] { aBlock, bBlock, cBlock });
            List<string> lines = builder.GetLinesFromOrGroupBlock(0, orGroupBlock).ToList();

            string[] expectedLines = new string[] { "a", "bb"};
            CollectionAssert.AreEquivalent(expectedLines, lines);
        }

        [TestMethod]
        public void GetLinesFromAndGroupBlockTest()
        {
            int patternLength = 4;
            var pattern = new string('\0', patternLength);
            LineBuilder builder = new LineBuilder(pattern, null);
            
            // (a|aa)(b|bb)

            TextBlock aBlock = new TextBlock("a");
            TextBlock aaBlock = new TextBlock("aa");
            TextBlock bBlock = new TextBlock("b");
            TextBlock bbBlock = new TextBlock("bb");
            OrGroupBlock orGroupBlock1 = new OrGroupBlock(new[] { aBlock, aaBlock });
            OrGroupBlock orGroupBlock2 = new OrGroupBlock(new[] { bBlock, bbBlock });
            AndGroupBlock andGroupBlock = new AndGroupBlock(new[] { orGroupBlock1, orGroupBlock2 });
            List<string> lines = builder.GetLinesFromAndGroupBlock(0, andGroupBlock).ToList();

            string[] expectedLines = new string[] { "ab", "abb", "aab", "aabb" };
            CollectionAssert.AreEquivalent(expectedLines, lines);
        }
    }
}
