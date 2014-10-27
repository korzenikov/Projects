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

        public static Tuple<int, int> GetMaxWithPredecessor(IEnumerable<int> sequence)
        {
            int comparisons = 0;
            var candidates = sequence.ToArray();
            var loosers = new Dictionary<int, List<int>>();
            int length = candidates.Length;
            while (length > 1)
            {
                for (int i = 0; i < length / 2; i++)
                {
                    var candidate1 = candidates[2 * i];
                    var candidate2 = candidates[2 * i + 1];
                    comparisons++;
                    if (candidate1 > candidate2)
                    {
                        candidates[i] = candidate1;
                        AddToLookup(loosers, candidate1, candidate2);
                    }
                    else
                    {
                        candidates[i] = candidate2;
                        AddToLookup(loosers, candidate2, candidate1);
                    }
                }

                if (length % 2 == 1)
                {
                    candidates[length / 2] = candidates[length - 1];
                    length = length / 2 + 1;
                }
                else
                {
                    length = length / 2;
                }
            }

            int max = candidates.Length > 1 ? candidates[0] : int.MinValue;
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
