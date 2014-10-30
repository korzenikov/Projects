using CrosswordSolverLib.CrosswordClasses;
using CrosswordSolverLib.SolverClasses;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CrosswordSolverLibTest.IntegrationTests
{
    [TestClass]
    public class SolverTestClass : BaseTestClass
    {
        #region Public Methods and Operators

        [TestMethod]
        public void SolveTest()
        {
            var solver = new Solver();

            var crossword = new MatrixCrossword(2, new[] { "(a|b)+", "b+" }, new[] { "a+b+", "(a|b)*" });

            bool solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void SolveTest2()
        {
            var solver = new Solver();
            var crossword = new MatrixCrossword(2, new[] { ".*", ".*", "(a|b)+", "b+" }, new[] { "[^a]+", "(aa|bb)*", "a+b+", "(a|b)*" });

            bool solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void SolveTest3()
        {
            var solver = new Solver();
            var crossword = new MatrixCrossword(2, new[] { "he|ll|o+", "[please]+" }, new[] { "[^speak]+", "ep|ip|ef" });

            bool solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void SolveTest4()
        {
            var solver = new Solver();

            var crossword = new MatrixCrossword(2, new[] { ".*m?o.*", "(an|fe|be)" }, new[] { @"(a|b|c)\1", "(ab|oe|sk)" });

            bool solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        [TestMethod]
        public void SolveTest6()
        {
            var solver = new Solver();

            var crossword = new MatrixCrossword(3, new[] { "(t|e|n)*", @"(.)*w+\1", "[lent]*" }, new[] { "(ent|nte|net)*", "[wear]*", "[r-z]e*[m-r]" });

            bool solved = solver.Solve(crossword);
            Assert.IsTrue(solved);
            Assert.IsTrue(crossword.IsSolved(), "Crossword has not been solved");
        }

        #endregion
    }
}