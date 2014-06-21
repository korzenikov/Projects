using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MinPriorityQueue<TElement, TPriority> : PriorityQueue<TElement, TPriority>
        where TPriority : IComparable<TPriority>
    {
        public MinPriorityQueue(IEnumerable<KeyValuePair<TElement, TPriority>> keyValuePairs)
            : base(keyValuePairs)
        {
        }

        protected override bool IsHigherPriority(TPriority priority1, TPriority priority2)
        {
            return priority1.CompareTo(priority2) < 0;
        }
    }
}