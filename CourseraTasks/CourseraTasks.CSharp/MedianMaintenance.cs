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
            MaxHeap<int> smallestNumbers = new MaxHeap<int>();
            MinHeap<int> largestNumbers = new MinHeap<int>();
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

                smallestNumbers.Insert(min);
                largestNumbers.Insert(next);

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
                        smallestNumbers.Insert(top);
                        largestNumbers.Insert(number);
                    }
                    else
                    {
                        smallestNumbers.Insert(number);
                    }
                }
                else
                {
                    if (number < smallestNumbers.Top)
                    {
                        var top = smallestNumbers.ExtractTop();
                        largestNumbers.Insert(top);
                        smallestNumbers.Insert(number);
                    }
                    else
                    {
                        largestNumbers.Insert(number);
                    }
                }
                yield return smallestNumbers.Top;
            }
        }
    }
}
