using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class Knapsack
    {
        public Knapsack(IEnumerable<KnapsackItem> items, int capacity)
        {
            Items = items.ToArray();
            Capacity = capacity;
        }

        public IReadOnlyList<KnapsackItem> Items { get; private set; }

        public int Capacity { get; private set; }
    }
}
