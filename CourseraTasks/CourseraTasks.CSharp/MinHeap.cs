using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MinHeap<T> : Heap<T>
        where T : IComparable<T>
    {
        public MinHeap(IEnumerable<T> values) : base(values)
        {
        }

        public MinHeap()
        {
        }

        public T Min
        {
            get
            {
                return HighestPriorityElement;
            }
        }

        public T ExtractMin()
        {
            return ExtractHighestPriorityElement();
        }

        protected override bool IsHigherPriority(T priority1, T priority2)
        {
            return priority1.CompareTo(priority2) < 0;
        }
    }
}