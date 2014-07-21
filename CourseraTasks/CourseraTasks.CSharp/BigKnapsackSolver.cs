using CourseraTasks.CSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public class BigKnapsackSolver
    {
        private readonly IReadOnlyList<KnapsackItem> _items;

        private readonly Dictionary<int, int>[] _calculatedMaxCosts;
        private readonly int _capacity;

        public BigKnapsackSolver(Knapsack knapsack)
        {
            _items = new ReadOnlyCollection<KnapsackItem>(knapsack.Items.Where(item => item.Weight <= knapsack.Capacity).ToArray());
            _capacity = knapsack.Capacity;
            _calculatedMaxCosts = new Dictionary<int, int>[_items.Count];
            for (int i = 0; i < _items.Count; i++)
			{
                _calculatedMaxCosts[i] = new Dictionary<int, int>();
			}

        }

        public int GetMaxCost()
        {
            return GetMaxCost(_items.Count - 1, _capacity);
        }

        private int GetMaxCost(int i, int w)
        {
            if (i < 0)
                return 0;

            int cost;
            if (_calculatedMaxCosts[i].TryGetValue(w, out cost))
            {
                return cost;
            }

            var item = _items[i];

            if (w < item.Weight)
            {
                return GetMaxCost(i - 1, w);
            }

            cost = Math.Max(GetMaxCost(i - 1, w), GetMaxCost(i - 1, w - item.Weight) + item.Cost);
            _calculatedMaxCosts[i][w]  = cost;
            return  cost;
        }
    }
}
