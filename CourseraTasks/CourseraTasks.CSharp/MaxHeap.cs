using System;
using System.Collections.Generic;

namespace CourseraTasks.CSharp
{
    public class MaxPriorityQuery<T> : PriorityQuery<T>
        where T : IComparable<T>
    {
        public MaxPriorityQuery(IEnumerable<T> values) : base(values)
        {
        }

        public MaxPriorityQuery()
        {
        }

        public T Max
        {
            get
            {
                return HighestPriorityElement;
            }
        }

        public T ExtractMax()
        {
            return ExtractHighestPriorityElement();
        }

        protected override bool IsHigherPriority(T priority1, T priority2)
        {
            return priority1.CompareTo(priority2) > 0;
        }
    }
}
