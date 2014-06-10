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

        protected override bool ShouldBeHigher(T element1, T element2)
        {
            return element1.CompareTo(element2) < 0;
        }
    }

    public class MinHeap<TKey, TValue> : Heap<TKey, TValue>
        where TKey : IComparable<TKey>
    {
        public MinHeap(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
            : base(keyValuePairs)
        {
        }

        protected override bool ShouldBeHigher(TKey key1, TKey key2)
        {
            return key1.CompareTo(key2) < 0;
        }
    }
}