using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class KnapsackSolver
    {
        public static int GetMaxCost(Knapsack knapsack)
        {
            var items = knapsack.Items.Where(item => item.Weight <= knapsack.Capacity).ToArray();
            var A = new int[items.Length, knapsack.Capacity + 1];

            for (int w = items[0].Weight; w <= knapsack.Capacity; w++)
			{
			    A[0, w] = items[0].Cost;
            }

            for (int i = 1; i < items.Length; i++)
            {
                var item = items[i];
                for (int w = 1; w <= knapsack.Capacity; w++)
			    {
                    if (w < item.Weight)
                    {
                        A[i, w] = A[i - 1, w];
                    }
                    else
                    {
                        A[i, w] = Math.Max(A[i - 1, w], A[i - 1, w - item.Weight] + item.Cost);
                    }
                }
   
            }

            return A[items.Length - 1, knapsack.Capacity];
        }

    }
}
