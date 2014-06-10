using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public abstract class Heap<T>
    {
        protected List<T> Values { get; set; }

        public T Top()
        {
            return Values[0];
        }

        public T ExtractTop()
        {
            T max = Values[0];
            Values[0] = Values[Values.Count - 1];
            Values.RemoveAt(Values.Count - 1);
            Heapify(0);
            return max;
        }

        public void Insert(T value)
        {
            Values.Add(value);
            BubbleUp(Values.Count - 1);
        }


        protected void Build(IEnumerable<T> values)
        {
            Values = values.ToList();
            for (int i = Values.Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        protected abstract void Heapify(int i);

        protected abstract bool ShouldBeHigher(int i, int j);

        protected int Parent(int i)
        {
            return (i + 1) / 2 - 1;
        }

        protected int Left(int i)
        {
            return 2 * i + 1;
        }

        protected int Right(int i)
        {
            return 2 * i + 2;
        }

        protected void SwapElements(int i, int j)
        {
            T temp = Values[i];
            Values[i] = Values[j];
            Values[j] = temp;
        }

        private void BubbleUp(int i)
        {
            while (i > 0 && ShouldBeHigher(i, Parent(i)))
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
        }
    }
}
