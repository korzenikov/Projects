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

        protected override bool IsHigherPriority(T element1, T element2)
        {
            return element1.CompareTo(element2) < 0;
        }
    }
}