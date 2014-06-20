using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public abstract class Heap<T>
    {
        private List<T> _values;

        protected Heap(IEnumerable<T> values)
        {
            Build(values);
        }

        protected Heap()
        {
            _values = new List<T>();
        }

        public int Count 
        { 
            get
            {
                return _values.Count;
            }
        }

        public T Top
        {
            get
            {
                return _values[0];
            }
        }

        public T ExtractTop()
        {
            T max = _values[0];
            _values[0] = _values[_values.Count - 1];
            _values.RemoveAt(_values.Count - 1);
            Heapify(0);
            return max;
        }

        public void Add(T value)
        {
            _values.Add(value);
            BubbleUp(_values.Count - 1);
        }

        protected abstract bool ShouldBeHigher(T element1, T element2);

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
            T temp = _values[i];
            _values[i] = _values[j];
            _values[j] = temp;
        }

        private void Build(IEnumerable<T> values)
        {
            _values = values.ToList();
            for (int i = _values.Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        private void Heapify(int i)
        {
            int l = Left(i);
            int r = Right(i);
            int indexOfElementToElevate;
            if (l < _values.Count && ShouldBeHigher(_values[l], _values[i]))
                indexOfElementToElevate = l;
            else indexOfElementToElevate = i;
            if (r < _values.Count && ShouldBeHigher(_values[r], _values[indexOfElementToElevate]))
                indexOfElementToElevate = r;
            if (indexOfElementToElevate != i)
            {
                SwapElements(i, indexOfElementToElevate);
                Heapify(indexOfElementToElevate);
            }
        }

        private void BubbleUp(int i)
        {
            while (i > 0 && ShouldBeHigher(_values[i], _values[Parent(i)]))
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }
    }

    public abstract class Heap<TKey, TValue>
    {
        private List<KeyValuePair<TKey, TValue>> _keyValuePairs;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        protected Heap(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
        {
            Build(keyValuePairs);
        }

        public bool IsEmpty
        {
            get
            {
                return _keyValuePairs.Count == 0;
            }
        }

        public TValue Top()
        {
            return _keyValuePairs[0].Value;
        }

        public TValue ExtractTop()
        {
            TValue max = _keyValuePairs[0].Value;
            _keyValuePairs[0] = _keyValuePairs[_keyValuePairs.Count - 1];
            _keyValuePairs.RemoveAt(_keyValuePairs.Count - 1);
            Heapify(0);
            return max;
        }

        public void Insert(TKey key, TValue value)
        {
            _keyValuePairs.Add(new KeyValuePair<TKey, TValue>(key, value));
            BubbleUp(_keyValuePairs.Count - 1);
        }

        public void ChangeKey(TValue value, TKey key)
        {
            var keyValuePair = _keyValuePairs.Single(pair => pair.Value.Equals(value));
            var i = _keyValuePairs.IndexOf(keyValuePair);

            if (!ShouldBeHigher(key, _keyValuePairs[i].Key))
                throw new ArgumentException("Cannot assign this value to specified element", "value");

            _keyValuePairs[i] = new KeyValuePair<TKey, TValue>(key, value);
            BubbleUp(i);
        }

        protected abstract bool ShouldBeHigher(TKey key1, TKey key2);

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

        private void Build(IEnumerable<KeyValuePair<TKey, TValue>> keyValuePairs)
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
            if (l < _keyValuePairs.Count && ShouldBeHigher(_keyValuePairs[l].Key, _keyValuePairs[i].Key))
                indexOfElementToElevate = l;
            else indexOfElementToElevate = i;
            if (r < _keyValuePairs.Count && ShouldBeHigher(_keyValuePairs[r].Key, _keyValuePairs[indexOfElementToElevate].Key))
                indexOfElementToElevate = r;
            if (indexOfElementToElevate != i)
            {
                SwapElements(i, indexOfElementToElevate);
                Heapify(indexOfElementToElevate);
            }
        }

        private void BubbleUp(int i)
        {
            while (i > 0 && ShouldBeHigher(_keyValuePairs[i].Key, _keyValuePairs[Parent(i)].Key))
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }
    }
}
