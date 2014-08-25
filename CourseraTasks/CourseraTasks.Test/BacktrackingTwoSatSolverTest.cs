using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class BacktrackingTwoSatSolverTest
    {
        [TestMethod]
        public void IsSatisfiableTest()
        {
            // (x0 ∨ x1) ∧ (¬x0 ∨ x2) ∧ (x2 ∨ x3) ∧ (¬x1 ∨ ¬x3)
            var satisfiableClauses = new[]
                {
                    new Clause(new Literal(0), new Literal(1)), 
                    new Clause(new Literal(0).Negate(), new Literal(2)),
                    new Clause(new Literal(2), new Literal(3)),
                    new Clause(new Literal(1).Negate(), new Literal(3).Negate()),
                };

            BacktrackingTwoSatSolver.IsSatisfiable(satisfiableClauses).Should().BeTrue();

            // (x0 ∨ x1) ∧ (¬x0 ∨ x2) ^ (¬x2 ∨ x1) ∧ (x0 ∨ x3) ∧ (¬x2 ∨ x3) ∧ (¬x1 ∨ ¬x3) 
            var unsatisfiableClauses = new[]
                {
                    new Clause(new Literal(0), new Literal(1)), 
                    new Clause(new Literal(0).Negate(), new Literal(2)),
                    new Clause(new Literal(2).Negate(), new Literal(1)),
                    new Clause(new Literal(0), new Literal(3)),
                    new Clause(new Literal(2).Negate(), new Literal(3)),
                    new Clause(new Literal(1).Negate(), new Literal(3).Negate()),
                };

            BacktrackingTwoSatSolver.IsSatisfiable(unsatisfiableClauses).Should().BeFalse();
        }
    }
}
