using System;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class KnapsackSolver
    {
        public static int GetMaxCost(Knapsack knapsack)
        {
            var items = knapsack.Items.Where(item => item.Weight <= knapsack.Capacity).ToArray();
            var optimalSolution = new int[items.Length, knapsack.Capacity + 1];

            for (int w = items[0].Weight; w <= knapsack.Capacity; w++)
            {
                optimalSolution[0, w] = items[0].Cost;
            }

            for (int i = 1; i < items.Length; i++)
            {
                var item = items[i];
                for (int w = 1; w <= knapsack.Capacity; w++)
                    if (w < item.Weight)
                    {
                        optimalSolution[i, w] = optimalSolution[i - 1, w];
                    }
                    else
                    {
                        optimalSolution[i, w] = Math.Max(optimalSolution[i - 1, w], optimalSolution[i - 1, w - item.Weight] + item.Cost);
                    }
            }

            return optimalSolution[items.Length - 1, knapsack.Capacity];
        }
    }
}
