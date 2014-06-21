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
            T top = _values[0];
            _values[0] = _values[_values.Count - 1];
            _values.RemoveAt(_values.Count - 1);
            Heapify(0);
            return top;
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
}
