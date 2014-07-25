using CourseraTasks.CSharp;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CourseraTasks.Test
{
    [TestClass]
    public class KnapsackSolverTest
    {
        [TestMethod]
        public void GetMaxCostTest()
        {
            var items = new[] { new KnapsackItem(3, 4), new KnapsackItem(2, 3), new KnapsackItem(4, 2), new KnapsackItem(4, 3) };
            var knapsack = new Knapsack(items, 6);
            KnapsackSolver.GetMaxCost(knapsack).Should().Be(8);
        }
    }
}
