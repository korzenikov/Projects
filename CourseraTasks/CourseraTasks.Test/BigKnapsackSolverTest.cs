using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class BigKnapsackSolverTest
    {
        [TestMethod]
        public void GetBigKnapsackMaxCostTest()
        {
            var items = new[] { new KnapsackItem(3, 4), new KnapsackItem(2, 3), new KnapsackItem(4, 2), new KnapsackItem(4, 3) };
            var knapsack = new Knapsack(items, 6);
            var solver = new BigKnapsackSolver(knapsack);
            solver.GetMaxCost().Should().Be(8);
        }
    }
}
