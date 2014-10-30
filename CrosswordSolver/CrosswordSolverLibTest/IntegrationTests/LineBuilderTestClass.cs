using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.LineBuilderClasses;
using CrosswordSolverLib.RegexClasses;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class LineBuilderTestClass : BaseTestClass
    {
        [TestMethod]
        public void GetLinesTestWithEmptyInput()
        {
            int inputLength = 3;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            // a+b+ - should be abb, aab
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");
            var plusBlock1 = new OneOrMoreBlock(aBlock);
            var plusBlock2 = new OneOrMoreBlock(bBlock);
            
            var groupBlock = new AndGroupBlock(new RegexBlock[] { plusBlock1, plusBlock2 });
            
            var regex1 = new RegularExpression(groupBlock);
            
            lines = builder.GetLines(regex1).ToList();
            string[] expectedLines = new string[] { "abb", "aab" };
            CollectionAssert.AreEquivalent(expectedLines, lines);

            // a*b* - should be bbb, abb, aab, aaa
            var starBlock1 = new ZeroOrMoreBlock(aBlock);
            var starBlock2 = new ZeroOrMoreBlock(bBlock);

            groupBlock = new AndGroupBlock(new RegexBlock[] { starBlock1, starBlock2 });

            var regex2 = new RegularExpression(groupBlock);

            lines = builder.GetLines(regex2).ToList();
            expectedLines = new string[] { "bbb", "abb", "aab", "aaa" };
            CollectionAssert.AreEquivalent(expectedLines, lines);
        }

        [TestMethod]
        public void TestRealWorldExpression1()
        {
            int inputLength = 10;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            var block1 = new TextBlock("o");
            var block2 = new TextBlock("rhh");
            var block3 = new TextBlock("mm");

            var orGroupBlock = new OrGroupBlock(new RegexBlock[] { block1, block2, block3 });
            var starBlock = new ZeroOrMoreBlock(orGroupBlock);
            var andGroupBlock = new OrGroupBlock(new RegexBlock[] { starBlock });
            var regex = new RegularExpression(andGroupBlock);

            lines = builder.GetLines(regex).ToList();

            // Check against regular expression
            string regexPattern = "(o|rhh|mm)*";
            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void TestRealWorldExpression2()
        {
            int inputLength = 12;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            var block1 = new TextBlock("c");
            var block2 = new TextBlock("mc");
            var block3 = new TextBlock("ccc");
            var block4 = new TextBlock("mm");
            
            
            var starBlock1 = new ZeroOrMoreBlock(block1);

            var orGroupBlock = new OrGroupBlock(new RegexBlock[] { block3, block4 });
            var starBlock2 = new ZeroOrMoreBlock(orGroupBlock);

            AndGroupBlock andGroupBlock = new AndGroupBlock(new RegexBlock  [] { starBlock1, block2, starBlock2 });

            var regex = new RegularExpression(andGroupBlock);

            lines = builder.GetLines(regex).ToList();

            // Check against regular expression
            string regexPattern = "c*mc(ccc|mm)*";
            CheckGeneratedLines(lines, regexPattern);
        }
    }
}
