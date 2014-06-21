namespace CourseraTasks.CSharp
{
    public abstract class PriorityQueueBase<TElement, TPriority>
    {
        public abstract int Count { get; }

        public TElement HighestPriorityElement
        {
            get
            {
                return GetElement(0);
            }
        }

        public TElement ExtractHighestPriorityElement()
        {
            TElement top = GetElement(0);
            SwapElements(0, Count - 1);
            RemoveAt(Count - 1);
            Heapify(0);
            return top;
        }
        
        protected abstract bool IsHigherPriority(TPriority priority1, TPriority priority2);

        protected abstract void RemoveAt(int index);

        protected abstract void SwapElements(int i, int j);

        protected void Heapify(int i)
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

        protected void HeapifyAll()
        {
            for (int i = Count / 2; i >= 0; i--)
            {
                Heapify(i);
            }
        }

        protected abstract TPriority GetPriority(int index);

        protected abstract TElement GetElement(int index);

        protected void BubbleUp(int i)
        {
            while (i > 0 && IsHigherPriority(GetPriority(i), GetPriority(Parent(i))))
            {
                var parent = Parent(i);
                SwapElements(i, parent);
                i = parent;
            }
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
    }
}
