using CrosswordSolverLib;
using CrosswordSolverLib.RegexClasses;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class ParseAndCheckTestClass 
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ParseAndGenerateExpressionTest1()
        {
            Assert.Inconclusive();
            //int inputLength = 12;
            //var input = new string('\0', inputLength);
            //var builder = new LineBuilder(input, null);

            //List<string> lines;

            //string regexPattern = "[am]+cm(rc)*r?";

            //var parser = new RegexParser();
            //RegularExpression regex = parser.Parse(regexPattern);

            //lines = builder.GetLines(regex).ToList();
            //Assert.AreNotEqual(0, lines.Count);

            //// Check against regular expression
            //CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest2()
        {
            Assert.Inconclusive();
            //int inputLength = 10;
            //var input = new string('\0', inputLength);
            //var builder = new LineBuilder(input, null);

            //List<string> lines;

            //string regexPattern = "(o|rhh|mm)*";

            //var parser = new RegexParser();
            //RegularExpression regex = parser.Parse(regexPattern);

            //lines = builder.GetLines(regex).ToList();
            //Assert.AreNotEqual(0, lines.Count);

            //// Check against regular expression
            //CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest3()
        {
            Assert.Inconclusive();
            //int inputLength = 4;
            //var input = new string('\0', inputLength);
            //var builder = new LineBuilder(input, null);

            //List<string> lines;

            //string regexPattern = "([^emc]|em)*";

            //var parser = new RegexParser();
            //RegularExpression regex = parser.Parse(regexPattern);

            //lines = builder.GetLines(regex).Take(10).ToList();
            //Assert.IsTrue(lines.Count > 1);

            //// Check against regular expression
            //CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndCheckExpressionTest4()
        {
            var builder = new Checker("\0\0h\0\0\0");

            string regexPattern = ".*g.*v.*h.*";

            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            builder.Check(regex).Should().BeTrue();
        }

        [TestMethod]
        public void ParseAndCheckExpressionTest5()
        {
            string regexPattern = @".+(.)(.)(.)(.)\4\3\2\1.*";
            var parser = new RegexParser();
            var regex = parser.Parse(regexPattern);

            var input1 = new string('\0', 12);

            var builder1 = new Checker(input1);
            builder1.Check(regex).Should().BeTrue();

            var builder2 = new Checker("\0\0\0\0\0\0\0\0a\0\0\0");
            builder2.Check(regex).Should().BeTrue();
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest6()
        {
            Assert.Inconclusive();
            //int inputLength = 12;
            //var input = new string('\0', inputLength);
            //var builder = new LineBuilder(input, null);

            //string regexPattern = ".*";

            //var parser = new RegexParser();
            //RegularExpression regex = parser.Parse(regexPattern);

            //string line = builder.GetLines(regex).First();
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest7()
        {
            Assert.Inconclusive();
            // var input = "\0\0\0\0\0\0c\0o\0d\0";
            //string input = "\0\0\0\0\0\0c\0\0\0\0\0";
            //var builder = new LineBuilder(input, null);

            //string regexPattern = "[^c]*mmm[^c]*";

            //var parser = new RegexParser();
            //RegularExpression regex = parser.Parse(regexPattern);

            //Assert.IsFalse(builder.GetLines(regex).Any());
        }

        [TestMethod]
        public void ParseAndCheckExpressionTest8()
        {
            string regexPattern = ".*xexm*";
            var parser = new RegexParser();
            var regex = parser.Parse(regexPattern);

            //var input1 = new string('\0', 12);

            //var builder1 = new Checker(input1);
            //builder1.Check(regex).Should().BeTrue();

            var builder2 = new Checker("\0\0\0\0\0\0\0m\0");
            builder2.Check(regex).Should().BeTrue();
        }

        [TestMethod]
        public void ParseAndCheckExpressionTest9()
        {
            var builder = new Checker("a\0\0\0\0\0\0\0");

            string regexPattern = "r*d*m*";

            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            builder.Check(regex).Should().BeFalse();
        }


        [TestMethod]
        public void ParseAndCheckExpressionTest10()
        {
            //                         er x e im cm xmp
            var builder = new Checker("er\0\0\0m\0m\0mp");

            string regexPattern = @".*(.)c\1x\1.*";

            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            builder.Check(regex).Should().BeTrue();
        }


        #endregion
    }
}