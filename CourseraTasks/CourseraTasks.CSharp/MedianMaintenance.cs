using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraTasks.CSharp
{
    public static class MedianMaintenance
    {
        public static IEnumerable<int> GetMedians(IEnumerable<int> sequence)
        {
            var enumerator = sequence.GetEnumerator();
            int min = 0;
            if (enumerator.MoveNext())
            {
                min = enumerator.Current;
                yield return enumerator.Current;
            }

            var smallestNumbers = new MaxHeap<int>();
            var largestNumbers = new MinHeap<int>();
            if (enumerator.MoveNext())
            {
                int next;
                if (enumerator.Current < min)
                {
                    next = min;
                    min = enumerator.Current;
                }
                else
                {
                    next = enumerator.Current;
                }

                smallestNumbers.Add(min);
                largestNumbers.Add(next);

                yield return min;
            }

            while (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                if (smallestNumbers.Count <= largestNumbers.Count)
                {
                    if (number > largestNumbers.Top)
                    {
                        var top = largestNumbers.ExtractTop();
                        smallestNumbers.Add(top);
                        largestNumbers.Add(number);
                    }
                    else
                    {
                        smallestNumbers.Add(number);
                    }
                }
                else
                {
                    if (number < smallestNumbers.Top)
                    {
                        var top = smallestNumbers.ExtractTop();
                        largestNumbers.Add(top);
                        smallestNumbers.Add(number);
                    }
                    else
                    {
                        largestNumbers.Add(number);
                    }
                }
                yield return smallestNumbers.Top;
            }
        }
    }
}
