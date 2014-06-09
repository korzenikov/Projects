using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public abstract class Heap<T>
    {
        protected List<T> Values { get; set; }

        protected Heap()
        {
            Values = new List<T>(); 
        }

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

        public abstract void Build(IEnumerable<T> values);

        public abstract void Insert(T value);

        public abstract void Remove(T value);
    }
}
