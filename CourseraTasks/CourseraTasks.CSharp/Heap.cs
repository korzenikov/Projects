using System.Collections.Generic;
using System.Linq;

namespace CourseraTasks.CSharp
{
    public abstract class PriorityQuery<T> : PriorityQueueBase<T, T>
    {
        private List<T> _values;

        protected PriorityQuery(IEnumerable<T> values)
        {
            Build(values);
        }

        protected PriorityQuery()
        {
            _values = new List<T>();
        }

        public override int Count 
        { 
            get
            {
                return _values.Count;
            }
        }

        public void Add(T value)
        {
            _values.Add(value);
            BubbleUp(_values.Count - 1);
        }

        protected override void SwapElements(int i, int j)
        {
            T temp = _values[i];
            _values[i] = _values[j];
            _values[j] = temp;
        }

        protected override T GetPriority(int index)
        {
            return _values[index];
        }

        protected override T GetElement(int index)
        {
            return _values[index];
        }

        protected override void RemoveAt(int index)
        {
            _values.RemoveAt(index);
        }

        private void Build(IEnumerable<T> values)
        {
            _values = values.ToList();
            HeapifyAll();
        }
    }
}
