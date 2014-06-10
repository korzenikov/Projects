using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MinHeap<T> : Heap<T>
        where T : IComparable<T>
    {
        public static MinHeap<T> Create(IEnumerable<T> values)
        {
            var heap = new MinHeap<T>();
            heap.Build(values);
            return heap;
        }

        protected override void Heapify(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int smallest;
            if (l < Values.Count && Values[l].CompareTo(Values[i]) < 0) smallest = l;
            else smallest = i;
            if (r < Values.Count && Values[r].CompareTo(Values[smallest]) < 0) smallest = r;
            if (smallest != i)
            {
                SwapElements(i, smallest);
                Heapify(smallest);
            }
        }

        protected override bool ShouldBeHigher(int i, int j)
        {
            return Values[i].CompareTo(Values[j]) < 0;
        }
    }
}