﻿using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MaxHeap<T> : Heap<T>
        where T : IComparable<T>
    {
        public static MaxHeap<T> Create(IEnumerable<T> values)
        {
            var heap = new MaxHeap<T>();
            heap.Build(values);
            return heap;
        }

        public override void Insert(T value)
        {
            Values.Add(value);
            BubbleUp(Values.Count - 1);
        }

        protected override void Heapify(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int largest;
            if (l < Values.Count && Values[l].CompareTo(Values[i]) > 0)
                largest = l;
            else largest = i;
            if (r < Values.Count && Values[r].CompareTo(Values[largest]) > 0)
                largest = r;
            if (largest != i)
            {
                SwapElements(i, largest);
                Heapify(largest);
            }
        }

        private void BubbleUp(int i)
        {
            while (i > 0 && Values[Parent(i)].CompareTo(Values[i]) < 0)
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }
    }
}