using CrosswordSolverLib.RegexBlocks;
using CrosswordSolverLib.RegexClasses;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.UnitTests
{
    [TestClass]
    public class RegexParserTest
    {
        [TestMethod]
        public void ParseTest1()
        {
            var parser = new RegexParser();
            string pattern = "a+b+";

            // Expected regular expression
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");

            var groupBlock = new AndGroupBlock(new RegexBlock[] { new OneOrMoreBlock(aBlock), new OneOrMoreBlock(bBlock) });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected,  options => options.IncludingAllRuntimeProperties(), "Pattern has been parsed incorrectly");
        }

        [TestMethod]
        public void ParseTest2()
        {
            var parser = new RegexParser();
            string pattern = "a*b*";

            // Expected regular expression
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");

            var starBlock1 = new ZeroOrMoreBlock(aBlock);
            var starBlock2 = new ZeroOrMoreBlock(bBlock);

            var groupBlock = new AndGroupBlock(new RegexBlock[] { starBlock1, starBlock2 });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest3()
        {
            var parser = new RegexParser();
            string pattern = "(ab|bc)+";

            // Expected regular expression
            var abBlock = new TextBlock("ab");
            var bcBlock = new TextBlock("bc");

            var orGroupBlock = new OrGroupBlock(new RegexBlock[] { abBlock, bcBlock });
            var plusBlock = new OneOrMoreBlock(orGroupBlock);
            var andGroupBlock = new AndGroupBlock(new[] { plusBlock });
            var expected = new RegularExpression(andGroupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest4()
        {
            var parser = new RegexParser();
            string pattern = "(a+)*";


            // Expected regular expression
            var aBlock = new TextBlock("a");

            var plusBlock = new OneOrMoreBlock(aBlock);
            var andGroupBlock = new AndGroupBlock(new[] { plusBlock });
            var starBlock = new ZeroOrMoreBlock(andGroupBlock);
            var andGroupBlock2 = new AndGroupBlock(new[] { starBlock });
            var expected = new RegularExpression(andGroupBlock2);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest5()
        {
            var parser = new RegexParser();
            string pattern = "a+b";

            // Expected regular expression
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");

            var plusBlock = new OneOrMoreBlock(aBlock);
            var andGroupBlock = new AndGroupBlock(new RegexBlock[] { plusBlock, bBlock });

            var expected = new RegularExpression(andGroupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest6()
        {
            var parser = new RegexParser();
            string pattern = "(a|b)";

            // Expected regular expression
            var aBlock = new TextBlock("a");
            var bBlock = new TextBlock("b");

            var orGroupBlock = new OrGroupBlock(new RegexBlock[] { aBlock, bBlock });

            var expected = new RegularExpression(orGroupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest7()
        {
            var parser = new RegexParser();
            string pattern = "a?";

            // Expected regular expression
            var aBlock = new TextBlock("a");
            var zeroOrOneBlock = new ZeroOrOneBlock(aBlock);
            var groupBlock = new AndGroupBlock(new RegexBlock[] { zeroOrOneBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest8()
        {
            var parser = new RegexParser();
            string pattern = "[abc]";

            // Expected regular expression
            var setBlock = new InclusiveSetBlock("abc");
            var groupBlock = new AndGroupBlock(new RegexBlock[] { setBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest9()
        {
            var parser = new RegexParser();
            string pattern = "[^abc]";

            // Expected regular expression
            var setBlock = new ExclusiveSetBlock("abc");
            var groupBlock = new AndGroupBlock(new RegexBlock[] { setBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest10()
        {
            var parser = new RegexParser();
            string pattern = ".*a.*";

            // Expected regular expression
            var anyCharacterBlock = new AnyCharacterBlock();
            var zeroOrOneBlock = new ZeroOrMoreBlock(anyCharacterBlock);
            var textBlock = new TextBlock("a");
            var groupBlock = new AndGroupBlock(new RegexBlock[] { zeroOrOneBlock, textBlock, zeroOrOneBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest11()
        {
            var parser = new RegexParser();
            string pattern = @".+(.)(.)(.)(.)\4\3\2\1.*";

            // Expected regular expression
            var anyCharacterBlock = new AnyCharacterBlock();
            var oneOrMoreBlock = new OneOrMoreBlock(anyCharacterBlock);
            var zeroOrOneBlock = new ZeroOrMoreBlock(anyCharacterBlock);
            var andGroupBlock = new AndGroupBlock(new [] { anyCharacterBlock });
            var b4 = new BackreferenceBlock(4);
            var b3 = new BackreferenceBlock(3);
            var b2 = new BackreferenceBlock(2);
            var b1 = new BackreferenceBlock(1);
            var groupBlock = new AndGroupBlock(new RegexBlock[] { oneOrMoreBlock, andGroupBlock, andGroupBlock, andGroupBlock, andGroupBlock, b4, b3, b2, b1, zeroOrOneBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest12()
        {
            var parser = new RegexParser();
            string pattern = @".*xexm*";

            // Expected regular expression
            var anyCharacterBlock = new AnyCharacterBlock();
            var zeroOrMoreBlock1 = new ZeroOrMoreBlock(anyCharacterBlock);
            var textBlock = new TextBlock("xex");

            var zeroOrMoreBlock2 = new ZeroOrMoreBlock(new TextBlock("m"));
            var groupBlock = new AndGroupBlock(new RegexBlock[] { zeroOrMoreBlock1, textBlock, zeroOrMoreBlock2 });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest13()
        {
            var parser = new RegexParser();
            string pattern = @".*h.*h.*";

            // Expected regular expression
            var anyCharacterBlock = new AnyCharacterBlock();
            var zeroOrMoreBlock = new ZeroOrMoreBlock(anyCharacterBlock);
            var textBlock = new TextBlock("h");

            var groupBlock = new AndGroupBlock(new RegexBlock[] { zeroOrMoreBlock, textBlock, zeroOrMoreBlock, textBlock, zeroOrMoreBlock });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }

        [TestMethod]
        public void ParseTest14()
        {
            var parser = new RegexParser();
            string pattern = @"[^c]*[^r]*iii.*";

            // Expected regular expression
            var textBlock = new TextBlock("iii");

            var groupBlock =
                new AndGroupBlock(
                    new RegexBlock[]
                        {
                            new ZeroOrMoreBlock(new ExclusiveSetBlock("c")), new ZeroOrMoreBlock(new ExclusiveSetBlock("r")), textBlock,
                            new ZeroOrMoreBlock(new AnyCharacterBlock())
                        });

            var expected = new RegularExpression(groupBlock);

            var actual = parser.Parse(pattern);
            actual.ShouldBeEquivalentTo(expected, options => options.IncludingAllRuntimeProperties());
        }
    }
}
