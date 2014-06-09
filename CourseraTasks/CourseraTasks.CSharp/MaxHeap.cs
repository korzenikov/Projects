using System;
using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public class MaxHeap<T> : Heap<T> where T: IComparable<T>
    {
        private readonly T _minValue;

        public MaxHeap(T minValue)
        {
            _minValue = minValue;
        }

        public override void Build(IEnumerable<T> values)
        {
            Values = values.ToList();
            for (int i = Values.Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        public override void Insert(T value)
        {
            Values.Add(_minValue);
            IncreaseKey(Values.Count - 1, value);
        }

        public override void Remove(T value)
        {
            int index = Values.IndexOf(value);
            Values.RemoveAt(index);
            Heapify(index);
        }

        public T Maximum()
        {
            return Values[0];
        }

        private void Heapify(int i)
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
                Heapify(i);
            }
        }

        private void SwapElements(int i, int j)
        {
            T temp = Values[i];
            Values[i] = Values[j];
            Values[j] = temp;
        }

        private void IncreaseKey(int i, T value)
        {
            if (value.CompareTo(Values[i]) < 0)
                throw new ArgumentException("new value is smaller than current", "value");
            Values[i] = value;
            while (i > 0 && Values[Parent(i)].CompareTo(Values[i]) < 0)
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }
    }
}
