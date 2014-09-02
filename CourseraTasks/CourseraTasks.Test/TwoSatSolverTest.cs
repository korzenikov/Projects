using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class TwoSatSolverTest
    {
        [TestMethod]
        public void IsSatisfiableTest()
        {
            // (x1 ∨ x2) ∧ (¬x1 ∨ x3) ∧ (x3 ∨ x4) ∧ (¬x2 ∨ ¬x4)
            var satisfiableClauses = new[]
                {
                    new Clause(new Literal(1), new Literal(2)), 
                    new Clause(new Literal(1).Negate(), new Literal(3)),
                    new Clause(new Literal(3), new Literal(4)),
                    new Clause(new Literal(2).Negate(), new Literal(4).Negate()),
                };

            TwoSatSolver.IsSatisfiable(satisfiableClauses).Should().BeTrue();

            // (x1 ∨ x2) ∧ (¬x1 ∨ x3) ^ (¬x3 ∨ x2) ∧ (x1 ∨ x4) ∧ (¬x3 ∨ x4) ∧ (¬x2 ∨ ¬x4) 
            var unsatisfiableClauses = new[]
                {
                    new Clause(new Literal(1), new Literal(2)), 
                    new Clause(new Literal(1).Negate(), new Literal(3)),
                    new Clause(new Literal(3).Negate(), new Literal(2)),
                    new Clause(new Literal(1), new Literal(4)),
                    new Clause(new Literal(3).Negate(), new Literal(4)),
                    new Clause(new Literal(2).Negate(), new Literal(4).Negate()),
                };

            TwoSatSolver.IsSatisfiable(unsatisfiableClauses).Should().BeFalse();
        }
    }
}
