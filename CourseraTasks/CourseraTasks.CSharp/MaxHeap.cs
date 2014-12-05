using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MaxHeap<T> : Heap<T>
        where T : IComparable<T>
    {
        public MaxHeap(IEnumerable<T> values) : base(values)
        {
        }

        public MaxHeap()
        {
        }

        public T Max
        {
            get
            {
                return HighestPriorityElement;
            }
        }

        public T ExtractMax()
        {
            return ExtractHighestPriorityElement();
        }

        protected override bool IsHigherPriority(T priority1, T priority2)
        {
            return priority1.CompareTo(priority2) > 0;
        }
    }
}
