using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public static class Tournament
    {
        public static Tuple<int, int> GetMaxWithPredecessorSimple(IEnumerable<int> array)
        {
            Contract.Requires(array != null);
            int comparisons = 0;
            int predecessorMax = int.MinValue;
            int max = int.MinValue;

            foreach (var element in array)
            {
                comparisons++;
                if (element > max)
                {
                    predecessorMax = max;
                    max = element;
                }
                else
                {
                    comparisons++;
                    if (element > predecessorMax)
                    {
                        predecessorMax = element;
                    }
                }
            }

            Console.WriteLine("Comparisons: {0}", comparisons);

            return Tuple.Create(predecessorMax, max);
        }

        public static Tuple<int, int> GetMaxWithPredecessor(IEnumerable<int> array)
        {
            int comparisons = 0;
            var candidates = new Queue<int>(array);
            var loosers = new Dictionary<int, List<int>>();
            while (candidates.Count > 1)
            {
                var winners = new Queue<int>();
                while (candidates.Count > 1)
                {
                    var candidate1 = candidates.Dequeue();
                    var candidate2 = candidates.Dequeue();
                    comparisons++;
                    if (candidate1 > candidate2)
                    {
                        winners.Enqueue(candidate1);
                        AddToLookup(loosers, candidate1, candidate2);
                    }
                    else
                    {
                        winners.Enqueue(candidate2);
                        AddToLookup(loosers, candidate2, candidate1);
                    }
                }

                if (candidates.Count == 1)
                {
                    winners.Enqueue(candidates.Dequeue());
                }

                candidates = winners;
            }

            int max = candidates.Count == 1 ? candidates.Dequeue() : int.MinValue;
            List<int> loosersList;
            int predecessorMax;
            if (loosers.TryGetValue(max, out loosersList))
            {
                predecessorMax = loosersList.Max();
                comparisons += Math.Max(0, loosersList.Count - 1);
            }
            else
            {
                predecessorMax = int.MinValue;
            }

            Console.WriteLine("Comparisons: {0}", comparisons);
            return Tuple.Create(predecessorMax, max);
        }

        private static void AddToLookup(IDictionary<int, List<int>> dictionary, int key, int value)
        {
            List<int> list;
            if (!dictionary.TryGetValue(key, out list))
            {
                list = new List<int>();
                dictionary[key] = list;
            }

            list.Add(value);
        }
    }
}
