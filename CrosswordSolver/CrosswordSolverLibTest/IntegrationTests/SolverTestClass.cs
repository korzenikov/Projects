using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrosswordSolverLib.RegexClasses;
using CrosswordSolverLib.SolverClasses;
using CrosswordSolverLib.CrosswordClasses;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class SolverTestClass : BaseTestClass
    {
        [TestMethod]
        public void SolveTest()
        {
            var solver = new Solver();
            int size = 2;

            MatrixCrossword crossword = new MatrixCrossword(size, new[] { "(a|b)+", "b+" }, new[] { "a+b+", "(a|b)*" });

            var solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void SolveTest2()
        {
            var solver = new Solver();
            int size = 4;

            MatrixCrossword crossword = new MatrixCrossword(size, new[] { ".*", ".*", "(a|b)+", "b+" }, new[] { "[^a]+", "(aa|bb)*", "a+b+", "(a|b)*" });

            var solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void GetUncertaintyLevelTest()
        {
            var solver = new Solver();

            RegexParser parser = new RegexParser();


            string pattern1 = "[cr]*";

            RegularExpression regex1 = parser.Parse(pattern1);

            int actual = solver.GetUncertaintyLevel(regex1);
            Assert.AreEqual(0, actual);

            string pattern2 = "([^mc]|mm|cc)*";
            RegularExpression regex2 = parser.Parse(pattern2);
            actual = solver.GetUncertaintyLevel(regex2);
            Assert.AreEqual(1, actual);
            
            string pattern3 = ".*g.*v.*h.*";
            RegularExpression regex3 = parser.Parse(pattern3);
            actual = solver.GetUncertaintyLevel(regex3);
            Assert.AreEqual(2, actual);
        }
    }
}
