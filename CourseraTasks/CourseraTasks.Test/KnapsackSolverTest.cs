using CourseraTasks.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

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
