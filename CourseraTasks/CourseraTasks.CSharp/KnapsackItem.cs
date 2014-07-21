using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CourseraTasks.CSharp
{
    public class KnapsackItem
    {
        public KnapsackItem(int cost, int weight)
        {
            Cost = cost;
            Weight = weight;
        }

        public int Cost { get; private set; }

        public int Weight { get; private set; }
    }
}