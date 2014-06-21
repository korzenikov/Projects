using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public abstract class PriorityQueue<TElement, TPriority> : Heap<TPriority>
    {
        private List<KeyValuePair<TElement, TPriority>> _keyValuePairs;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected PriorityQueue(IEnumerable<KeyValuePair<TElement, TPriority>> keyValuePairs)
        {
            Build(keyValuePairs);
        }

        public int Count
        {
            get
            {
                return _keyValuePairs.Count;
            }
        }

        public TElement Top()
        {
            return _keyValuePairs[0].Key;
        }

        public TElement ExtractHighestPriorityElement()
        {
            TElement top = _keyValuePairs[0].Key;
            _keyValuePairs[0] = _keyValuePairs[_keyValuePairs.Count - 1];
            _keyValuePairs.RemoveAt(_keyValuePairs.Count - 1);
            Heapify(0);
            return top;
        }

        public void Add(TElement key, TPriority value)
        {
            _keyValuePairs.Add(new KeyValuePair<TElement, TPriority>(key, value));
            BubbleUp(_keyValuePairs.Count - 1);
        }

        public void ChangePriority(TElement key, TPriority priority)
        {
            var keyValuePair = _keyValuePairs.Single(pair => pair.Key.Equals(key));
            var index = _keyValuePairs.IndexOf(keyValuePair);

            if (!IsHigherPriority(priority, GetPriority(index)))
                throw new ArgumentException("Cannot decrease priority of specified element", "priority");

            _keyValuePairs[index] = new KeyValuePair<TElement, TPriority>(key, priority);
            BubbleUp(index);
        }

        private static int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        private static int Left(int i)
        {
            return 2 * i + 1;
        }

        private static int Right(int i)
        {
            return 2 * i + 2;
        }

        private void SwapElements(int i, int j)
        {
            var temp = _keyValuePairs[i];
            _keyValuePairs[i] = _keyValuePairs[j];
            _keyValuePairs[j] = temp;
        }

        private void Build(IEnumerable<KeyValuePair<TElement, TPriority>> keyValuePairs)
        {
            _keyValuePairs = keyValuePairs.ToList();
            for (int i = _keyValuePairs.Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        private void Heapify(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int indexOfElementToElevate;
            if (l < Count && IsHigherPriority(GetPriority(l), GetPriority(i)))
                indexOfElementToElevate = l;
            else indexOfElementToElevate = i;
            if (r < Count && IsHigherPriority(GetPriority(r), GetPriority(indexOfElementToElevate)))
                indexOfElementToElevate = r;
            if (indexOfElementToElevate != i)
            {
                SwapElements(i, indexOfElementToElevate);
                Heapify(indexOfElementToElevate);
            }
        }

        private void BubbleUp(int i)
        {
            while (i > 0 && IsHigherPriority(GetPriority(i), GetPriority(Parent(i))))
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }

        private TPriority GetPriority(int index)
        {
            return _keyValuePairs[index].Value;
        }
    }
}