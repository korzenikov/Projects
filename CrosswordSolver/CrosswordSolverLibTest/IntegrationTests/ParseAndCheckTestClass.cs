using CrosswordSolverLib;
using CrosswordSolverLib.RegexClasses;
using CrosswordSolverLib.SolverClasses;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class ParseAndCheckTestClass 
    {
        #region Public Methods and Operators

        [TestMethod]
        public void ParseAndCheckExpressionTest()
        {
            Matches(".*g.*v.*h.*", "\0\0h\0\0\0").Should().BeTrue();
            Matches(@".+(.)(.)(.)(.)\4\3\2\1.*", "\0\0\0\0\0\0\0\0a\0\0\0").Should().BeTrue();
            Matches("[^c]*mmm[^c]*", "\0\0\0\0\0\0c\0o\0d\0").Should().BeFalse();
            Matches("[^c]*mmm[^c]*", "\0\0\0\0\0\0d\0\0\0\0\0").Should().BeTrue();
            Matches(".*xexm*", "\0\0\0\0\0\0\0m\0").Should().BeTrue();
            Matches("r*d*m*", "a\0\0\0\0\0\0\0").Should().BeFalse();
            Matches(@".*(.)c\1x\1.*", "er\0\0\0m\0m\0mp").Should().BeTrue();
        }
         
        #endregion

        #region Helper Methods

        private static bool Matches(string pattern, string input)
        {
            var parser = new RegexParser();
            RegularExpression regex = parser.Parse(pattern);

            var checker = new Checker(input);
            return checker.Check(regex);
        }

        #endregion
    }
}