using CrosswordSolverLib.LineBuilderClasses;
using CrosswordSolverLib.RegexClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class ParseAndGenerateTestClass : BaseTestClass
    {

        [TestMethod]
        public void ParseAndGenerateExpressionTest1()
        {
            int inputLength = 12;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            string regexPattern = "[am]+cm(rc)*r?";

            RegexParser parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            lines = builder.GetLines(regex).ToList();
            Assert.AreNotEqual(0, lines.Count);
            // Check against regular expression
            
            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest2()
        {
            int inputLength = 10;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            string regexPattern = "(o|rhh|mm)*";

            RegexParser parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            lines = builder.GetLines(regex).ToList();
            Assert.AreNotEqual(0, lines.Count);
            // Check against regular expression

            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest3()
        {
            int inputLength = 4;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            string regexPattern = "([^emc]|em)*";

            RegexParser parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            lines = builder.GetLines(regex).Take(10).ToList();
            Assert.IsTrue(lines.Count  > 1);
            // Check against regular expression

            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest4()
        {
            int inputLength = 7;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            string regexPattern = ".*g.*v.*h.*";

            RegexParser parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            lines = builder.GetLines(regex).Take(10).ToList();
            Assert.AreNotEqual(0, lines.Count);
            // Check against regular expression

            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest5()
        {
            int inputLength = 12;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            List<string> lines;

            string regexPattern = @".+(.)(.)(.)(.)\4\3\2\1.*";

            RegexParser parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            lines = builder.GetLines(regex).Take(10).ToList();

            Assert.AreNotEqual(0, lines.Count);
            // Check against regular expression

            CheckGeneratedLines(lines, regexPattern);
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest6()
        {
            int inputLength = 12;
            var input = new string('\0', inputLength);
            var builder = new LineBuilder(input, null);

            string regexPattern = ".*";

            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            var line = builder.GetLines(regex).First();
        }

        [TestMethod]
        public void ParseAndGenerateExpressionTest7()
        {
            //var input = "\0\0\0\0\0\0c\0o\0d\0";
            var input =   "\0\0\0\0\0\0c\0\0\0\0\0";
            var builder = new LineBuilder(input, null);

            string regexPattern = "[^c]*mmm[^c]*";

            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(regexPattern);

            Assert.IsFalse(builder.GetLines(regex).Any());
        }
    }
}
