﻿using System;
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
        
        protected override bool IsHigherPriority(T element1, T element2)
        {
            return element1.CompareTo(element2) > 0;
        }
    }
}
