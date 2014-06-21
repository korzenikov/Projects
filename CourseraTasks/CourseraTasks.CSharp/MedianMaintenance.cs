using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public static class MedianMaintenance
    {
        public static IEnumerable<int> GetMedians(IEnumerable<int> sequence)
        {
            var enumerator = sequence.GetEnumerator();

            var smallestNumbers = new MaxPriorityQuery<int>();
            var largestNumbers = new MinPriorityQuery<int>();

            if (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                smallestNumbers.Add(number);
                yield return smallestNumbers.Max;
            }
            
            while (enumerator.MoveNext())
            {
                var number = enumerator.Current;
                if (smallestNumbers.Count <= largestNumbers.Count)
                {
                    if (number > largestNumbers.Min)
                    {
                        var min = largestNumbers.ExtractMin();
                        smallestNumbers.Add(min);
                        largestNumbers.Add(number);
                    }
                    else
                    {
                        smallestNumbers.Add(number);
                    }
                }
                else
                {
                    if (number < smallestNumbers.Max)
                    {
                        var max = smallestNumbers.ExtractMax();
                        largestNumbers.Add(max);
                        smallestNumbers.Add(number);
                    }
                    else
                    {
                        largestNumbers.Add(number);
                    }
                }

                yield return smallestNumbers.Max;
            }
        }
    }
}
