using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

using CourseraTasks.CSharp;

namespace CourseraTasks
{
    public static class KnapsackReader
    {
        public static Knapsack GetKnapsack(TextReader reader)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            var firstRow = reader.ReadLine();
            var firtsRowParts = firstRow.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            int capacity = int.Parse(firtsRowParts[0]);
            int numberOfItems = int.Parse(firtsRowParts[1]);
            
            var items = new List<KnapsackItem>(numberOfItems);
            while (true)
            {
                string row = reader.ReadLine();
                if (row == null)
                {
                    break;
                }

                var parts = row.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
                var parameters = parts.Select(x => int.Parse(x, CultureInfo.InvariantCulture)).ToArray();

                items.Add(new KnapsackItem(parameters[0], parameters[1]));
            }

            return new Knapsack(items, capacity);
        }
    }
}
